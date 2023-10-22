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
    int hp;
    #endregion
    #region Player Movement
    [SerializeField] protected float runSpeed;
    [SerializeField] protected float maxJumpTime;
    [SerializeField] protected float initialJumpVelocity;
    [SerializeField] protected float fallSpeedMultiplier;
    private Vector2 movementDirection;
    float currentJumpVelocity;
    float movementVertical;
    float movement;
    bool jumpIsHeld;
    bool canJump;
    bool isDashing;
    bool canDash; 
    private float dashCooldown = 0.5f; 
    [SerializeField] protected float dashSpeed = 25f; 
    [SerializeField] protected float dashTime = 1f; 
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


        rb = GetComponent<Rigidbody2D>();
        movement = 0f;
        jumpIsHeld = false;
        canJump = false;
        canDash = true; 
        isDashing = false;
    }

    void FixedUpdate()
    {
        UpdateVelocity();

    }

    void OnMove(InputValue movementValue)
    {
        movement = movementValue.Get<float>();
        movementDirection = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
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
        Debug.Log("calling Dash");
        float val = movementValue.Get<float>();
        // Dash was pressed down
        if (val == 1)
        {
            if (canDash) 
            {
                canDash = false;
                isDashing = true; 
                StartCoroutine(dashCooldownFeature());
            }
          
        }
    }

    IEnumerator dashCooldownFeature() 
    {
            yield return new WaitForSeconds(dashCooldown);
            canDash = true; 
            isDashing = false; 
    }

    void UpdateVelocity()
    {
        Vector3 currVelocity = rb.velocity;

        float newHorizontalVelocity = movement * runSpeed;

        float newVerticalVelocity = rb.velocity.y;

        if (isDashing && movement != 0) 
        {
            newHorizontalVelocity = dashSpeed * movement; 
            newVerticalVelocity = dashSpeed * movementDirection.y;   
        }

        else if (jumpIsHeld && currentJumpVelocity > 0f)
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
}
