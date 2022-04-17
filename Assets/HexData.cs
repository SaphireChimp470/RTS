using System.Collections;
using UnityEngine;

public class HexData : MonoBehaviour
{
    private Animator anim;
    private void Start()
    {
        anim = this.gameObject.GetComponent<Animator>();
    }
    private void OnMouseEnter()
    {
        anim.SetBool("sizeUp", true);
    }
    private void OnMouseExit()
    {
        anim.SetBool("sizeUp", false);
    }
    private void OnMouseUpAsButton()
    {
        BuildOnHexManager.Instance.CurHex = this.gameObject;
    }

}
