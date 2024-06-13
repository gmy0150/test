using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInputKey
{
    protected Rigidbody2D rb;
    protected Player player;
    protected PlayerControl control;
    protected float xInput;
    protected float yvelocity;
    protected float xvelocity;


    public PlayerInputKey(Player _player, PlayerControl _control){
        this.player = _player;
        this.control = _control;
    }
    public virtual void Enter()
    {
        rb = player.rigid;

    }

    // Update is called once per frame
    public virtual void Update()
    {
        xInput = Input.GetAxisRaw("Horizontal");
        xvelocity = rb.velocity.x;
        yvelocity = rb.velocity.y;
    }
    public virtual void Exit()
    {
    }
}
