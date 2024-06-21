using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGroundState : PlayerInputKey
{
    public int count{get;private set;}

    public PlayerGroundState(Player _player, PlayerControl _control) : base(_player, _control)
    {
    }

     public override void Enter()
    {
        base.Enter();
    }
    public override void Exit()
    {
        base.Exit();
    }
    public override void Update()
    {
        base.Update();
        if(Input.GetKeyDown(KeyCode.LeftArrow)&&player.IsGroundDetected()){
            control.ChangeState(player.gravityState);
        }
        if(Input.GetKeyDown(KeyCode.UpArrow)&&player.IsGroundDetected()){
            control.ChangeState(player.gravityState);
        }
        if(Input.GetKeyDown(KeyCode.DownArrow)&&player.IsGroundDetected()){
            control.ChangeState(player.gravityState);
        }
        if(Input.GetKeyDown(KeyCode.RightArrow)&&player.IsGroundDetected()){
            control.ChangeState(player.gravityState);
        }

        
    }
}
