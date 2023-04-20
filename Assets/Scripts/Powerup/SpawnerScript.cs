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
        spawnedPickup = null;
        nextSpawnTime = Time.time + spawnDelay;
        tf = gameObject.transform;
    }

    // Update is called once per frame
    void Update()
    {
        // If it is time to spawn a pickup
        if(spawnedPickup == null)
        {
            if (Time.time > nextSpawnTime)
            {
                //Spawn it and set the next time
                spawnedPickup = Instantiate<GameObject>(pickupPrefab, tf.position, Quaternion.identity) as GameObject;
                nextSpawnTime = Time.time + spawnDelay;
            }
        }
        else
        {
            //Otherwise, postpone the spawn
            nextSpawnTime = Time.time + spawnDelay;
        }
        
    }
}
