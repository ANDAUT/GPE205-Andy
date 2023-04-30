using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class MinePowerup : Powerup
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public float healthToRemove;
    public float speedToRemove;
    private bool hurtByMine;
    public override void Apply(PowerupManager target)
    {
        //This will apply health changes
        Health targetHealth = target.GetComponent<Health>();
        if (targetHealth != null)
        {
            //Remember, the second parameter is the pawn who caused the healing (the healer)
            targetHealth.Heal(-healthToRemove, target.GetComponent<Pawn>());
        }

        TankPawn targetSpeed = target.GetComponent<TankPawn>();
        if (targetSpeed != null)
        {
            //This calls the 'boost' function within tankpawn to change the speed
            targetSpeed.changeSpeed(-speedToRemove);
            hurtByMine = true;
        }

    }

    public override void Remove(PowerupManager target)
    {
        //This will remove health and speed changes

        TankPawn targetSpeed = target.GetComponent<TankPawn>();
        if (Time.time > duration && hurtByMine == true)
        {
            targetSpeed.changeSpeed(speedToRemove);
            hurtByMine = false;
        }
    }

}
