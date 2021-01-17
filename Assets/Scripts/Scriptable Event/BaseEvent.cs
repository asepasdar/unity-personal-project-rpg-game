using UnityEngine;

namespace RPG.Scriptable.Base.Event
{
    [CreateAssetMenu(fileName = "New Item", menuName = "Event/Base Event")]
    public class BaseEvent : ScriptableObject
    {
        public System.Action Channel;

        public void RaiseEvent()
        {
            Channel?.Invoke();
        }
    }
}
