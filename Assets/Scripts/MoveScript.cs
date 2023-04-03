using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MoveScript : MonoBehaviour
{
    // Declaring an abstract start function
    public abstract void Start();

    // Declaring the abstract movement function
    public abstract void Move(Vector3 direction, float speed);

    public abstract void Turn(float turnSpeed);
}
