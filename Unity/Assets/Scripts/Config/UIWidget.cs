using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class UIWidget : MonoBehaviour
{
    public virtual void PopUp() 
    {
        Destroy(this.gameObject);
    }
}
