using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scr_DoubleLightSource : MonoBehaviour {

	public int counter;
	public int counterFin;

	public Animator ani;

	// Use this for initialization
	void Awake () {
		counter = 0;
	}

	void FixedUpdate(){

		if(counter >= counterFin){

		ani.SetBool("Open", true);

		}
		else {
		ani.SetBool("Open", false);
		}

	}


	public void DoubleOnOff(){
		if (counter >= counterFin){
			GetComponent<Scr_TriggerLight>().SwitchLightOn();
		}
		else{
			GetComponent<Scr_TriggerLight>().SwitchLightOff();
		}
	}

}
