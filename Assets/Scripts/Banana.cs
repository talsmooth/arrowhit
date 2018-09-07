using UnityEngine;
using System.Collections;
public class Banana : MonoBehaviour {

    public Vector3 bananaAngle;

    public Vector3 bananaRotation;

    public float boomerangDistance;

    public float boomerangWidth;

    [Range(1.0f, 10.0f)]
    public float boomerangSeconds;

    [Range(0.01f, 0.94f)]
    public float percentageOfCircle;

    [Range(0.0001f, 0.050f)]
    public float bananaEase;

    float acc = 1.2f;

    public bool drawPath;

    public float check = 1;

    public Rigidbody rb;

    public GameObject cube;

    bool stopAcc;

    public Vector3 vel;

    Vector3 qv;

    public Collider[] cols = new Collider[0];

    public AudioSource bananaAudio;

    float posX;

    Vector3 pos;

    Vector3 temp;

    Vector3 step;

    bool stopCurve;


    void Start () 
    {
        if (!AudioManager._soundMute)
        {
            bananaAudio.clip = AudioManager._bananaFlight;
            bananaAudio.Play();
        }
           
        transform.rotation = Quaternion.Euler(bananaAngle);
        pos = transform.position;
        rb.maxAngularVelocity = 10;
        StartCoroutine(Throw(boomerangDistance, boomerangWidth, vel, boomerangSeconds));
    }

    void Update()
    {
        if (Time.timeScale > 0 && drawPath)
        {
            Instantiate(cube, transform.position, Quaternion.identity);
        }

        posX = transform.position.x;

        if (posX < -14)
        {
            bananaAudio.Stop();
            Destroy(gameObject);
        }

    }

    void FixedUpdate()
    {
        rb.AddRelativeTorque(bananaRotation);
    }
       
    IEnumerator Throw(float dist, float width, Vector3 direction, float time) 
    {
        //rb.useGravity = false;
        //rb.velocity = Vector3.zero;
        Quaternion q = Quaternion.FromToRotation (Vector3.right, direction); 
        float timer = 0.0f;
        float timeT = time;


        while (timer < timeT) 
        {
            float t = (Mathf.PI * 2.0f * ((timer / time) * check) - Mathf.PI/2.0f);
            timeT = time / check;
            float x = width * Mathf.Cos(t);
            float z = dist * Mathf.Sin(t);
            Vector3 v = new Vector3(z + dist, x, 0); 
            if (!stopCurve)
            {
                qv = q * v;
                step = pos + qv - temp;
            }

            if (stopCurve)
            {
                qv = qv + step;
                timer = 0.0f;
            } 

            if (step.x >= 0)
            {
                check -= bananaEase;
            }
            else
            {
                check += bananaEase * 0.65f;
            }
                
            rb.MovePosition(pos + qv);

            timer += Time.deltaTime;

            if ((timer / timeT) > 0.8)
            {
                check += (10 - boomerangSeconds) / (4500 - (percentageOfCircle * 4000));
            }

            if ((timer / timeT) > percentageOfCircle && !stopCurve)
            {
                stopCurve = true;

                if (percentageOfCircle > 0.86f && percentageOfCircle < 0.92f)
                {
                    step = step * (percentageOfCircle * acc);
                }

                else if (percentageOfCircle > 0.91f)
                {
                    step = step * (percentageOfCircle * (acc + 0.5f));
                }
            }

            else if (!stopCurve)
            {
                temp = pos + qv; 
            }

            Debug.Log(check);

            yield return null;
        }
            
    }
}