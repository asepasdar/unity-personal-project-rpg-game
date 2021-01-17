using RPG.Scriptable.Base.Event.Position;
using UnityEngine;

public class TeleportLocator : MonoBehaviour
{
    public Vector3 Position;
    public EventPosition Channel;

    private void Start()
    {
        Channel.RaiseEvent(Position);
    }
}
