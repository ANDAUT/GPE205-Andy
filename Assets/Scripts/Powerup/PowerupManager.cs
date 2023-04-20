using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class PowerupManager : MonoBehaviour
{

    public List<Powerup> powerups;
    private List<Powerup> removedPowerupQueue;

    // Start is called before the first frame update
    void Start()
    {
        powerups = new List<Powerup>();
    }

    // Update is called once per frame
    void Update()
    {
        
        DecrementPowerupTimers();
        LateUpdate();
        
        //LateUpdate();
    }

    // This add function will add a powerup
    public void Add (Powerup powerupToAdd)
    {
        //This'll create an Add() method soon
        powerupToAdd.Apply(this);

        //Saves it to the list
        powerups.Add(powerupToAdd);
    }

    // This will create a remove function instead
    public void Remove(Powerup powerupToRemove)
    {
        //This will create a Remove() method soon
        powerupToRemove.Remove(this);

        //Added a catch here in order to prevent a null pointer exception
        if(removedPowerupQueue != null)
        {
            //Adds the powerup to the 'to be removed' list
            removedPowerupQueue.Add(powerupToRemove);
        }
        
    }

    public void DecrementPowerupTimers()
    {
        foreach (Powerup powerup in powerups)
        {
            //Subtracts the time it took to draw the frame from the duration
            powerup.duration -= Time.deltaTime;
            //If the time is up, we want to remove this powerup
            if (powerup.duration <= 0)
            {
                Remove(powerup);
            }
        }
    }

    private void ApplyRemovePowerupsQueue()
    {
        //No more iterating = we can safely remove the powerups in the temporary list
        if (removedPowerupQueue != null)
        {
            foreach (Powerup powerup in removedPowerupQueue)
            {
                powerups.Remove(powerup);
            }
            // and then reset this list
            removedPowerupQueue.Clear();
        } else
        {
            //Debug.Log("Nothing in the list");
        }
    }

    private void LateUpdate()
    {
        ApplyRemovePowerupsQueue();
    }
}
