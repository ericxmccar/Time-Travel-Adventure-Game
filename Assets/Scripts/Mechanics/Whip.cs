using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Whip : MonoBehaviour
{
    public Camera mainCamera;
    public LineRenderer _lineRenderer;
    public DistanceJoint2D _Distancejoint;
    public Rigidbody2D rb;
    public float force;
    public Vector3 MouseDir;
    public Transform LinePosition;
    public bool isGrappling;
    public Transform lookToHook;
    public LayerMask whatisGrappleable; 
    public GameObject Hook; 

    // Start is called before the first frame update
    void Start()
    {
        isGrappling = true;
        _Distancejoint.autoConfigureDistance =true;
        _Distancejoint.enabled = false;
        _lineRenderer.enabled = false;
       
    }


    // Update is called once per frame
    void Update()
    {
      MouseDir = mainCamera.ScreenToWorldPoint(Input.mousePosition);
       
     if (isGrappling == true)
     {
     
        if(Input.GetKeyDown(KeyCode.Mouse0))
        {
           

            
           Vector2 mousepos = (Vector2)mainCamera.ScreenToWorldPoint(Input.mousePosition);
           
            _lineRenderer.SetPosition(0,mousepos);
       
            _lineRenderer.SetPosition(1,transform.position);
            _Distancejoint.connectedAnchor = mousepos; 
            
            var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit) && (hit.transform.name == "Hook")) 
            {
                    _Distancejoint.enabled = true;
                    LinePosition.position = mousepos;
            } 
             
               
        }
        if (Input.GetKey(KeyCode.Mouse0))
        {
           var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit) && (hit.transform.name == "Hook")) 
            {
                    _lineRenderer.enabled = true;
                    _lineRenderer.SetPosition(1,transform.position);
            } 
           
        
               
        } else if ( Input.GetKeyUp(KeyCode.Mouse0))
        {
            _Distancejoint.enabled = false;
         
            _lineRenderer.enabled = false;
        }
        if (_Distancejoint.enabled)
        {
            _lineRenderer.SetPosition(1,transform.position);
        }
        if (Input.GetKey(KeyCode.E) && Input.GetKey(KeyCode.Mouse0))
        {      
             
            Vector3 Direction = LinePosition.position - transform.position;
           
            rb.velocity = new Vector2(Direction.x * force, Direction.y * force).normalized * force *Time.deltaTime;
             _Distancejoint.enabled = false;
        }
        if (Input.GetKeyUp(KeyCode.E) && Input.GetKey(KeyCode.Mouse0))
        {
            var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit) && (hit.transform.name == "Hook")) 
            {
                    _Distancejoint.enabled = true;
            }
        }
             
    }
    }  
}
