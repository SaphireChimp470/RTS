using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using UnityEditor.Events;

public class BuildOnHexManager : NetworkBehaviour
{
    [SerializeField] private GameObject[] buildMenus;

    [SerializeField] private GameObject curHex;

    public GameObject CurHex
    {
        get
        {
            return curHex;
        }
        set
        {
            curHex = value;
            #region setting the buildMenus (on & off)
            if (value.gameObject.GetComponent<BiomeData>().name == "Planet")
            {
                for (int i = 0; i < buildMenus.Length; i++)
                {
                    buildMenus[i].gameObject.SetActive(false);
                }
                buildMenus[0].gameObject.SetActive(true);
            }
            else if (value.gameObject.GetComponent<BiomeData>().name == "ShipWreck")
            {
                for (int i = 0; i < buildMenus.Length; i++)
                {
                    buildMenus[i].gameObject.SetActive(false);
                }
                buildMenus[1].gameObject.SetActive(true);
            }
            else if (value.gameObject.GetComponent<BiomeData>().name == "Space")
            {
                for (int i = 0; i < buildMenus.Length; i++)
                {
                    buildMenus[i].gameObject.SetActive(false);
                }
                buildMenus[2].gameObject.SetActive(true);
            }
            else if (value.gameObject.GetComponent<BiomeData>().name == "Asteroids")
            {
                for (int i = 0; i < buildMenus.Length; i++)
                {
                    buildMenus[i].gameObject.SetActive(false);
                }
                buildMenus[3].gameObject.SetActive(true);
            }
            #endregion
        }
    }

    private static BuildOnHexManager instance;
    public static BuildOnHexManager Instance { get => instance; }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Debug.Log("Nie moze byc 2 singletonow >:-c");
            Destroy(this.gameObject);
        }
    }

    private GameObject Structure;
    [Client]
    public void PreBuild(GameObject structure)
    {
        Structure = structure;
        CmdBuild();
    }

    [Command(requiresAuthority = false)]
    private void CmdBuild() => Build();

    [ClientRpc]
    private void Build()
    {
        GameObject curStructure = Instantiate(Structure, curHex.transform.position, Quaternion.identity);
        NetworkServer.Spawn(curStructure);
        curStructure.transform.parent = curHex.transform;
    }
}
