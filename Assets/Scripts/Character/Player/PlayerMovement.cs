using UnityEngine;
using UnityEngine.EventSystems;
using RPG.Interact.Base;
using RPG.Data.Player;
using RPG.Data.Enemy;
using RPG.Combat.Base;
using System.Linq;
using RPG.Stats.Base;
using System.Collections;

namespace RPG.Movement.Base.Player
{
    public class PlayerMovement : Movement
    {
        CombatCharacter _myCombat;
        Camera _camMain;
        Transform _cam;
        Vector2 _input;
        Interactable _interactTarget;

        bool _canControl = true;
        protected override void Start()
        {
            base.Start();
            _camMain = Camera.main;
            _cam = PlayerData.instance.Resources.PlayerMainCamera;
            _myCombat = PlayerData.instance.Player.GetComponent<CombatCharacter>();
            
        }

        // Update is called once per frame
        protected override void FixedUpdate()
        {
            if (PlayerData.instance.IsOpenUI && EventSystem.current.IsPointerOverGameObject())
                return;
            if (Input.GetKeyDown(KeyCode.A) && _canControl) {
                if (_interactTarget != null)
                {
                    _interactTarget.OnDefocus();
                    _interactTarget = null;
                }

                Transform tClosest = EnemyData.instance.Enemies.
                    OrderBy(t => (t.position - transform.position).sqrMagnitude).
                    FirstOrDefault();
                if (tClosest != null) {
                    float sqlLen = (tClosest.position - transform.position).sqrMagnitude;
                    _interactTarget = tClosest.GetComponent<Interactable>();
                    _interactTarget.OnFocus();
                    transform.LookAt(tClosest);

                    bool response = _myCombat.Attack(tClosest.GetComponent<BaseStats>(), tClosest, sqlLen, () => {
                        _canControl = true;
                    });
                    if(response)
                        _canControl = false;
                }
                
                return;
            }
            if (Input.GetMouseButtonDown(0) && _canControl)
            {
                if (_interactTarget != null)
                {
                    _interactTarget.OnDefocus();
                    _interactTarget = null;
                }
                Ray ray = _camMain.ScreenPointToRay(Input.mousePosition);
                if (Physics.Raycast(ray, out RaycastHit hit, 100))
                {
                    _interactTarget = hit.collider.GetComponent<Interactable>();
                    if (_interactTarget != null)
                    {
                        _interactTarget.OnFocus();
                        MoveToObject(_interactTarget.interactionSpot.position, _interactTarget.radius);
                    }
                }
            }

            if ( (UltimateJoystick.GetHorizontalAxis("Movement") != 0 || UltimateJoystick.GetVerticalAxis("Movement") != 0) 
                && _canControl) {
                if (_interactTarget != null)
                {
                    _interactTarget.OnDefocus();
                    _interactTarget = null;
                }
                
                _input = new Vector2(UltimateJoystick.GetHorizontalAxis("Movement"), UltimateJoystick.GetVerticalAxis("Movement"));
                _input = Vector2.ClampMagnitude(_input, 1);

                Vector3 camF = _cam.forward;
                Vector3 camR = _cam.right;

                camF.y = 0;
                camR.y = 0;
                camF = camF.normalized;
                camR = camR.normalized;

                Vector3 destination = transform.position + (camF * _input.y + camR * _input.x);
                MoveToPoint(destination);
            }

            base.FixedUpdate();
        }
    }
}
