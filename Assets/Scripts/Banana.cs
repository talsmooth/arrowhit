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

    public float x4;

    public float y4;

    public float x5;

    public float y5;

    public float x6;

    public float y6;

    Vector3 pos;

    float ran;

    int state;

    int count;

    public Vector3 rotate;

    public GameObject cube;

    // Use this for initialization
    void Start () 
    {
        AudioManager._banana.Play();
        rb.velocity = new Vector3(x, y, 0);
        ran = Random.Range(3.22f, 4.62f);
        rb.AddTorque(rotate);
    }

    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            Time.timeScale = 1;
        }
        if (Time.timeScale > 0)
        {
            Instantiate(cube, transform.position, Quaternion.identity);
        }
    }
    // Update is called once per frame
    void FixedUpdate () 
    {
        if (pos.x < -14)
        {
            AudioManager._banana.Stop();
            Destroy(gameObject);
        }

        pos = transform.position;

        if (pos.x > ran && state == 0)
        {
            state = 1;
            Debug.Log(state);
        }

        else if (rb.velocity.x < 1.2f && state == 1)
        {
            //Time.timeScale = 0;
            state = 2;
            Debug.Log(state);
        }

        else if (state == 2 && count > 5)
        {
            //Time.timeScale = 0;
            rb.velocity = new Vector3 (-rb.velocity.x, rb.velocity.y, 0);
            state = 3;
            Debug.Log(state);
        }

        else if (state == 3 && count > 10) 
        {
            //Time.timeScale = 0;
            state = 4;
            Debug.Log(state);
        }

        else if (state == 4 && rb.velocity.x < -4)
        {
            //Time.timeScale = 0;
            state = 5;
            Debug.Log(state);
        }

        else if (state == 5 && rb.velocity.x < -8)
        {
            //Time.timeScale = 0;
            state = 6;
            Debug.Log(state);
        }

        switch (state)
        {
            case 0:

                rb.velocity = new Vector3(rb.velocity.x * x0, rb.velocity.y * y0, 0);
                break;

            case 1:
                rb.velocity = new Vector3(rb.velocity.x * x1, rb.velocity.y * y1, 0);
                break;

            case 2:
                count ++;
                rb.velocity = new Vector3(rb.velocity.x * x2, rb.velocity.y * y2, 0);
                break;

            case 3:
                count ++;
                rb.velocity = new Vector3(rb.velocity.x * x3, rb.velocity.y * y3, 0);
                break;

            case 4:
                rb.velocity = new Vector3(rb.velocity.x * x4, rb.velocity.y * y4, 0);
                break;

            case 5:
                rb.velocity = new Vector3(rb.velocity.x * x5, rb.velocity.y * y5, 0);
                break;

            case 6:
                rb.velocity = new Vector3(rb.velocity.x * x6, rb.velocity.y * y6, 0);
                break;
        }

    }
}
