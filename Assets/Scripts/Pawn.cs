using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Pawn : MonoBehaviour
{

    //This determines the tank's speed (forward and back)
    public float moveSpeed;

    //This determines the tank's TURNING speed (left and right)
    public float turnSpeed;

    //Declaring the mover
    public MoveScript movement;

    // Start = Called before first frame update
   public virtual void Start()
    {
        //Debug.Log("Pawn in action");
        movement = GetComponent<MoveScript>();
    }

    // This is called every frame (Dangerous)
    public virtual void Update()
    {
    }

    public abstract void MoveForward();
    public abstract void MoveBackward();
    public abstract void TurnRight();
    public abstract void TurnLeft();

}
