using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageOnHit : MonoBehaviour
{

    public float damageDone;
    public Pawn owner;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnTriggerEnter(Collider other)
    {
        //This looks for the 'Health' component on anything this collides with
        Health otherHealth = other.gameObject.GetComponent<Health>();
        //GetComponent seems to look for specific values in an object even if they are not declared here(?)

        if (otherHealth != null)
        {
            //This deals damage
            otherHealth.TakeDamage(damageDone, owner);
        }

        //On contact with anything, this object will destroy itself
        Destroy(gameObject);
    }
}
