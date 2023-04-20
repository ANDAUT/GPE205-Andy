using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIController : Controller
{

    //An enum organizing what states the AI can be in
    public enum AIState { Guard, Scan, Chase, Attack, Patrol, ReturnHome, ChooseTarget};

    //The current state
    public AIState currentState;

    //Float for the time the AI's state has changed
    private float lastStateChangeTime;

    //Declares the target
    public GameObject target;

    //Flee distance float
    public float fleeDistance;

    //Waypoint enum
    public Transform[] waypoints;

    //Waypoint stop distance
    public float waypointStopDistance;

    //Current waypoint
    private int currentWaypoint = 0;

    //Patrol loop boolean
    public bool isLooping;

    //This unit's hearing distance
    public float hearingDistance;

    //This unit's field of view (FoV)
    public float fieldOfView;






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
        TargetPlayerOne();

        //Debug.Log("Thinking...");
        switch (currentState)
        {
            case AIState.Guard:
                DoIdleState();
                currentWaypoint = 0;

                if (IsDistanceLessThan(target, 15))
                {
                    ChangeState(AIState.Chase);
                }
                break;
            case AIState.Patrol:
                //Does work
                Patrol();
                //Transition check
                if (IsDistanceLessThan(target, 15))
                {
                    ChangeState(AIState.Chase);
                }
                break;

            case AIState.Chase:
                //Does work
                DoChaseState();
                //Transition check
                if (IsDistanceLessThan(target, 12))
                {
                    ChangeState(AIState.Attack);
                } else if (!IsDistanceLessThan(target, 15))
                {
                    ChangeState(AIState.Patrol);
                }
                break;

            case AIState.Attack:

                DoAttackState();

                if(!IsDistanceLessThan(target, 12))
                {
                    ChangeState(AIState.Chase);
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

    protected void PatrolState()
    {
        Patrol();
    }

    protected void Patrol()
    {
        //This detects if we have enough waypoints in our list to move to a current waypoint
        if (waypoints.Length > currentWaypoint)
        {
            //Then we seek that waypoint
            Seek(waypoints[currentWaypoint]);
            //If we're close enough, this increments to the next waypoint
            if (Vector3.Distance(pawn.transform.position, waypoints[currentWaypoint].position) < waypointStopDistance)
            {
                currentWaypoint++;
            }
        } else
        {
            RestartPatrol();
        }
        }
    protected void RestartPatrol()
    {
        //This sets the index to 0
        if (isLooping == true)
        {
            currentWaypoint = 0;
        } else
        {
            ChangeState(AIState.Guard);
        }


    }

    public void TargetPlayerOne()
    {
        //This checks if the gamemanager exists
        if(GameManager.instance.players != null)
        {
            //Checks if there's players in said manager
            if(GameManager.instance.players.Count > 0)
            {
                //Targets the gameobject of the pawn of the first player controller in the list
                target = GameManager.instance.players[0].pawn.gameObject;
            } else
            {
                //Debug.Log("No target");
            }
        }
    }

    protected bool IHaveTarget()
    {
        //this returns true if we have a target, false if we don't
        return (target != null);
    }

    protected void TargetNearestTank()
    {
        //Gets a list of all tanks
        Pawn[] allTanks = FindObjectsOfType<Pawn>();

        //This assumes that the first tank is closest
        Pawn closestTank = allTanks[0];
        float closestTankDistance = Vector3.Distance(pawn.transform.position, closestTank.transform.position);

        //This iterates through them one at a time
        foreach(Pawn tank in allTanks)
        {
            //If this one is closer than the closest
            if(Vector3.Distance(pawn.transform.position, tank.transform.position) <= closestTankDistance)
            {
                //Makes sure this is the closest
                closestTank = tank;
                closestTankDistance = Vector3.Distance(pawn.transform.position, closestTank.transform.position);
            }
        }

        //Targets closest tank
        target = closestTank.gameObject;
    }

    public bool CanHear(GameObject target)
    {
        //This gets the target's NoiseMaker component
        NoiseMaker noiseMaker = target.GetComponent<NoiseMaker>();

        //If the target lacks a noisemaker, they can't make noise, so this returns false
        if (noiseMaker == null)
        {
            return false;
        }

        //If the target's making 0 noise, they also can't be heard, so return false too
        if (noiseMaker.VolumeDistance <= 0)
        {
            return false;
        }

        //If the target is making noise, add the volumeDistance in the noisemaker to the hearingDistance of this AI
        float totalDistance = noiseMaker.VolumeDistance + hearingDistance;

        //If the distance between our pawn and target is closer than this distance...
        if(Vector3.Distance(pawn.transform.position, target.transform.position) <= totalDistance)
        {
            //Then the target is heard!
            return true;
        }
        else
        {
            //Otherwise, this unit is too far from the target to hear them
            return false;
        }
    }

    public bool CanSee(GameObject target)
    {

        //Rays
        //RaycastHit hit;
        Ray visionRay = new Ray(transform.position, Vector3.down);

        //This finds the vector from this unit to the target
        Vector3 agentToTargetVector = target.transform.position - transform.position;

        //This finds the angle between the direction this unit is facing (forward in local space) and the veector to the target.

        float angleToTarget = Vector3.Angle(agentToTargetVector, pawn.transform.forward);

        //If that angle is less than our field of view...
        if (angleToTarget < fieldOfView)
        {
            return true;
        } else
        {
            return false;
        }
    }
}
