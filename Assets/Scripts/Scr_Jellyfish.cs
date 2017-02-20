﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scr_Jellyfish : MonoBehaviour {

	public Rigidbody Root;
	public Rigidbody Top;

	public float TopRiseSpeed;

	private float LocalHeight;
	private float WordHeight;
	[HideInInspector] public Transform Hand;
    public Transform Goal;

    public float MaxDistance;
    public float MinDistance;
    public float sinkSpeed;

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

        if (Hand != null)
        {
            Root.transform.position = Hand.position;
            WordHeight = Hand.position.y + LocalHeight;
            // FixedJoint  joint = gameObject.AddComponent<FixedJoint>();
            //joint.connectedBody = Hand.GetComponent<Rigidbody>();
        }


        else
        {
            RaycastHit hit;
            Vector3 target;

            if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.down), out hit))
            {
                if (hit.distance > MaxDistance) {
                    Root.transform.position += Vector3.down * sinkSpeed * Time.deltaTime;
                }
                if (hit.distance < MinDistance)
                {
                    Root.transform.position -= Vector3.down * sinkSpeed * Time.deltaTime;
                }
            }
        }
	}

    public void JellyToGoal() {
        if (Goal != null)
        {
            Root.transform.position = Goal.position;
            WordHeight = Goal.position.y + LocalHeight;
        }
    }
	/*void OnTriggerEnter(Collider col){
		//hat kollisionsobjekt dieses script
		Scr_PlayerInput Player = col.gameObject.GetComponent<Scr_PlayerInput>();

		if(Player != null){
			Hand = Player.Hand;
		}
	}*/

}