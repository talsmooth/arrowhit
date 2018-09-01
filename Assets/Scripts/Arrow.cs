using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour {

    Rigidbody rb;

    public GameObject[] col = new GameObject[3];

    float losingVelocity;

    public bool shoot;

    public bool startVel;

    public GameObject help;

    public GameObject splash1;

    public GameObject splash2;

    public bool spl1;

    public int sp1;

    public bool spl2;

    public int sp2;

    public Collider myCol;

    public Collider mySecCol;

    public bool align;

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
            AudioManager._sound.clip = AudioManager._shooting;
            AudioManager._sound.Play();
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
                Destroy (splash1);
                spl1 = false;
            }
        }

        if (spl2)
        {
            if (sp2 == 0)
            {
                //splash2 = Instantiate(splash, help.transform.position, Quaternion.LookRotation(rb.velocity));
            }
            sp2++;
            if (sp2 > 15)
            {
                //Destroy (splash2);
                spl2 = false;
            }
        }
         
        if (align)
        {
            AlignArrow();
        }

        if (startVel && rb != null)
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
        if (collider.gameObject.tag == "fruit" || collider.gameObject.tag == "dummy" || collider.gameObject.tag == "banana")
        {
            for (int i = 0; i < 3; i++)
            {
                if (col[i].GetComponent<ArrowCollider>().myFruit == null)
                {
                    if (i == 1)
                    {
                        Game.arrows += 1;
                        Game.Text("One More Arrow", 2);

                        AudioManager._sound.clip = AudioManager._comboX2;
                        AudioManager._sound.Play();
                    }
                    else if (i == 2)
                    {
                        Game.arrows += 1;
                        Game.Text("One More Arrows", 2);

                        AudioManager._sound.clip = AudioManager._comboX3;
                        AudioManager._sound.Play();
                    }
                    RaycastHit hit;
                    if (Physics.Raycast(transform.position, transform.forward, out hit))
                    {
                        if (collider.gameObject.GetComponent<Fruit>().particle != null)
                        {
                            splash1 = Instantiate(collider.gameObject.GetComponent<Fruit>().particle, hit.point, Quaternion.LookRotation(-rb.velocity));
                            spl1 = true;
                        }
                    }

                    if (collider.gameObject.tag == "fruit" || collider.gameObject.tag == "banana")
                    {
                        StabFruit(collider, col[i]);
                        return;
                    }
                    else
                    {
                        StabDummy(collider);
                        return;
                    }
                }
            }

        }
        else if (collider.gameObject.tag == "bomb")
        {
            Game.gameOver = true;
        }

        else if (collider.gameObject.tag == "arrowBomb")
        {
            collider.GetComponent<ArrowBomb>().explode = true;
        }


    }

    void StabFruit(Collider collider, GameObject col)
    {
        AudioManager._sound.clip = AudioManager._hitFruit;
        AudioManager._sound.Play();

        if (collider.gameObject.tag == "banana")
        {
            AudioManager._banana.Stop();
            Destroy(collider.gameObject.GetComponent<Banana>());
        }

        col.GetComponent<ArrowCollider>().myFruit = collider.gameObject;
        col.GetComponent<ArrowCollider>().copy = Instantiate(collider.gameObject);
        col.GetComponent<ArrowCollider>().align = true;
        Destroy(collider.transform.GetChild(0).gameObject);
        Destroy(col.GetComponent<ArrowCollider>().copy.GetComponent<Rigidbody>());
        Destroy(col.GetComponent<ArrowCollider>().copy.GetComponent<Collider>());
        Destroy(collider.gameObject.GetComponent<Rigidbody>());
        collider.transform.rotation = transform.rotation;
        collider.transform.parent = transform;
        Destroy(collider);

    }

    void StabDummy(Collider collider)
    {     
        AudioManager._sound.clip = AudioManager._hitDummy;
        AudioManager._sound.Play();
        align = true;
        collider.gameObject.GetComponent<Rigidbody>().velocity = rb.velocity / 5;
        col[2].transform.localPosition = new Vector3 (0,0,1);
        col[2].transform.parent = collider.transform;
        transform.parent = col[2].transform;
        myCol.enabled = false;
        Destroy(rb);
        AlignArrow();
    }

    void AlignArrow ()
    {
        Vector3 pos = col[2].transform.localPosition;
        float x = 0;
        float y = 0;
        float z = 0;

        if (Mathf.Abs(pos.x) > 0.9f || Mathf.Abs(pos.y) > 0.9f || Mathf.Abs(pos.z) > 0.9f)
        {
            if (Mathf.Abs(pos.x) > 0.9f)
            {
                if (pos.x > 0)
                {
                    x = -0.1f;
                }
                else
                {
                    x = 0.1f;
                }
                
            }

            if (Mathf.Abs(pos.y) > 0.9f)
            {
                if (pos.y > 0)
                {
                    y = -0.1f;
                }
                else
                {
                    y = 0.1f;
                }

            }

            if (Mathf.Abs(pos.z) > 0.9f)
            {
                if (pos.z > 0)
                {
                    z = -0.1f;
                }
                else
                {
                    z = 0.1f;
                }

            }

            col[2].transform.localPosition = new Vector3 (col[2].transform.localPosition.x + x, col[2].transform.localPosition.y + y, col[2].transform.localPosition.z + z);
        }

        else
        {
            align = false;
        }

    }
        
}
