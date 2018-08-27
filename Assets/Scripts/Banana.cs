using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Banana : MonoBehaviour {

    public Rigidbody rb;

    public float x;

    public float y;

    public float x0;

    public float y0;

    public float x1;

    public float y1;

    public float x2;

    public float y2;

    public float x3;

    public float y3;

    Vector3 pos;

    float ran;

    int state;

    bool change;


	// Use this for initialization
	void Start () 
    {
        rb.velocity = new Vector3(x, y, 0);
        ran = Random.Range(5.9f, 8.1f);
        rb.AddTorque(0, 300, 1000);
	}
	
	// Update is called once per frame
	void FixedUpdate () 
    {
        pos = transform.position;

        if (pos.x > ran && state == 0)
        {
            state = 1;
        }

        else if (rb.velocity.x < 0.1f && state == 1)
        {
            state = 2;
        }

        else if (state == 2 && pos.x < ran)
        {
            state = 3;
        }
            
        switch (state)
        {
            case 0:

                //rb.velocity.x *= x0;
                //rb.velocity.y *= y0;
                break;

            case 1:
                //rb.velocity.x *= x1;
                //rb.velocity.y *= y1;
               
                break;

            case 2:
                //rb.velocity.x *= x2;
                //rb.velocity.y *= y2;
              
                break;

            case 3:
                //rb.velocity.x *= x3;
                //rb.velocity.y *= y3;  
                break;
        }

	}
}
