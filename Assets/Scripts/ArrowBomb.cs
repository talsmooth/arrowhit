using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowBomb : MonoBehaviour {

    public bool explode;

    public GameObject arrowLauncher;

    public Rigidbody rb;

    public GameObject particle;

    Renderer rend;

    Collider col;



	// Use this for initialization
	void Start () 
    {
        rb.AddTorque(new Vector3 (0,0,500));
	}
	
	// Update is called once per frame
	void FixedUpdate () 
    {
        if (explode)
        {
            arrowLauncher = Instantiate(arrowLauncher, transform.position, Quaternion.identity);    
              
            arrowLauncher.GetComponent<ArrowLauncher>().bomb = gameObject;

            rend = gameObject.GetComponent<MeshRenderer>();

            rend.enabled = false;

            col = gameObject.GetComponent<Collider>();

            col.enabled = false;

            explode = false;

        }
	}
        
}
