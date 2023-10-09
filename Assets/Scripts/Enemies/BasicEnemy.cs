using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicEnemy : Enemy
{
    public override void Start()
    {
        base.Start();

        moveVelocity = Vector3.left;
    }
}
