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

        if(xInput == 0){
            control.ChangeState(player.idleState);
        }
        if(Input.GetButtonDown("Vertical")&&player.IsGroundDetected()&&!MapManager.Instance.GravityRoom){
            control.ChangeState(player.jumpState);
        }
    } 
    public void move(){
        if(player.gravityState.count == 1){//오른쪽
            player.SetVelocity(xvelocity , xInput * player.moveSpeed);
        }
        else if(player.gravityState.count == 2){//위
            player.SetVelocity(-xInput * player.moveSpeed, yvelocity);
        }
        else if(player.gravityState.count == -1){//왼쪽
            player.SetVelocity(xvelocity,-xInput * player.moveSpeed);
        }
        else if(player.gravityState.count == -2){//아래
            player.SetVelocity(xInput * player.moveSpeed, yvelocity);
        }
    }

}
