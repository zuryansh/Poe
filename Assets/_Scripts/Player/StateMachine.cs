using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public abstract class StateMachine : MonoBehaviour
{
    [SerializeField] protected State state;

    public void SetState(State newState)
    {
        State previousState = state;
        if (previousState != null)
        {
            state.Disable(); // call the previous states disable function
        }
        
        state = newState;   // set the state
        state.Start(previousState); // call the new states start function and tell it the previous state
    }
}
