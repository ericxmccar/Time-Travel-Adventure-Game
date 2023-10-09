using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(TimeChangeable))]
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
    protected TimeChangeable timeChangeable;
    #endregion

    #region Unity
    protected Rigidbody2D rb;
    #endregion

    // Start is called before the first frame update
    public virtual void Start()
    {
        hp = maxHp;

        moveVelocity = Vector3.zero;

        rb = GetComponent<Rigidbody2D>();
        timeChangeable = GetComponent<TimeChangeable>();
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
        rb.velocity = moveVelocity * timeChangeable.timeMultiplier;
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
