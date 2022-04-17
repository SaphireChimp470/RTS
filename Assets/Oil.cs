using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Oil : MonoBehaviour
{
    public int localStorage;
    public int level; //bedzie pobieral to z innego skryptu "structureData" czy cos
    private void Start()
    {
        localStorage = 0;
        level = 1;
        Invoke("Gather", 1f);
    }

    void Gather()
    {
        Debug.Log("essa1");
        localStorage += level * level;
        Invoke("Gather", 1f);
    }
}
