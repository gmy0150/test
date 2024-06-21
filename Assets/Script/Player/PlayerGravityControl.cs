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
        // count++;
        // if(count > 4){
        //     count = 1;
        // }
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
            if(!player.IsGroundDetected()){
                control.ChangeState(player.moveState);
            }
            if(player.IsGroundDetected()){
                control.ChangeState(player.idleState);
            }
        }else if(Input.GetKeyUp(KeyCode.RightArrow)){
            count = 1;
            if(!player.IsGroundDetected()){
                control.ChangeState(player.moveState);
            }
            if(player.IsGroundDetected()){
                control.ChangeState(player.idleState);
            }
        }else if(Input.GetKeyUp(KeyCode.UpArrow)){
            count = 2;
            if(!player.IsGroundDetected()){
                control.ChangeState(player.moveState);
            }
            if(player.IsGroundDetected()){
                control.ChangeState(player.idleState);
            }

        }else if(Input.GetKeyUp(KeyCode.LeftArrow)){
            count = 3;
            if(!player.IsGroundDetected()){
                control.ChangeState(player.moveState);
            }
            if(player.IsGroundDetected()){
                control.ChangeState(player.idleState);
            }
        }
        
    }
}
