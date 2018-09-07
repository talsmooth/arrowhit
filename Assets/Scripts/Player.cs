using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    public GameObject arrow;

    public static bool climb;

    public static bool turn;

    public Animator anim;

    int idleHash = Animator.StringToHash("Idle");

    int runHash = Animator.StringToHash("Run");

    int shootHash = Animator.StringToHash("Shoot");

    int shootHash2 = Animator.StringToHash("Shoot2");

    float origX;

    public static float nextHeight;

    float rotateS;

    float moveYS;

    float moveZS;

    public float angleX;

    public float stepZ;

    public float stepY;

    Vector3 des;

    bool shoot2;

    bool instan;

    GameObject Arrow;

    // Use this for initialization
    void Start () 
    {
        rotateS = angleX/2;

        moveYS = stepY/2;

        moveZS = stepZ/2;

        origX = transform.position.x;
    }

    // Update is called once per frame
    void Update ()
    {
        if (Input.GetMouseButton(0) && !climb)
        {
            Shoot();
        }
        else if (Input.GetMouseButtonUp(0) && instan && Arrow.GetComponent<Rigidbody>() != null)
        {
            instan = false;
            Arrow.GetComponent<Rigidbody>().isKinematic = false;
            Arrow.GetComponent<Arrow>().shoot = true;
            Arrow = null;
        }
    }
        
    void FixedUpdate () 
    {
        if (climb)
        {
            Steps();
        }
    }

    void Shoot()
    { 
        Vector3 dir = new Vector3 (Input.mousePosition.x, Input.mousePosition.y, 9.5f);
        dir = Camera.main.ScreenToWorldPoint(dir) - transform.position;
        if (Arrow != null)
        {
            Arrow.transform.rotation = Quaternion.Euler(Mathf.Atan2(dir.x, dir.y) * Mathf.Rad2Deg - 90, 90, 0);
        }

        if (!instan && Game.arrows > 0)
        {
            if (!shoot2)
            {
                anim.SetTrigger(shootHash);
                shoot2 = true;
            }
            else
            {
                shoot2 = false;
                anim.SetTrigger(shootHash2);
            }
            if (!Game._arrowOnOff)
            {
                Game.arrows--;
            }
            instan = true;
            Arrow = Instantiate(arrow, transform.position, Quaternion.Euler(Mathf.Atan2(dir.x, dir.y) * Mathf.Rad2Deg - 90, 90, 0));
            Arrow.GetComponent<Rigidbody>().isKinematic = true;
        }
    }

    void Steps()
    {
        if (!turn)
        {
            if (transform.position.y < nextHeight - 2.8f || (transform.position.y > nextHeight - 0.5f && transform.position.y < nextHeight))
            {
                if (transform.position.y > nextHeight-1)
                {
                    rotateS = rotateS * 0.99f;
                    moveYS = moveYS * 0.99f;
                    moveZS = moveZS * 0.99f; 
                }

                else if (rotateS > angleX)
                {
                    rotateS = rotateS * 1.01f;
                    moveYS = moveYS * 1.01f;
                    moveZS = moveZS * 1.01f;
                }
                else
                {
                    rotateS = angleX;
                    moveYS = stepY;
                    moveZS = stepZ;
                }

                transform.Rotate(0, rotateS, 0);
                transform.Translate(0, moveYS, moveZS, Space.Self);
            }
            else if (transform.position.y < nextHeight)
            {
                transform.Rotate(0, angleX, 0);
                transform.Translate(0, stepY, stepZ, Space.Self);
            }
            else
            {       
                des = new Vector3(origX, nextHeight, 0);
                anim.SetBool(runHash, false);
                anim.SetTrigger(idleHash);

                if (transform.rotation.eulerAngles.y < 90)
                {
                    transform.Rotate(0, 4.778f, 0);  
                    Vector3 dir = des - transform.position;
                    transform.Translate(dir.x, dir.y, dir.z, Space.World);
                }
                else
                {
                    transform.rotation = Quaternion.Euler(0, 90, 0);
                    transform.position = des;
                    climb = false;
                }
            }

        }
            
        else if (turn && transform.rotation.y > 0)
        {
            if (instan)
            {
                instan = false;
                Arrow.GetComponent<Rigidbody>().isKinematic = false;
                Arrow.GetComponent<Arrow>().shoot = true;
            }
            anim.SetBool(runHash, true);
            transform.Rotate(0, -22.5f, 0);
        }
        else
        {
            turn = false;
        }
            
    }
        
}
