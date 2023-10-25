using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class RotatingPlatform : MonoBehaviour
{
    public float rotationSpeed; 
    public GameObject pivotObject; 
    // Start is called before the first frame update
    void Start()
    {
        rotationSpeed = 20;
    }

    // Update is called once per frame
    void Update()
    {
        transform.RotateAround(pivotObject.transform.position, new Vector3(0, 0, 1), rotationSpeed * Time.deltaTime);
    }
}
