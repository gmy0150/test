using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerIdleState : PlayerGroundState
{
    public PlayerIdleState(Player _player, PlayerControl _control) : base(_player, _control)
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
        if(xInput != 0 && control.currentState != player.jumpState){
            control.ChangeState(player.moveState);
        }
        
    }
}
