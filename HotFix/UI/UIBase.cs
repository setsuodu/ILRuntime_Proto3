using UnityEngine;

namespace HotFix
{
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
        public virtual void ApplyLanguage() { }
    }
}