using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour {

	[SerializeField]
	Transform _target;

	Vector3 dir;

	void Start () {
		dir = _target.position - Camera.main.transform.position;
	}
	
	void LateUpdate () {
		Camera.main.transform.position = _target.position - dir;
	}
}
