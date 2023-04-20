using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class SpeedPowerup : Powerup
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private float currentSpeed;
    public float speedToAdd;
    private bool boosted;
    public override void Apply(PowerupManager target)
    {
        TankPawn targetSpeed = target.GetComponent<TankPawn>();
        if (targetSpeed != null)
        {
            //This calls the 'boost' function within tankpawn to change the speed
            targetSpeed.changeSpeed(speedToAdd);
            boosted = true;
        }
    }

    public override void Remove(PowerupManager target)
    {
        TankPawn targetSpeed = target.GetComponent<TankPawn>();
        if (Time.time > duration && boosted == true)
        {
            targetSpeed.changeSpeed(-speedToAdd);
            boosted = false;
        }
    }
}
