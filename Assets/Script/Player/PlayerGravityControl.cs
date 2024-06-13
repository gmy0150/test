using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGravityControl : PlayerInputKey
{
    public int count{get;private set;}
    public PlayerGravityControl(Player _player, PlayerControl _control) : base(_player, _control)
    {
    }

    // Start is called before the first frame update
    public override void Enter()
    {
        base.Enter();
        count++;
        if(count > 4){
            count = 1;
        }
    }
    public override void Exit()
    {
        base.Exit();
    }
    public override void Update()
    {
        base.Update();
        if(!player.IsGroundDetected()){
            control.ChangeState(player.moveState);
        }
        if(player.IsGroundDetected()){
            control.ChangeState(player.idleState);
        }
    } 
}
