using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropRock : MonoBehaviour {

    public bool dropRock;
    public float dropForce;
    Rigidbody rb;

    void Start ()
    {
        rb = transform.GetChild(0).GetComponent<Rigidbody>();
    }
	
	void Update ()
    {
        if (dropRock)
        {
            rb.useGravity = true;
            if (transform.GetChild(0).GetComponent<Rigidbody>().useGravity == true)
            {
                rb.velocity = new Vector3(0, -dropForce, 0);
            }
            rb.isKinematic = false;
        }

        if (transform.GetChild(0).localPosition.z < -13)
        {
            Destroy(gameObject);
        }
    }
}
