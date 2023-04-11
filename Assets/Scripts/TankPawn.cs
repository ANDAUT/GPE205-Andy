using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankPawn : Pawn
{
    //Classes that inherit from others must follow their rules
    //In this case, TankPawn MUST declare specific functions

    //FireRate
    private float fireCooldown;


    // Start is called before the first frame update
    public override void Start()
    {
        //Debug.Log("Tank in action!");
        base.Start();
        //base. seems to be refering to the superclass?

        fireCooldown = Time.time + fireRate;
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

        //Errors are much less scary when they're yellow
        
    }

    public override void RotateTowards(Vector3 targetPosition)
    {
        //This looks for the vector for the target
        Vector3 vectorToTarget = targetPosition - transform.position;

        //This finds the rotation that looks down said vector
        Quaternion targetRotation = Quaternion.LookRotation(vectorToTarget, Vector3.up);

        //Rotates toward the vector, but this code uses time to ensure it doesn't happen instantly, turnspeed judges how fast an enemy tank will turn
        transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, turnSpeed * Time.deltaTime);
    }

    public override void fireShell()
    {
        if(Time.time >= fireCooldown)
        {
            shooter.Shoot(Shell, bulletForce, damageDone, duration);
            fireCooldown = Time.time + fireRate;
        }
    }
}
