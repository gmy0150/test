using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJumpState : PlayerInputKey
{
    public PlayerJumpState(Player _player, PlayerControl _control) : base(_player, _control)
    {
    }
    public override void Enter()
    {
        base.Enter();
        rb.velocity = new Vector2(rb.velocity.x, player.jumpforce);
    }
    public override void Exit()
    {
        base.Exit();
    }
    public override void Update()
    {
        base.Update();
            // rb.velocity = new Vector2(xInput * player.moveSpeed, rb.velocity.y);
        if(rb.velocity.y == 0){
            control.ChangeState(player.idleState);
        }
    }
}
