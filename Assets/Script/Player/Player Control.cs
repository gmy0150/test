using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    public PlayerInputKey currentState{get;private set;}
    public PlayerInputKey tempstate{get;private set; }
    public void Initalize(PlayerInputKey _startState){
        currentState = _startState;
        tempstate = _startState;    
        currentState.Enter();
    }
    public void ChangeState(PlayerInputKey _newState){
        tempstate  = currentState;
        currentState.Exit();
        currentState = _newState;
        currentState.Enter();
    }
    
    public string GetState()
    {
        return tempstate.ToString(); 
    }
}
