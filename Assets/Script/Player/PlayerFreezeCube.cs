using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFreezeCube : PlayerInputKey
{
    public static bool cubes;
    static GameObject saveCube;
    public PlayerFreezeCube(Player _player, PlayerControl _control) : base(_player, _control)
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
        if(!cubes){
            saveCube = player.CheckCube();
            if(saveCube!= null){
                FreezeCube(saveCube);
            }
        }
        else {
            UnFreezeCube();
        }
            control.ChangeState(player.idleState);
    }
    void FreezeCube(GameObject cube){
        Rigidbody2D rigid = cube.GetComponent<Rigidbody2D>();
        if(rigid != null){
            rigid.constraints = RigidbodyConstraints2D.FreezePosition;
            cubes = true;
        }
    }
    void UnFreezeCube(){
        Rigidbody2D rigid = saveCube.GetComponent<Rigidbody2D>();
        if(rigid != null){
            rigid.constraints = RigidbodyConstraints2D.None;
            cubes = false;
        }
    }
}
