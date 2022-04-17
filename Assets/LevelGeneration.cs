using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class LevelGeneration : NetworkBehaviour
{
    [SerializeField] public List<Biome> Biomes;
    public int size;

    [SerializeField] private GameObject cursor;
    [SerializeField] private GameObject hex;
    [SerializeField] private GameObject hexParent;

    [Command(requiresAuthority = false)] // mo¿na napisaæ "[Command(requiresAuthority = false)]"
    public void CmdGenerate() => Generate(true);

    [ClientRpc]
    void Generate(bool normalBiomes)
    {
        if (!isServer) return;
        hexParent = GameObject.Find("HexagonsFolder");

        #region counting length
        int lgth = 0;

        for (int i = 0; i < size + 4; i++)
        {
            if (i % 2 == 1)
            {
                for (int a = 0; a < size; a++) lgth += 1;
            }
            else
            {
                for (int a = 0; a < size + 1; a++) lgth += 1;
            }
        }
        #endregion

        BiomeData[] allHexes;
        allHexes = FindObjectsOfType<BiomeData>();
        for (int i = 0; i < allHexes.Length; i++)
        {
            Destroy(allHexes[i].gameObject);
        }

        GameObject[] hexes = new GameObject[lgth];

        int c = 0;
        cursor.transform.position = Vector3.zero;

        #region spawning hexes
        for (int i = 0; i < size + 4; i++)
        {
            if (i % 2 == 0)
            {
                cursor.transform.position = new Vector3(0, cursor.transform.position.y, 0);
                for (int a = 0; a < size; a++)
                {
                    GameObject curHex = Instantiate(hex, cursor.transform.position, Quaternion.identity);
                    NetworkServer.Spawn(curHex);
                    curHex.transform.parent = hexParent.transform;
                    curHex.gameObject.name = c.ToString();
                    // trzeba jeszcze zapisac ze ten hexagon jest jednym z 2 (lub jakiejkolwiek innej ale tej mniejszej liczby)
                    cursor.transform.position = new Vector3(cursor.transform.position.x + 2, cursor.transform.position.y, 0); // do przodu
                    hexes[c] = curHex;
                    c++;
                }
            }
            else
            {
                cursor.transform.position = new Vector3(-1, cursor.transform.position.y, 0);
                for (int a = 0; a < size + 1; a++)
                {
                    GameObject curHex = Instantiate(hex, cursor.transform.position, Quaternion.identity);
                    NetworkServer.Spawn(curHex);
                    cursor.transform.position = new Vector3(cursor.transform.position.x + 2, cursor.transform.position.y, 0); // do przodu
                    curHex.transform.parent = hexParent.transform;
                    curHex.gameObject.name = c.ToString();

                    hexes[c] = curHex;
                    c++;
                }
            }
            cursor.transform.position = new Vector3(cursor.transform.position.x, cursor.transform.position.y - 0.5f, 0); // w dó³

        }
        #endregion
        #region biome setup

        if (normalBiomes)
        {
            for (int i = 0; i < hexes.Length - 1; i++)
            {
                hexes[i].GetComponent<BiomeData>().SetBiomeValues(Biomes[Random.Range(0, 4)]);
            }
        }
        else
        {
            //zachowanie dla innych biomow
        }
        #endregion
    }
    
    [Server]
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            CmdGenerate();
        }
    }
}

[System.Serializable]
public class Biome
{
    public string name;

    public GameObject[] structures;

    [Range(0.0f, 10.0f)] public float scrap;
    [Range(0.0f, 10.0f)] public float food;
}
