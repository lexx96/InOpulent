using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitEffect : MonoBehaviour {

    private float effectSpeed;
    public float effectSpeedMax;
    public float effectSpeedMin;
    public float scaleDownSpeed = 2.5f;

	void Start ()
    {
        Destroy(gameObject, 1.5f);

        effectSpeed = Random.Range(effectSpeedMin, effectSpeedMax);
        transform.localScale = new Vector3(3, 3, 3);
    }

	void Update ()
    {
        Vector3 dir = Camera.main.transform.position - transform.position;

        transform.forward = -dir;

        transform.position = new Vector3(transform.position.x + Time.deltaTime * effectSpeed, transform.position.y + Time.deltaTime * 3f, transform.position.z);

        if (transform.localScale.x > 0)
        {
            transform.localScale = new Vector3(transform.localScale.x - Time.deltaTime * scaleDownSpeed, 
                                               transform.localScale.y - Time.deltaTime * scaleDownSpeed, 
                                               transform.localScale.z - Time.deltaTime * scaleDownSpeed);
        }
	}
}
