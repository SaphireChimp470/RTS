using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildableResourcesManager : MonoBehaviour
{
    public int scrap;
    public int components;
    public int fuel;

    public void AddResources(int type, int quantity)
    {
        if (type == 0)
        {
            scrap += quantity;
        }
        else if (type == 1)
        {
            components += quantity;
        }
        else if (type == 2)
        {
            fuel += quantity;
        }
    }
}
