using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cam : MonoBehaviour {


    public static bool climb;

    public GameObject player;

    float delta;

    float oriDelta;

    bool afterMax;


    public float startEase = 0.013f;

    float _startEase;

    public float increasingSpeed = 15;  

    float _increasingSpeed;

    public float maxSpeed = 0.037f;

    float _maxSpeed;

    public float finalEase = 0.038f;

    float _finalEase;

    public float finalEaseMax = 0.004f;

    float _finalEaseMax;

    public float distanceFinalEase = -1.1f;

    float _distanceFinalEase;

	// Use this for initialization
	void Start () 
    {
        oriDelta = transform.position.y - player.transform.position.y;

        _startEase = startEase;

        _increasingSpeed = increasingSpeed;

        _maxSpeed = maxSpeed;

        _finalEase = 1 - finalEase;

        _finalEaseMax = finalEaseMax;

        _distanceFinalEase = distanceFinalEase;

	}
	
	// Update is called once per frame
	void FixedUpdate () 
    {
        if (climb)
        {
            delta = transform.position.y - (player.transform.position.y + oriDelta);
           
            _startEase = _startEase * (1+ (delta / - _increasingSpeed));
          
            if ( _startEase > _maxSpeed)
            {
                _startEase = _maxSpeed;
                afterMax = true;
            }

            if (delta < 0)
            {
                if (delta > _distanceFinalEase && _startEase > _finalEaseMax)
                {   
                    if (afterMax)
                    {
                        _startEase = _startEase * _finalEase;
                    } 
                }

                transform.position = new Vector3(transform.position.x, transform.position.y +  _startEase, transform.position.z);
            }

            else if (delta >= 0)
            {

                //if (delta > 0.001)
                //{
                    //transform.position = new Vector3(transform.position.x, transform.position.y - 0.0005f, transform.position.z);
               // }
                //else
                //{
                    if (!Player.climb && !Trainer.climb)
                    {
                        afterMax = false;
                        transform.position = new Vector3(transform.position.x, player.transform.position.y + oriDelta, transform.position.z);
                        _startEase = startEase;
                        climb = false;
                    }
                //}
            }
        }
	}
}
