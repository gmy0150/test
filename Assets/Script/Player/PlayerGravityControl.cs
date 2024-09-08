using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGravityControl : PlayerInputKey
{
    public int count = -2;
    public int savecount = 0;
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
        if(Input.GetButtonUp("Vertical")){
            count *= -1;
            Changecontrol();
        } 
    }
    public void PushButton(){
        count ++;
        if(count == 0 || count == 3){
            count -= 2;
        }
        Changecontrol();
    }
    void Changecontrol(){
        //player.gravitycontrol();
        if(!player.IsGroundDetected()){
            control.ChangeState(player.moveState);
        }
        if(player.IsGroundDetected()){
            control.ChangeState(player.idleState);
            player.rigid.velocity = Vector2.zero;
        }

    }
    public void KeepButton(){
        // savecount = count;
        // count ++;
        // if(count == 0 || count == 3){
        //     count -= 2;
        // }
        // Changecontrol();
    }
    public void ButtonOut(){
        // count = savecount;
        Changecontrol();
    }
}
