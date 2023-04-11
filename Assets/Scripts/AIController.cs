using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIController : Controller
{

    //An enum organizing what states the AI can be in
    public enum AIState { Guard, Scan, Chase, Attack, Sus, ReturnHome};

    //The current state
    public AIState currentState;

    //Float for the time the AI's state has changed
    private float lastStateChangeTime;

    //Declares the target
    public GameObject target;

    //Flee distance float
    public float fleeDistance;


    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();
        ChangeState(AIState.Guard);
    }

    // Update is called once per frame
    public override void Update()
    {
        base.Update();
        //DoSeekState();
        MakeDecisions();
    }

    public override void DetectInput()
    {
    }

    public void MakeDecisions()
    {
        //Debug.Log("Thinking...");
        switch (currentState)
        {
            case AIState.Guard:
                //Does work
                DoIdleState();
                //Transition check
                if (IsDistanceLessThan(target, 10))
                {
                    ChangeState(AIState.Chase);
                }
                break;
            case AIState.Chase:
                //Does work
                DoChaseState();
                //Transition check
                if (!IsDistanceLessThan(target, 10))
                {
                    ChangeState(AIState.Guard);
                }
                break;
        }
    }

    public virtual void ChangeState(AIState newState)
    {
        //Changes the current state
        currentState = newState;
        //Saves the time when we changed states
        lastStateChangeTime = Time.time;
    }

    public void DoSeekState()
    {
        //Calls seek
        Seek(target);
    }

    protected virtual void DoIdleState()
    {
        //Nothing
    }

    public void Seek(GameObject target)
    {
       Seek(target.transform.position);
    }

    public void Seek(Vector3 targetPosition)
    {
        //This rotates toward the function
        pawn.RotateTowards(targetPosition);

        //This moves forward
        pawn.MoveForward();
    }

    public void Seek(Transform targetTransform)
    {
        //This seeks the position of our targetTransform
        Seek(targetTransform.position);
    }

    public void Seek(Pawn targetPawn)
    {
        //This seeks the Pawn's transform
        Seek(targetPawn.transform);
    }

    protected virtual void DoChaseState()
    {
        Seek(target);
    }

    public void Shoot()
    {
        //Tells the pawn to shoot
        pawn.fireShell();
    }

    protected virtual void DoAttackState()
    {
        //Chase
        Seek(target);
        //Fire
        Shoot();
    }

    protected void Flee()
    {
        //Finds the distance between this and the player
        float targetDistance = Vector3.Distance(target.transform.position, pawn.transform.position);

        //This determines the distance in percentages
        float percentOfFleeDistance = targetDistance / fleeDistance;

        //This clamps the percentage between 0% and 100%
        percentOfFleeDistance = Mathf.Clamp01(percentOfFleeDistance);

        //This flips the previous value
        float flippedPercentOfFleeDistance = 1 - percentOfFleeDistance;

        //At the moment, I unsure over how to implement this

        //This finds the vector to the target
        Vector3 vectorToTarget = target.transform.position - pawn.transform.position;

        //This finds the vector AWAY from the target by multiplying by -1 (it flips the above vector basically)
        Vector3 vectorAwayFromTarget = -vectorToTarget;

        //This finds the vector we would travel down in order to flee
        Vector3 fleeVector = vectorAwayFromTarget.normalized * fleeDistance;

        //This seeks the point that is "fleeVector" away from our current position
        Seek(pawn.transform.position + fleeVector);
    }

    protected bool IsDistanceLessThan(GameObject target, float distance)
    {
        if (Vector3.Distance (pawn.transform.position, target.transform.position) < distance)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

}
