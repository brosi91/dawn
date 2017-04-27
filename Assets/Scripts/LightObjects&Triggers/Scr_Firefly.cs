using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scr_Firefly : MonoBehaviour {

	 public Transform Goal;
	public bool inGoal;
	public bool inDouble;


	private AudioSource Idle;

	void Awake(){
		inGoal = false;
		inDouble = false;
		Idle = GetComponentInChildren<AudioSource>();
	}

	public void PlayMusic(){

		if (Idle.isPlaying){
			Idle.Stop();
		}
		else {
			Idle.Play();
		}

	}
}
