using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AISniperControl : AIController
{
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

        if (pawn == null)
        {
            ChangeState(AIState.Dead);
        }
    }

    public override void MakeDecisions()
    {
        if (pawn != null)
        {


            TargetPlayerOne();

            //Debug.Log("Thinking...");
            switch (currentState)
            {
                case AIState.Guard:
                    DoIdleState();
                    currentWaypoint = 0;

                    if (IsDistanceLessThan(target, 30) && target != null)
                    {
                        ChangeState(AIState.Attack);
                    }
                    else if (target == null)
                    {
                        DoIdleState();
                    }
                    break;

                case AIState.Patrol:
                    //Does work
                    Patrol();
                    //Transition check
                    if (IsDistanceLessThan(target, 30))
                    {
                        ChangeState(AIState.Attack);
                    }
                    break;

                case AIState.Attack:

                    DoAttackState();

                    if (IsDistanceLessThan(target, 15) && pawn != null)
                    {
                        ChangeState(AIState.Flee);
                    }
                    break;

                case AIState.Flee:

                    DoFleeState();

                    if(!IsDistanceLessThan(target, 20) && pawn != null)
                    {
                        ChangeState(AIState.Patrol);
                    }
                    break;
                    
                case AIState.Dead:

                    break;

            }
        }
    }

    protected override void DoAttackState()
    {
        if (pawn != null)
        {
            Seek(target);
            //Fire
            Shoot();
        }
        else if (pawn == null)
        {
            ChangeState(AIState.Scan);
        }

    }
}
