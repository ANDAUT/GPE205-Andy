using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Powerup
{
    public abstract void Apply(PowerupManager target);
    public abstract void Remove(PowerupManager target);

    public float duration;
    public bool isPermanent;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
