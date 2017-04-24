using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scr_ContainItem : MonoBehaviour {

	public int containItemCount;

	public Animator ani;
	public ParticleSystem parti;

	void Awake(){
		containItemCount = 0;
	}

	void FixedUpdate(){

		if(containItemCount > 0){

		ani.SetBool("Open", true);

		}
		else {
		ani.SetBool("Open", false);
		}

	}

	void Update(){

		if(parti != null){
			if(containItemCount > 0 && parti.isPlaying != true){
				parti.Play();
			} 
			else if(containItemCount < 1&& parti.isPlaying == true){
				parti.Stop();
			}
		}

	}
}
