using UnityEngine;

public abstract class UIBase : MonoBehaviour
{
    public virtual void PopUp()
    {
        Destroy(this.gameObject);
    }

    public virtual void Pop()
    {
        UIManager.Get().Pop(this);
    }
    //public virtual void OnNetCallback(PacketType eventID, INetSerializable reader, NetPeer peer) { }
    //public virtual void OnUserCallback(PlayerStatus status) { }
    public virtual void ApplyLanguage() { }
}