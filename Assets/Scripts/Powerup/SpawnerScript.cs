using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerScript : MonoBehaviour
{
    public GameObject pickupPrefab;
    public float spawnDelay;
    private float nextSpawnTime;
    private Transform tf;
    private GameObject spawnedPickup;

    // Start is called before the first frame update
    void Start()
    {
        nextSpawnTime = Time.time + spawnDelay;
    }

    // Update is called once per frame
    void Update()
    {
        //If there's nothing there, something will spawn
        if (spawnedPickup == null)
        {
            //...and if it's time to spawn
            if (Time.time > nextSpawnTime)
            {
                //Spawn it and set the next time
                spawnedPickup = Instantiate(pickupPrefab, transform.position, Quaternion.identity) as GameObject;
                nextSpawnTime = Time.time + spawnDelay;
            } else
            {
                //Otherwise, postpone the spawn
                nextSpawnTime = Time.time + spawnDelay;
            }
        }
    }
}
