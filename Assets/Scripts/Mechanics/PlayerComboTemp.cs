using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

/*
* Temporary Script to test out player combo mechanics
*/

public class PlayerComboTemp : MonoBehaviour
{
    #region inspector variables
    [SerializeField] [Tooltip("The the GameObject with trigger collider of melee hitbox.")]
    private GameObject meleeHitBoxGameObject;
    [SerializeField] [Tooltip("The amount of knock back applied on the third hit.")]
    private float knockBack = 0;
    [SerializeField] [Tooltip("The amount of time to wait before the next hit.")]
    private float comboResetCoolDown = 0;
    [SerializeField] [Tooltip("The amount of time to wait before the next hit.")]
    private float[] coolDown = new float[3];
    #endregion

    #region non inspector variables
    private int comboNum = 0;
    private float resetCoolDownCount = 0;
    private float attackCoolDownCount = 0;
    private Collider2D meleeHitBox;
    private bool isAttacking = false;
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        meleeHitBox = meleeHitBoxGameObject.GetComponent<Collider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void DealDamage(Enemy enemey, float damage)
    /// <summary>
    /// Subtract damage from enemey.hp
    /// </summary>
    {
        // this might be better if we do it in enemy?
    }

    void FistCombo(Collider2D other)
    /// <summary>
    /// Will alternate between 0,1,2
    /// </summary>
    {
        comboNum = (comboNum + 1) % 3;
        // Debug.Log("combo number: " + comboNum);
        if (comboNum == 2) 
        { // apply knockback
            Vector2 knockbackForce = new Vector2(knockBack * 100 * this.transform.localScale.x, 0);
            Rigidbody2D otherRb = other.gameObject.GetComponent<Rigidbody2D>();
            otherRb.AddForce(knockbackForce);
        }
        DealDamage(null, 10f); // placeholder
    }

    void OnMelee(InputValue movementValue)
    {
        float val = movementValue.Get<float>();
        if (val == 1) 
        {
            isAttacking = true;
        }

    }

    void OnTriggerStay2D(Collider2D other) 
    {
        /* todo:
        * check that the other is an enemy (via tag maybe)
        * call function to make enemy lose hp
        * might need to make a list of all colliders if we want to hit everything
        */

        if (other.gameObject != this.gameObject) { //might want to use enemy tag
            if (isAttacking) 
            {
                Debug.Log(other.gameObject);

                FistCombo(other);
                isAttacking = false;
            }
        }
    }

}
