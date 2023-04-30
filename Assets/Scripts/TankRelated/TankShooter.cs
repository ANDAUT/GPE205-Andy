using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankShooter : Shooter
{

    public Transform firePointTransform;

    // Start is called before the first frame update
    public override void Start()
    {
        
    }

    // Update is called once per frame
    public override void Update()
    {
        
    }

    public override void Shoot(GameObject Shell, float bulletForce, float damageDone, float duration)
    {
        //This instantiates the shell (Bullet)
        GameObject newShell = Instantiate(Shell, firePointTransform.position, firePointTransform.rotation) as GameObject;

        //DamageOnHit
        DamageOnHit doh = newShell.GetComponent<DamageOnHit>();

        //If DamageOnHit is found
        if (doh != null)
        {
            //DamageDone is set to the value passed here
            doh.damageDone = damageDone;

            //This sets the owner of this fired shell to the source pawn, if there is one
            doh.owner = GetComponent<Pawn>();
        }

        //RigidBody component
        Rigidbody rb = newShell.GetComponent<Rigidbody>();

        //If rigidbody is found
        if (rb != null)
        {
            //This adds force to make the shell move forward (Using the bulletForce float for power)
            rb.AddForce(firePointTransform.forward * bulletForce);
        }

        //This destroys the shell after a set time; Duration = lifetime
        Destroy(newShell, duration);

    }
}