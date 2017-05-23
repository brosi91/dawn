using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scr_CamInLight : MonoBehaviour {

	public Scr_PlayerInput inputScr;
	private MeshRenderer WaterRend;
	public LayerMask Mask;

	RaycastHit other;
	bool On;

	void FixedUpdate(){
		if (Physics.SphereCast(transform.position, 0.5f, Vector3.up, out other, Mask)){
			Debug.Log("Collision with"+ other.transform.name);
			if (other.transform.tag == "Light"){
				WaterRend = other.transform.GetComponentInChildren<MeshRenderer> ();
				Debug.Log ("Entered into " + WaterRend.gameObject.name);
				if (inputScr.Swim == false) {
					WaterRend.enabled = false;
				} else {
					WaterRend.enabled = true;
				}
			}
		} else {
			if (WaterRend != null){
				WaterRend.enabled = true;
			}
			WaterRend = null;
		}
	}

	/*
	void FixedUpdate(){
		if (Physics.SphereCast(transform.position, 0.5f, Vector3.up, out other)){
			if (other.transform.tag == "Light"){
				WaterRend = other.transform.GetComponentInChildren<MeshRenderer> ();
				if (On && inputScr.Swim == false) {
					WaterRend.enabled = false;
					On = false;
				} else if (!On) {
					WaterRend.enabled = true;
					On = true;
				}
			}
		} else if (!On) {
			if (WaterRend != null){
				WaterRend.enabled = true;
			}
			On = true;
			WaterRend = null;
		}
	}*/
	 /*
	void OnTriggerStay (Collider other) {
		if (other.tag == "Light") {
			WaterRend = other.gameObject.GetComponentInChildren<MeshRenderer> ();
			if (inputScr.Swim == false) {
				WaterRend.enabled = false;
			} else {
				WaterRend.enabled = true;
			}
		}
	}

	void OnTriggerExit(Collider other){
		if (other.tag == "Light") {
			WaterRend.enabled = true;
		}	
	}
	*/
}
