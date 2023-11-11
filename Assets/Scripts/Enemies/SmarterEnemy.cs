using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmarterEnemy : Enemy
{
    [SerializeField] float start;
    [SerializeField] float end;
    [SerializeField] float ActivationDistance;
    private GameObject player;
    public override void Start()
    {
        base.Start();
        moveVelocity = Vector3.left;
        player = GameObject.FindWithTag("Player");
    }
    void FixedUpdate() {
        base.UpdateVelocity();
        if (Mathf.Abs(player.transform.position.x - this.transform.position.x) < ActivationDistance) 
        {
            
            if (player.transform.position.x < this.transform.position.x) 
            {
                moveVelocity = Vector3.left;
            }
            if (player.transform.position.x > this.transform.position.x) 
            {
                moveVelocity = Vector3.right;
            }
        }
        else
        {
            if (this.transform.position.x <= start) 
            {
                moveVelocity = Vector3.right;
            }
            if (this.transform.position.x >= end) 
            {
                moveVelocity = Vector3.left;
            }
        }
    }
}
