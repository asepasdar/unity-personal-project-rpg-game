using UnityEngine;
using UnityEngine.EventSystems;
using RPG.Interact.Base;
using RPG.Data.Player;
using RPG.Data.Enemy;
using RPG.Combat.Base;
using System.Linq;
using RPG.Stats.Base;
using RPG.Scriptable.Base.Event.Position;
using RPG.Scriptable.Base.Event.Boolean;

namespace RPG.Movement.Base.Player
{
    public class PlayerMovement : Movement
    {
        [Header("Listening Channel")]
        public EventPosition ChannelPosition;
        public EventBool ChannelOpenUI;

        CombatCharacter _myCombat;
        Camera _camMain;
        Transform _cam;
        Vector2 _input;
        Interactable _interactTarget;

        bool _canControl = true;
        bool _isAttacking = false;
        bool _isOpenUI = false;
        public void Attack() {
            _isAttacking = true;
        }
        protected override void Start()
        {
            base.Start();
            _camMain = Camera.main;
            _cam = PlayerData.instance.Resources.PlayerMainCamera;
            _myCombat = PlayerData.instance.Player.GetComponent<CombatCharacter>();
            UpdateManager.instance.Movements.Add(this);

            ChannelPosition.Channel += SetMyPosition;
            ChannelOpenUI.Channel += ChangeState;
        }

        void SetMyPosition(Vector3 position) {
            agent.Warp(position);
        }

        void ChangeState(bool status) {
            _isOpenUI = !status;
        }

        public override void FixedUpdateMe()
        {
            if (_isOpenUI && EventSystem.current.IsPointerOverGameObject())
                return;
            if (_isAttacking && _canControl) {
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
                _isAttacking = false;
                return;
            }
            if (Input.GetMouseButtonDown(0) && _canControl && !_isOpenUI)
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
                _interactTarget = null;
                
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

            base.FixedUpdateMe();
        }
    }
}
