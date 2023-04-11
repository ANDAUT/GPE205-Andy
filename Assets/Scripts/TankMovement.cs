using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankMovement : MoveScript
{
    //Declaring a variable for the rigidbody component
    private Rigidbody rBody;

    // Start is called before the first frame update
    public override void Start()
    {
        // Getting the Rigidbody component (Continued)
        rBody = GetComponent<Rigidbody>();
    }

    public override void Move(Vector3 direction, float speed)
    {
        // Utilizing the already declared values (direction and speed) to determine movement
        Vector3 moveVector = direction.normalized * speed * Time.deltaTime;
        rBody.MovePosition(rBody.position + moveVector);
    }

    public override void Turn(float turnSpeed)
    {
        rBody.transform.Rotate(0, turnSpeed * Time.deltaTime, 0);
    }
}
