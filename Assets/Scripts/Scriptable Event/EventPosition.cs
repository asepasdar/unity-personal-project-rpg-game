using UnityEngine;

namespace RPG.Scriptable.Base.Event.Position
{
    [CreateAssetMenu(fileName = "New Item", menuName = "Event/Position")]
    public class EventPosition : ScriptableObject
    {
        public System.Action<Vector3> Channel;

        public void RaiseEvent(Vector3 audio)
        {
            Channel?.Invoke(audio);
        }
    }
}
