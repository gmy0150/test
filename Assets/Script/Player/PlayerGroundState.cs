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
        if(Input.GetKeyDown(KeyCode.LeftArrow)&&player.IsGroundDetected()){
            control.ChangeState(player.gravityState);
        }
        if(Input.GetKeyDown(KeyCode.UpArrow)&&player.IsGroundDetected()){
            control.ChangeState(player.gravityState);
        }
        if(Input.GetKeyDown(KeyCode.DownArrow)&&player.IsGroundDetected()){
            control.ChangeState(player.gravityState);
        }
        if(Input.GetKeyDown(KeyCode.RightArrow)&&player.IsGroundDetected()){
            control.ChangeState(player.gravityState);
        }
        if(Input.GetKeyDown(KeyCode.Q)){
            control.ChangeState(player.freezeState);
        }
        
        player.CheckCube();
    }
    
    // public void MoveCollsion(){
    //     if(player.isCube()){
    //         saveCube = player.CheckCube();
    //         DontmoveCube(saveCube);
    //     }
    //     else{
    //         if(saveCube != null)
    //             MoveCube();
    //     } 
    // }
    // void DontmoveCube(GameObject cube){
    //     Rigidbody2D rigid = cube.GetComponent<Rigidbody2D>();
    //     if(rigid != null){
    //         rigid.isKinematic = true;
    //     }
    // }
    void MoveCube(){
        Rigidbody2D rigid = saveCube.GetComponent<Rigidbody2D>();
        if(rigid != null){
            rigid.isKinematic = false;
        }
    }
}
