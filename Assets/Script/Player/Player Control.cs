using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    public PlayerInputKey currentState{get;private set;}
    public void Initalize(PlayerInputKey _startState){
        currentState = _startState;
        currentState.Enter();
    }
    public void ChangeState(PlayerInputKey _newState){
        currentState.Exit();
        currentState = _newState;
        currentState.Enter();
    }
}
