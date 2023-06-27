using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIPatrolControl : AIController
{
    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();
        ChangeState(AIState.Patrol);
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

                    if (IsDistanceLessThan(target, 15) && target != null)
                    {
                        ChangeState(AIState.Chase);
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
                    }
                    else if (!IsDistanceLessThan(target, 15))
                    {
                        ChangeState(AIState.Patrol);
                    }
                    break;

                case AIState.Attack:

                    DoAttackState();

                    if (!IsDistanceLessThan(target, 12) && pawn != null)
                    {
                        ChangeState(AIState.Chase);
                    }
                    break;
                case AIState.Dead:

                    break;

            }
        }
    }
}
