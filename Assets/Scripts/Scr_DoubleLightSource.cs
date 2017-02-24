using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scr_DoubleLightSource : MonoBehaviour {

	public int counter;
	public int counterFin;

	// Use this for initialization
	void Awake () {
		counter = 0;
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
