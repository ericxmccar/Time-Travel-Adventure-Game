using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    #region Player Status
    [SerializeField] protected int maxHp;
    [SerializeField] Item heldItem;
    [SerializeField] List<Item> inventory;
    [SerializeField] int hp;
    #endregion

    #region Player Movement
    [SerializeField] protected float runSpeed;
    [SerializeField] protected float maxJumpTime;
    [SerializeField] protected float initialJumpVelocity;
    [SerializeField] protected float fallSpeedMultiplier;
    float currentJumpVelocity;
    float movement;
    bool jumpIsHeld;
    bool canJump;
    protected float jumpStep; // Calculated on Start
    #endregion

    #region Unity
    protected Rigidbody2D rb;
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        inventory = new List<Item>();
        hp = maxHp;

        currentJumpVelocity = initialJumpVelocity;
        jumpStep = initialJumpVelocity / maxJumpTime;
        
        movement = 0f;
        jumpIsHeld = false;
        canJump = false;

        rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        UpdateVelocity();
    }

    void OnMove(InputValue movementValue)
    {
        movement = movementValue.Get<float>();
    }

    void OnDown(InputValue movementValue)
    {
        float val = movementValue.Get<float>();
        if (val == 1)
        {

        }
        else
        {

        }
    }

    void OnJump(InputValue movementValue)
    {
        float val = movementValue.Get<float>();
        // Jump was pressed down
        if (val == 1) 
        {
            if (canJump) 
            {
                jumpIsHeld = true;
                canJump = false;
            }
        }
        // Jump was released
        else 
        {
            jumpIsHeld = false;
        }
    }

    void OnDash(InputValue movementValue)
    {
        float val = movementValue.Get<float>();
        // Dash was pressed down
        if (val == 1)
        {
            // TODO
        }
    }

    void UpdateVelocity()
    {
        Vector3 currVelocity = rb.velocity;

        float newHorizontalVelocity = movement * runSpeed;

        float newVerticalVelocity = rb.velocity.y;
        if (jumpIsHeld && currentJumpVelocity > 0f)
        {
            newVerticalVelocity = currentJumpVelocity;
            currentJumpVelocity -= jumpStep;
            if (currentJumpVelocity < 0f) 
            {
                currentJumpVelocity = 0f;
                jumpIsHeld = false;
            }
        }
        else if (currentJumpVelocity < initialJumpVelocity)
        {
            newVerticalVelocity = -currentJumpVelocity;
            currentJumpVelocity += fallSpeedMultiplier * jumpStep;
            if (currentJumpVelocity > initialJumpVelocity)
            {
                currentJumpVelocity = initialJumpVelocity;
            }
        }
        rb.velocity = new Vector3(newHorizontalVelocity, newVerticalVelocity, currVelocity.z);
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        bool wasFalling = rb.velocity.y < 0f;
        bool collidedVertically = true;

        Vector2 firstPoint = col.GetContact(0).point;
        for (int i = 1; i < col.contactCount; i++)
        {
            Vector2 currPoint = col.GetContact(i).point;
            if (currPoint.y != firstPoint.y)
            {
                collidedVertically = false;
                break;
            }
        }

        if (wasFalling && collidedVertically)
        {
            canJump = true;
        }
    }

    public void AddItem(Item item)
    {
        inventory.Add(item);
    }
    
    public void TakeDmg(int dmg)
    {
        hp -= dmg;
        if (hp <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        // TODO
    }
}
