using UnityEngine;

namespace RPG.CameraController.Base
{
    public class CameraController : MonoBehaviour
    {
        public Transform target;

        public Vector3 offset;
        public float zoomSpeed = 4f;
        public float minZoom = 5f;
        public float maxZoom = 15f;

        public float pitch = 2f;
        public float rotationSpeed = 2f;

        float currentZoom = 5f;

        void Update()
        {
            currentZoom -= Input.GetAxis("Mouse ScrollWheel") * zoomSpeed;
            currentZoom = Mathf.Clamp(currentZoom, minZoom, maxZoom);
        }
        void LateUpdate()
        {
            Quaternion camTurnAngle = Quaternion.AngleAxis(UltimateJoystick.GetHorizontalAxis("Rotate") * rotationSpeed, Vector3.up);
            offset = camTurnAngle * offset;
            transform.position = target.position - offset * currentZoom;
            transform.LookAt(target.position + Vector3.up * pitch);
        }
    }
}
