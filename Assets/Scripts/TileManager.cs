using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class TileManager : MonoBehaviour {
    public GameObject[] tilePrefabs;
    private Transform playerTransform;
    private float spawnZ = 0.0f;
    private float tileLength = 5.47f;
    private int amnTilesOnScreen = 7;
    private float safeZone = 6.0f;
    private List<GameObject> activeTiles;
    private int lastIndex = 0;
	// Use this for initialization
	void Start () {
        activeTiles = new List<GameObject>();
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        for(int i=0;i<amnTilesOnScreen;i++)
        {
            if (i < 2) SpawnTiles(0);
            else SpawnTiles();
           
        }
    }
	
	// Update is called once per frame
	void Update () {
        if (playerTransform.position.z-safeZone>(spawnZ-amnTilesOnScreen*tileLength))
        {
            SpawnTiles();
            DeleteTiles();
        }
	}
    private void DeleteTiles()
    {
        Destroy(activeTiles[0]);
        activeTiles.RemoveAt(0);
    }
    private int RandomIndex()
    {
        if (tilePrefabs.Length<=1) { return 0; }
        int randomIndex = lastIndex;
        while(randomIndex == lastIndex)
        {
            randomIndex = Random.Range(0,tilePrefabs.Length);
        }
        lastIndex = randomIndex;
        return randomIndex;
    }
    private void SpawnTiles(int prefabIndex =-1)
    {
        GameObject go;
        if(prefabIndex == -1)
            go = Instantiate(tilePrefabs[RandomIndex()]) as GameObject;
        else
            go = Instantiate(tilePrefabs[prefabIndex]) as GameObject;
      
        go.transform.SetParent(transform);
        go.transform.position = Vector3.forward * spawnZ;
        spawnZ += tileLength;
        activeTiles.Add(go);
    }
}
