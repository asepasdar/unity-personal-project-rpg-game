using UnityEngine;
using RPG.Interact.Base;
using RPG.Data.Player;

namespace RPG.Movement.Base.Player
{
    public class PlayerMovement : Movement
    {
        Camera _camMain;
        Transform _cam;
        Vector2 _input;
        protected override void Start()
        {
            base.Start();
            _camMain = Camera.main;
            _cam = PlayerData.instance.Resources.PlayerMainCamera;
        }

        // Update is called once per frame
        protected override void FixedUpdate()
        {
            if (Input.GetMouseButtonDown(0))
            {
                Ray ray = _camMain.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;

                if (Physics.Raycast(ray, out hit, 100))
                {
                    Interactable _interactTarget = hit.collider.GetComponent<Interactable>();
                    if (_interactTarget != null)
                    {
                        _interactTarget.OnFocus();
                        MoveToObject(_interactTarget.interactionSpot.position, _interactTarget.radius);
                    }
                }
            }

            if (Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0) {

                _input = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
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
