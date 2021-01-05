using System.Collections;
using UnityEngine;
using RPG.Data.Player;

namespace RPG.Interact.Base
{
    [RequireComponent(typeof(Collider))]
    public class Interactable : MonoBehaviour
    {
        public float radius = 3f;
        public Transform interactionSpot;

        protected bool _isFocus = false;

        protected virtual void Start()
        {
            SphereCollider col = GetComponent<SphereCollider>();
            col.radius = radius;
            col.isTrigger = true;
        }

        #region All Virtual Function
        protected virtual IEnumerator Interact() {
            yield return new WaitForEndOfFrame();
        }

        /// <summary>
        /// Fungsi jika object masuk ke dalam radius
        /// </summary>
        /// <param name="other"></param>
        protected virtual void OnTriggerEnter(Collider other)
        {
            if (_isFocus == true && other.gameObject.CompareTag("Player")) StartCoroutine(Interact());
        }

        /// <summary>
        /// Fungsi ketika player
        /// </summary>
        /// <param name="other"></param>
        protected virtual void OnTriggerExit(Collider other)
        {
            if (_isFocus == true && other.gameObject.CompareTag("Player")) OnDefocus();
        }

        /// <summary>
        /// Fungsi ketika player mengklik / focus pada object
        /// </summary>
        public virtual void OnFocus() {
            _isFocus = true;
        }

        /// <summary>
        /// Fungsi untuk melakukan defocus (set ke false)
        /// </summary>
        public virtual void OnDefocus() {
            _isFocus = false;
        }

        /// <summary>
        /// Menggambarkan radius / area interact
        /// </summary>
        protected virtual void OnDrawGizmosSelected()
        {
            if (interactionSpot == null) interactionSpot = transform;
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(transform.position, radius);
        }
        #endregion

    }
}
