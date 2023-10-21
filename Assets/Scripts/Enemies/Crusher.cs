using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crusher : Enemy
{
    [SerializeField] float FallingVelocity;
    [SerializeField] float LiftingVelocity;
    [SerializeField] float ActivationDistance;
    [SerializeField] float Seconds;
    private float position;
    private GameObject player;
    private bool goingUp; 

    public override void Start()
    {   
        base.Start();
        position = this.transform.position.y;
        player = GameObject.FindWithTag("Player");
        goingUp = false;
    }
    public override void FixedUpdate()
    {
        if (this.transform.position.y > 2.45 && goingUp) 
        {
            moveVelocity = new Vector3(0,0,0);
            goingUp = false;
        }
        if (Mathf.Abs(player.transform.position.x - this.transform.position.x) < ActivationDistance && !goingUp) 
        {
            float neg = -1 * FallingVelocity;
            moveVelocity = Vector3.down * FallingVelocity;
        }
        if (this.transform.position.y < -1.2) {
            Invoke("Up", 2.0f);
        }

        base.FixedUpdate();
    }
    public void Up() 
    {
    goingUp = true;
    moveVelocity = Vector3.up * LiftingVelocity;
    }
    IEnumerator WaitCoroutine() {
        yield return new WaitForSeconds(Seconds);
    }
}
