using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class BiomeData : NetworkBehaviour
{
    public string name;
    [SerializeField] private GameObject[] structures;
    [SerializeField] private float scrap;
    [SerializeField] private float food;

    [SerializeField] private Color space;
    [SerializeField] private Color shipWreck;
    [SerializeField] private Color planet;
    [SerializeField] private Color asteroids;

    [ClientRpc]
    public void SetBiomeValues(Biome biomeData) // note: server nie mo¿e odpalaæ komend!
    {
        name = biomeData.name;

        if (name == "Planet")
        {
            this.GetComponent<SpriteRenderer>().color = planet;
        }
        if (name == "ShipWreck")
        {
            this.GetComponent<SpriteRenderer>().color = shipWreck;
        }
        if (name == "Asteroids")
        {
            this.GetComponent<SpriteRenderer>().color = asteroids;
        }
        if (name == "Space")
        {
            this.GetComponent<SpriteRenderer>().color = space;
        }

        for (int i = 0; i < biomeData.structures.Length; i++) structures[i] = biomeData.structures[i];
        scrap = biomeData.scrap;
        food = biomeData.food;
    }
}
