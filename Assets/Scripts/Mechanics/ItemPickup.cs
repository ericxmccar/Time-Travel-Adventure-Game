using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickup : MonoBehaviour
{
    [SerializeField] Item item;

    void OnCollisionEnter2D(Collision2D col) 
    {
        Player play = col.gameObject.GetComponent<Player>();
        if (play != null) {
            play.AddItem(item);
            Destroy(this.gameObject);
        }
    }
}
