using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    #region Enemy Status
    [SerializeField] protected int maxHp;
    [SerializeField] protected int dmgOnTouch;
    [SerializeField] protected ItemPickup[] drops;
    int hp;
    #endregion

    #region Enemy Movement
    protected Vector3 moveVelocity;
    protected float moveSpeedMultiplier;
    #endregion

    #region Unity
    protected Rigidbody2D rb;
    #endregion

    // Start is called before the first frame update
    public virtual void Start()
    {
        hp = maxHp;

        moveVelocity = Vector3.zero;
        moveSpeedMultiplier = 1f;

        rb = GetComponent<Rigidbody2D>();
    }

    public virtual void FixedUpdate()
    {
        UpdateVelocity();
    }

    public virtual void TakeDmg(int dmg)
    {
        hp -= dmg;
        if (dmg <= 0)
        {
            Die();
        }
    }

    protected virtual void Die()
    {
        foreach (ItemPickup drop in drops)
        {
            Instantiate(drop, gameObject.transform.position, Quaternion.identity);
        }
        Destroy(gameObject);
    }

    protected virtual void UpdateVelocity()
    {
        Vector3 currVelocity = rb.velocity;
        rb.velocity = moveVelocity * moveSpeedMultiplier;
    }

    protected virtual void OnCollisionEnter2D(Collision2D col)
    {
        Player player = col.gameObject.GetComponent<Player>();
        if (player != null)
        {
            if (dmgOnTouch > 0)
            {
                player.TakeDmg(dmgOnTouch);
            }
        }
    }
}
