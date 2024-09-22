using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAirState : PlayerInputKey
{
    public PlayerAirState(Player _player, PlayerControl _control) : base(_player, _control)
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

        rb.velocity = new Vector2(xInput * player.moveSpeed, rb.velocity.y);
        
    }
}
