using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickup : MonoBehaviour
{
    [SerializeField] Item item;
    void OnCollisionEnter2D(Collision2D col) 
    {
        Player Play = col.gameObject.GetComponent<Player>();
        if (Play != null) {
            Play.AddItem(item);
            Destroy(this.gameObject);
        }
    }
}
