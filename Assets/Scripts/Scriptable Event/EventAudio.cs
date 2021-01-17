using UnityEngine;

namespace RPG.Scriptable.Base.Event.Audio
{
    [CreateAssetMenu(fileName = "New Item", menuName = "Event/Audio")]
    public class EventAudio : ScriptableObject
    {
        public System.Action<AudioClip> Channel;

        public void RaiseEvent(AudioClip audio) {
            Channel?.Invoke(audio);
        }
    }
}
