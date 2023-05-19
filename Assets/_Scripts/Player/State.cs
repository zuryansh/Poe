using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public abstract class State 
{
    protected Player player;

    public State(Player _system)
    {
        player = _system;
    }

    public virtual void Start(State previousState)
    {
        return;
    }
    public virtual void Update()
    {
        return;
    }
    public virtual void FixedUpdate()
    {
        return;
    }
    public virtual void Disable()
    {
        return;
    }

}
