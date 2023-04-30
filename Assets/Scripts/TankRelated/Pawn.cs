using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Pawn : MonoBehaviour
{

    //This holds our controller
    public Controller controller;

    //This determines the tank's speed (forward and back)
    public float moveSpeed;

    //This determines the tank's TURNING speed (left and right)
    public float turnSpeed;

    //Declaring the mover
    public MoveScript movement;

    //Declaring the shooter
    public Shooter shooter;

    //Declaring the shell prefab
    public GameObject Shell;
    //float for the force of the shell
    public float bulletForce;
    //float for damage the shell does
    public float damageDone;
    //float for lifetime for shell
    public float duration;
    //float for firerate
    public float fireRate;


    // Start = Called before first frame update
    public virtual void Start()
    {
        //Debug.Log("Pawn in action");
        movement = GetComponent<MoveScript>();
        shooter = GetComponent<Shooter>();
    }

    // This is called every frame (Dangerous)
    public virtual void Update()
    {
    }

    public abstract void MoveForward();
    public abstract void MoveBackward();
    public abstract void TurnRight();
    public abstract void TurnLeft();
    public abstract void RotateTowards(Vector3 targetPosition);
    public abstract void fireShell();

}
