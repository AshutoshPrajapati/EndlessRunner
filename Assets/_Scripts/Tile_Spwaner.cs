using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile_Spwaner : MonoBehaviour
{
    [Header("Road Prefeb")]
    public GameObject[] roadPrefeb;
    public float zSpwan = 0;
    public int numberOfTile = 4;
    private float tileLength = 30;
    public List<GameObject> activeTiles = new List<GameObject>();

    [Header("------Player Transform------")]
    public Transform playerTransform;


    void Start()
    {
        for (int i = 0; i < numberOfTile; i++)
        {
            if (i == 0)
            {
                SpwanTile(0);
            }
            else
            {
                SpwanTile(Random.Range(0, roadPrefeb.Length));
            }
        }
    }


    void Update()
    {
        if (playerTransform.position.z-35 > zSpwan - (numberOfTile * tileLength))
        {
            SpwanTile(Random.Range(0, roadPrefeb.Length));
            DeletePreviousTile();
        }
    }
    public void SpwanTile(int spwanIndex)
    {
        GameObject go =  Instantiate(roadPrefeb[spwanIndex], transform.forward * zSpwan, transform.rotation);
        activeTiles.Add(go);
        zSpwan += tileLength;
    }
    private void DeletePreviousTile()
    {
        Destroy(activeTiles[0]);
        activeTiles.RemoveAt(0);

    }
}
