using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    [SerializeField] Item itemIn;
    // Start is called before the first frame update
    void Start()
    {
    }
    void OnCollisionEnter2D(Collision2D col) {
        Player Play = col.otherCollider.GetComponent<Player>();
        Debug.Log("Hit");
        if (Play != null) {
            Play.AddItem(itemIn);
            Destroy(col.gameObject);
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
