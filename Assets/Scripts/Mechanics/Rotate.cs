using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotate : MonoBehaviour
{
    public float rotationSpeed; 
    // Start is called before the first frame update
    void Start()
    {
        rotationSpeed = -20;
    }

    // Update is called once per frame
    void Update()
    {
        transform.rotation = Quaternion.identity;
        // transform.Rotate(new Vector3(0, 0, rotationSpeed) * Time.deltaTime);
    }
}
