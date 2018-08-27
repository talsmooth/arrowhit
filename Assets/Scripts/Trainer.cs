using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trainer : MonoBehaviour {

    public static bool climb;

    public static bool turn;
   
    float origX;

    public static float nextHeight;

    float rotateS;

    float moveYS;

    float moveZS;

    public float angleX;

    public float stepZ;

    public float stepY;

    Vector3 des;

    public Animator anim;

    int idleHash = Animator.StringToHash("Idle");

    int runHash = Animator.StringToHash("Run");

    int throwHash = Animator.StringToHash("Throw");

    int throw2Hash = Animator.StringToHash("Throw2");

    public static bool throwB;

    bool throw2;

    // Use this for initialization
    void Start () 
    {
        rotateS = angleX/2;

        moveYS = stepY/2;

        moveZS = stepZ/2;

        origX = transform.position.x;
    }

    void Update ()
    {
        if (throwB)
        {
            throwB = false;

            if (!throw2)
            {
                anim.Play(throwHash);
                throw2 = true;
            }
            else
            {
                throw2 = false;
                anim.Play(throw2Hash);
            }
        }
    }

    void FixedUpdate () 
    {
        if (climb)
        {
            Steps();
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
                if (!Player.climb)
                {
                    Player.climb = true;
                }
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
                    transform.Rotate(0, 9.458f, 0);  
                    Vector3 dir = des - transform.position;
                    transform.Translate(dir.x, dir.y, dir.z, Space.World);
                }
                else
                {
                    transform.rotation = Quaternion.Euler(0, 90, 0);
                    transform.position = des;
                    Game.arrows++;
                    Game.Text("One More Arrow", 2);
                    climb = false;
                }
            }

        }

        else if (turn && transform.rotation.y > 0)
        {
            anim.SetBool(runHash, true);
            transform.Rotate(0, -22.5f, 0);
        }
        else
        {
            turn = false;
        }

    }
}




