using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowLauncher : MonoBehaviour {

    public int noOfArrows;

    public float secondsForLaunch;

    public int howManyFramesKeepFalling;

    public GameObject small;

    float angle = -90;

    int arrowsCount;

    bool fixedFrameCount;

    int frameCount;

    public GameObject bomb;

    int count;

	// Use this for initialization
	void Start () {
        StartCoroutine(Launch());
	}
	
	// Update is called once per frame
	void FixedUpdate () 
    {
        count++;

        if (count < howManyFramesKeepFalling)
        {
            transform.position = bomb.transform.position;
        }

        if (fixedFrameCount)
        {
            frameCount--;
        }
       
	}

    IEnumerator Launch ()
    {
        while (arrowsCount < noOfArrows)
        {
            for (int i = 0; i < noOfArrows; i++)
            {
                Instantiate(small, transform.position, Quaternion.Euler(angle,90,0));  
                angle = angle + (360 / noOfArrows);
                yield return new WaitForSeconds(secondsForLaunch/noOfArrows);
            }   

            break; 

            yield return null;
        }

        Destroy(bomb);
        Destroy(gameObject);

    }

    IEnumerator Frames(int frameCounter)
    {
        frameCount = frameCounter;
        fixedFrameCount = true;
        while (frameCount > 0)
        {
            yield return null;
        }
        fixedFrameCount = false;
    }
}
