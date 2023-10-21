using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlowdownField : MonoBehaviour
{
    [SerializeField] float slowdownFactor;

    void OnTriggerEnter2D(Collider2D col)
    {
        TimeChangeable timechange = col.gameObject.GetComponent<TimeChangeable>();
        if (timechange != null) {
            timechange.timeMultiplier = slowdownFactor;
        }
    }

    void OnTriggerExit2D(Collider2D col) 
    {
        TimeChangeable timechange = col.gameObject.GetComponent<TimeChangeable>();
        if (timechange != null) {
            timechange.timeMultiplier = 1f;
        }
    }

    // // Start is called before the first frame update
    // void Start()
    // {
        
    // }

    // // Update is called once per frame
    // void Update()
    // {
        
    // }
}
