using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGravityControl : PlayerInputKey
{
    public int count = 4;
    public PlayerGravityControl(Player _player, PlayerControl _control) : base(_player, _control)
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
        KeyDown();
    } 
    void KeyDown(){
        if(Input.GetKeyUp(KeyCode.DownArrow)){
            count = 4;
            Changecontrol();
        }else if(Input.GetKeyUp(KeyCode.RightArrow)){
            count = 1;
            Changecontrol();
        }else if(Input.GetKeyUp(KeyCode.UpArrow)){
            count = 2;
            Changecontrol();
        }else if(Input.GetKeyUp(KeyCode.LeftArrow)){
            count = 3;
            Changecontrol();
        }   
    }
    void Changecontrol(){
        if(!player.IsGroundDetected()){
            control.ChangeState(player.moveState);
        }
        if(player.IsGroundDetected()){
            control.ChangeState(player.idleState);
        }
    }
}
