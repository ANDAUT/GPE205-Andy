using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPickup : MonoBehaviour
{
    public HealthPowerup powerup;

    public void OnTriggerEnter(Collider other)
    {
        //Stored variable of other object's powerupcontroller (if it has one)
        PowerupManager powerupManager = other.GetComponent<PowerupManager>();

        //If the colliding object DOES have a powerupcontroller...
        if(powerupManager != null)
        {
            //This adds the powerup
            powerupManager.Add(powerup);

            //and this destroys this pickup
            Destroy(gameObject);
        }

    }

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
    }
}
