using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : Controller
{
    //Same rule as TankPawn, PlayerController must call certain abstract methods called in Controller

    //Declaring keys for movement
    //the 'Keycode' type is provided by Unity
    public KeyCode moveForwardKey;
    public KeyCode moveBackwardKey;
    public KeyCode turnRightKey;
    public KeyCode turnLeftKey;

    // Start is called before the first frame update
    public override void Start()
    {
        //Checks for GameManager
        if(GameManager.instance != null)
        {
            //Checks for players
            if (GameManager.instance.players != null)
            {
                //Adds itself to the player list
                GameManager.instance.players.Add(this);
            }
        }
        //This runs the parent's Start() function
        base.Start();
    }

    public void OnDestroy()
    {
        // Checks for manager
        if (GameManager.instance != null)
        {
            // Tracks players
            if (GameManager.instance.players != null)
            {
                // Leaves the list (it's done for now)
                GameManager.instance.players.Remove(this);
            }
        }
    }

    // Update is called once per frame
    public override void Update()
    {
        //Detects keyboard inputs
        DetectInput();


        //base. refers to the parent class
        //tl;dr, base.Update(); runs code from the parent's Update() rather than this one
        base.Update();
    }

    public override void DetectInput()
    {
        if (Input.GetKey(moveForwardKey))
        {
            pawn.MoveForward();
        }

        if (Input.GetKey(moveBackwardKey))
        {
            pawn.MoveBackward();
        }

        if (Input.GetKey(turnRightKey))
        {
            pawn.TurnRight();
        }

        if (Input.GetKey(turnLeftKey))
        {
            pawn.TurnLeft();
        }
    }
}
