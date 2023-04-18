using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class HealthPowerup : Powerup
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public float healthToAdd;
    public override void Apply(PowerupManager target)
    {
        //This will apply health changes
        Health targetHealth = target.GetComponent<Health>();
        if (targetHealth != null)
        {
            //Remember, the second parameter is the pawn who caused the healing (the healer)
            targetHealth.Heal(healthToAdd, target.GetComponent<Pawn>());
        }
    }

    public override void Remove(PowerupManager target)
    {
        //This will remove health changes
    }
}
