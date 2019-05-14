using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeBlock : MonoBehaviour {

    public Material transparent;

    public Material currentMaterial;
    public bool changeMaterial = false;

	void Start ()
    {
		
	}

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Walls"))
        {
            changeMaterial = true;
            currentMaterial = other.GetComponent<MeshRenderer>().material;
        }
    }

    void OnTriggerStay(Collider other)
    {
        if (changeMaterial && other.gameObject.CompareTag("Walls"))
        {
            other.GetComponent<MeshRenderer>().material = transparent;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Walls"))
        {
            changeMaterial = false;
            other.GetComponent<MeshRenderer>().material = currentMaterial;
        }
    }
}
