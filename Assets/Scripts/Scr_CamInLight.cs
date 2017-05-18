using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scr_CamInLight : MonoBehaviour {

	public Scr_PlayerInput inputScr;
	private MeshRenderer WaterRend;
	

	void Awake(){

	}

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
}
