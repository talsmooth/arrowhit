using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destroy : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.tag == "fruit" || collider.gameObject.tag == "bomb" || collider.gameObject.tag == "dummy")
        {
            Destroy(collider.gameObject);
        }
    }
}
