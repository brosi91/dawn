using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scr_FixedRotation : MonoBehaviour {

Quaternion Rotation;


	void Awake (){
		Rotation = transform.rotation;
	}
	

	void LateUpdate () {
		transform.rotation = Rotation;
	}
}
