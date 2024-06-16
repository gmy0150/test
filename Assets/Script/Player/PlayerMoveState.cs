using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoveState : PlayerGroundState
{
    public PlayerMoveState(Player _player, PlayerControl _control) : base(_player, _control)
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
        move();
        // Debug.Log(yInput);
        if(xInput == 0){
            control.ChangeState(player.idleState);
        }
    } 
    public void move(){
        if(player.gravityState.count == 0){
            player.SetVelocity(xInput * player.moveSpeed, yvelocity);
        }
        else if(player.gravityState.count == 1){
            player.SetVelocity(xvelocity , xInput * player.moveSpeed);
        }
        else if(player.gravityState.count == 2){
            player.SetVelocity(-xInput * player.moveSpeed, yvelocity);
        }
        else if(player.gravityState.count == 3){
            player.SetVelocity(xvelocity,-xInput * player.moveSpeed);
        }
        else if(player.gravityState.count == 4){
            player.SetVelocity(xInput * player.moveSpeed, yvelocity);
        }
    }
}
