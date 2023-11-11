using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : MonoBehaviour
{
    public Transform enemy;
    public Transform target;
    
    
    // Update is called once per frame

    void Update()
    {
        transform.right = target.position - transform.position;
        
        
    }
}
