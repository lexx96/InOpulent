using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockCollision : MonoBehaviour {

	void Start () {}

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Destroy(GetComponent<Collider>());
            other.GetComponent<CharacterControllerParent>().TakeDamage(other.transform, false, 5, 0);
        }
    }
}
