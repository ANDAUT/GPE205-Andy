using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankPawn : Pawn
{
    //Classes that inherit from others must follow their rules
    //In this case, TankPawn MUST declare specific functions


    // Start is called before the first frame update
    public override void Start()
    {
        //Debug.Log("Tank in action!");
        base.Start();
        //base. seems to be refering to the superclass?
    }

    // Update is called once per frame
    public override void Update()
    {
        base.Update();
    }

    //As mentioned before, classes that inherit must follow the rules
    //In this case, TankPawn is inheriting off of Pawn, and MUST declare the following functions
    public override void MoveForward()
    {
        if (movement != null)
        {
            //Debug.Log("Moving Forward!");
            movement.Move(transform.forward, moveSpeed);
        } else
        {
            Debug.LogWarning("No Movement Script in TankPawn.MoveForward()!");
        }
        
    }

    public override void MoveBackward()
    {
        if (movement != null)
        {
            //Debug.Log("Moving Backward!");
            movement.Move(transform.forward, -moveSpeed);
        } else
        {
            Debug.LogWarning("No Movement Script in TankPawn.MoveBackward()!");
        }
        
    }
    public override void TurnRight()
    {
        if (movement != null)
        {
            //Debug.Log("Turning Right!");
            movement.Turn(turnSpeed);
        } else
        {
            Debug.LogWarning("No Movement Script in TankPawn.TurnRight()!");
        }
        
    }
    public override void TurnLeft()
    {
        if (movement != null)
        {
            //Debug.Log("Turning Left!");
            movement.Turn(-turnSpeed);
        } else
        {
            Debug.LogWarning("No Movement Script in TankPawn.TurnLeft()!");
        }
        
    }
}
