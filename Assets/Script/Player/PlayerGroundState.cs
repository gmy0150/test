using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGroundState : PlayerInputKey
{
    public int count{get;private set;}
    GameObject saveCube;

    public PlayerGroundState(Player _player, PlayerControl _control) : base(_player, _control)
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
        if(Input.GetButtonDown("Vertical")&&player.IsGroundDetected()&&MapManager.Instance.GravityRoom){
            Debug.Log("작동안해?");
            control.ChangeState(player.gravityState);
        }
        if(Input.GetButtonDown("Vertical")&&player.IsGroundDetected()&&!MapManager.Instance.GravityRoom){
            control.ChangeState(player.jumpState);
            Debug.Log("작동안해?");
        }

        if(Input.GetKeyDown(KeyCode.Q)){
            control.ChangeState(player.freezeState);
        }

        if(xInput == 0 && rb.velocity.y == 0){
            control.ChangeState(player.idleState);
        }
        player.CheckCube();
    }

}
