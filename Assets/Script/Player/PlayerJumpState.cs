using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJumpState : PlayerAirState
{
    public PlayerJumpState(Player _player, PlayerControl _control) : base(_player, _control)
    {
    }

    public override void Enter()
    {
        base.Enter();
        rb.velocity = new Vector2(xvelocity, player.jumpforce);
        Debug.Log("작동해");
    }
    public override void Exit()
    {
        base.Exit();
        Debug.Log("나감?");
    }
    public override void Update()
    {
        
        rb.velocity = new Vector2(xInput * player.moveSpeed, rb.velocity.y);
        base.Update();
        Debug.Log("작동중?");
        if(rb.velocity.y == 0 ){
            control.ChangeState(player.idleState);
        }
        
    }
}