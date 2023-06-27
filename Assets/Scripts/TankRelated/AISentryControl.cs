using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AISentryControl : AIController
{
    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();
        ChangeState(AIState.Scan);
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
                case AIState.Scan:
                    DoScanState();

                    if (IsDistanceLessThan(target, 15))
                    {
                        ChangeState(AIState.Attack);
                    }
                    else if (target == null)
                    {
                        DoIdleState();
                    }
                    break;
                case AIState.Attack:
                    //Does work
                    DoAttackState();
                    //Transition check
                    if (IsDistanceLessThan(target, 20))
                    {
                        ChangeState(AIState.Scan);
                    }
                    break;
                case AIState.Dead:

                    break;

            }
        }
    }

    private void DoScanState()
    {
        pawn.transform.Rotate(0, .1f, 0);
    }
}
