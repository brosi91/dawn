using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scr_Jellyfish : MonoBehaviour {

	public Rigidbody Root;
	public Rigidbody Top;

	public float TopRiseSpeed;

	private float LocalHeight;
	private float WordHeight;
	private Transform Hand;

	void Awake(){
		LocalHeight = Top.transform.position.y - Root.transform.position.y;
	}

	void FixedUpdate(){
		//wenn kopf zu tief, nach oben
		if(Top.transform.position.y < WordHeight){
			Top.AddForce(Vector3.up*TopRiseSpeed);
		}

	}

	void LateUpdate(){

		if(Hand != null){
			Root.transform.position = Hand.position;
			WordHeight = Hand.position.y + LocalHeight;
		}

	}

	void OnTriggerEnter(Collider col){
		//hat kollisionsobjekt dieses script
		Scr_PlayerInput Player = col.gameObject.GetComponent<Scr_PlayerInput>();

		if(Player != null){
			Hand = Player.Hand;
		}
	}

}
