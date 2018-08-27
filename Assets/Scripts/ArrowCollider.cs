using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowCollider : MonoBehaviour {

    public GameObject myFruit;

    public GameObject copy;

    public bool check1;

    public bool check2;

    float random;

    public float sliding;

    public bool slide;

    public bool align;

    public int counter;

    public float friction;

    public float frictionU;

    public float magnitude;

    public float cal;

    public float temp;

    public float delta;

    public float per;

	void Start ()
    {
        random = Random.Range(0.1f, 0.2f);
    }

    void FixedUpdate()
    {

        if (transform.position.y < -45)
        {
            Destroy(copy);
            Destroy(gameObject);
        }

        if (myFruit != null)
        {
            copy.transform.position = myFruit.transform.position;

            if (!slide)
            {
                magnitude = transform.parent.gameObject.GetComponent<Rigidbody>().velocity.magnitude;

                temp = magnitude / Game._fruitSlideSpeed;

                delta = myFruit.transform.localPosition.z - transform.localPosition.z;

                cal = delta / temp;

                per = ((cal / 4) / (Game._arrVelocityFruit / 2)) / 100;

                slide = true;
            }

        }

        if (align && slide)
        {
            alignFruit();
            if (check1 && check2)
            {
                align = false;
            }
        }
    }

    void alignFruit ()
    {
        counter++;
            
        if (counter <= cal / 4)
        {
            if (counter == 3)
            {
                transform.parent.gameObject.GetComponent<Arrow>().spl2 = true;
            }
            sliding = (delta / 2) / (cal / 4);
            friction = 1 - (((Game._arrVelocityFruit / 2) / (cal / 4)) / 100);
        }
        else if (counter <= cal / 2)
        {
           
            sliding = (delta / 4) / (cal / 4);
            friction = 1 - (((Game._arrVelocityFruit / 4) / (cal / 4)) / 100);

        }
        else if (counter <= cal / 1.33f)
        {
            sliding = (delta / 8) / (cal / 4);
            friction = 1 - (((Game._arrVelocityFruit / 8) / (cal / 4)) / 100);

        }
        else
        {
            sliding = sliding * 0.85f;
            friction = friction * 0.999f;
        }

        if (sliding > 0.001f && Mathf.Abs(myFruit.transform.localPosition.z - transform.localPosition.z) > random)
        {
            transform.parent.gameObject.GetComponent<Rigidbody>().velocity *= friction;
            myFruit.transform.localPosition = new Vector3 (0, myFruit.transform.localPosition.y, myFruit.transform.localPosition.z - sliding);
        }
        else
        {
            check1 = true;
        }
            
        if (myFruit.transform.localPosition.y > 0.07)
        {
            myFruit.transform.Translate(0, -sliding * 0.75f, 0);
        }
        else if (myFruit.transform.localPosition.y < -0.07)
        {
            myFruit.transform.Translate(0, sliding * 0.75f, 0);
        }
        else
        {
            check2 = true;
        }
    }
}
    