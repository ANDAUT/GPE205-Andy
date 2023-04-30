using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]

public class ScorePowerup : Powerup
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public float scoreToAdd;

    public override void Apply(PowerupManager target)
    {
        //This will apply score changes
        TankPawn scorer = target.GetComponent<TankPawn>();
        if (scorer != null)
        {
            //This calls the 'boost' function within tankpawn to change the speed
            scorer.controller.AddToScore(scoreToAdd);
        }

    }

    public override void Remove(PowerupManager target)
    {
        //This will undo score changes... for... some reason?
    }
}
