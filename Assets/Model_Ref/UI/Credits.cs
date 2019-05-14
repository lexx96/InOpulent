using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Credits : MonoBehaviour {

	// Use this for initialization
	void Start () {
        transform.localPosition = new Vector3(0, -900f, 0);
	}
	
	// Update is called once per frame
	void Update () {
        if (transform.localPosition.y <= 1500)
        {
            transform.localPosition += new Vector3(0, Time.deltaTime * 80, 0);
        }
	}
}
