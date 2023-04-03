using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Controller : MonoBehaviour
{
    //This holds our pawn
    public Pawn pawn;

    // Start is called before the first frame update
    public virtual void Start()
    {
        //Empty for now!
    }

    // Update is called once per frame
    public virtual void Update()
    {
        //Also empty for now lol
    }

    //Abstract means that any child must override it
    public abstract void DetectInput();
}
