using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scr_Foodsteps : MonoBehaviour {

	public Transform footR;
	public Transform footL;
	public float grenze;

	private bool footHeightR = false;
	private bool footHeightL = false;
	private Scr_PlayerInput inputScr;

	public AudioClip[] footSteps;

	void Start(){

	inputScr = GetComponent<Scr_PlayerInput>();

	}

	void Update () {
		if (footL.position.y - transform.position.y > grenze){

			footHeightL = true;

		}

		else if (footHeightL && inputScr.Swim == false){
			Scr_Soundmanager.Sound.Play(footSteps[Random.Range(0,footSteps.Length)], gameObject, 0.3f, 0.5f, 0.9f, 1.1f);
			footHeightL = false;
		}

		if(footR.position.y - transform.position.y > grenze){
			footHeightR = true;
		}

		else if (footHeightR && inputScr.Swim == false){
			Scr_Soundmanager.Sound.Play(footSteps[Random.Range(0,footSteps.Length)], gameObject, 0.3f, 0.5f, 0.9f, 1.1f);
			footHeightR = false;
		}
	}
}
