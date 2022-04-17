using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class AuthorityManager : NetworkBehaviour
{
    [SerializeField] private GameObject generationPrefab;
    public override void OnStartServer()
    {
        if (isServer && isClient)
        {
            Debug.Log(this.name);
            GameObject GenerationObject = Instantiate(generationPrefab);
            NetworkServer.Spawn(GenerationObject, connectionToClient);
        }
    }
}
