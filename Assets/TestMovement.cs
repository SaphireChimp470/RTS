using Mirror;
using UnityEngine;

public class TestMovement : NetworkBehaviour // zwyk³e monobehaviour ale ma funkcjonalnosc z mirrora
{
    void Movement()
    {
        if (isLocalPlayer)
        {
            float mvHor = Input.GetAxis("Horizontal");
            float mvVer = Input.GetAxis("Vertical");
            Vector3 mov = new Vector3(mvHor, mvVer, 0);
            transform.position += mov;
        }
    }
    private void FixedUpdate()
    {
        Movement();
    }
}
