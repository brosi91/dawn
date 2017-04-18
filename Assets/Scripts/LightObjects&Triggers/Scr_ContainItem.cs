using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scr_ContainItem : MonoBehaviour {

	public int containItemCount;

	public Animator ani;

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

}
