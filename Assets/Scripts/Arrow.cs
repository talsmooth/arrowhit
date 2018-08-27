using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour {

    Rigidbody rb;

    public GameObject[] col = new GameObject[3];

    float losingVelocity;

    public List <GameObject> fruitDestroy;

    public bool shoot;

    public bool startVel;

    public GameObject help;

    public GameObject splash;

    public GameObject splash1;

    public GameObject splash2;

    public bool spl1;

    public int sp1;

    public bool spl2;

    public int sp2;

    public 

	// Use this for initialization
	void Start () 
    {
        rb = GetComponent<Rigidbody>();
        losingVelocity = Game.losingVelocity;
	}
	
	void Update () 
    {
        if (shoot)
        {
            startVel = true;
            shoot = false;
            rb.AddRelativeForce(0,0,Game._arrowForce); 
            help.GetComponent<SpriteRenderer>().enabled = false;
        }
        if (transform.position.y < -55)
        {
            Destroy(gameObject);
        }
	}

    void FixedUpdate()
    {
        if (spl1)
        {
            sp1++;
            if (sp1 > 15)
            {
                //Destroy (splash1);
                spl1 = false;
            }
        }

        if (spl2)
        {
            if (sp2 == 0)
            {
                splash2 = Instantiate(splash, help.transform.position, Quaternion.LookRotation(rb.velocity));
            }
            sp2++;
            if (sp2 > 15)
            {
                //Destroy (splash2);
                spl2 = false;
            }
        }



        if (startVel)
        {   
            if (rb.velocity != Vector3.zero)
            {
                rb.rotation = Quaternion.LookRotation(rb.velocity);  
            }

            if (losingVelocity < 1)
            {
                rb.velocity = rb.velocity * losingVelocity;
                losingVelocity += Game.losingVelocityLength;
            }
        }
    }

    void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.tag == "fruit")
        {
            for (int i = 0; i < 3; i++)
            {
                if (col[i].GetComponent<ArrowCollider>().myFruit == null)
                {
                    if (i == 1)
                    {
                        Game.arrows += 1;
                        Game.Text("One More Arrow", 2);
                    }
                    else if (i == 2)
                    {
                        Game.arrows += 1;
                        Game.Text("One More Arrows", 2);
                    }
                    RaycastHit hit;
                    if (Physics.Raycast(transform.position, transform.forward, out hit))
                    {
                        splash1 = Instantiate(splash, hit.point, Quaternion.LookRotation(-rb.velocity));
                        spl1 = true;
                    }

                        
                    StabFruit(collider, col[i]);
                    return;
                }
            }

        }
        else
        {
            if (collider.gameObject.tag == "bomb")
            {
                Game.gameOver = true;
            }
        }
    }

    void StabFruit(Collider collider, GameObject col)
    {
        Game.hits++;
        col.GetComponent<ArrowCollider>().myFruit = collider.gameObject;
        col.GetComponent<ArrowCollider>().copy = Instantiate(collider.gameObject);
        col.GetComponent<ArrowCollider>().align = true;
        Destroy(collider.transform.GetChild(0).gameObject);
        Destroy(col.GetComponent<ArrowCollider>().copy.GetComponent<Rigidbody>());
        Destroy(col.GetComponent<ArrowCollider>().copy.GetComponent<CapsuleCollider>());
        Destroy(collider.gameObject.GetComponent<Rigidbody>());
        collider.transform.rotation = transform.rotation;
        collider.transform.parent = transform;
        Destroy(collider);
    }
        
}
