using UnityEngine;

namespace RPG.Scriptable.Base.Event.Boolean
{
    [CreateAssetMenu(fileName = "New Item", menuName = "Event/Event Boolean")]
    public class EventBool : ScriptableObject
    {
        public System.Action<bool> Channel;

        public void RaiseEvent(bool status)
        {
            Channel?.Invoke(status);
        }
    }
}
