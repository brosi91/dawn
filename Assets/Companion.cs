using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Companion : MonoBehaviour {

	public Transform PlayerTarget;
	//public float MaxSpeed;
	//public float SpeedFactor;
	public float MinDistnace = 2;
	public AnimationCurve SpeedCurve;


	void Awake(){

	PlayerTarget = GameObject.Find("CameraTarget").transform;

	}
	
	// Update is called once per frame
	void FixedUpdate () {

		transform.LookAt(PlayerTarget);

		float distance = Vector3.Distance(transform.position, PlayerTarget.position);

		//float speed = Mathf.Clamp(SpeedFactor * distance - MinDistnace, 0, MaxSpeed);
		float speed = SpeedCurve.Evaluate(distance - MinDistnace); 

		if(distance>= MinDistnace){
			transform.position += transform.forward*speed*Time.fixedDeltaTime;
		}

	}
	
}
