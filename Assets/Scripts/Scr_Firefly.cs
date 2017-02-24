using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scr_Firefly : MonoBehaviour {

	 public Transform Goal;
	public bool inGoal;
	public bool inDouble;

	void Awake(){
		inGoal = false;
		inDouble = false;
	}

	/*public void FireflyToGoal() {
		if (Goal != null)
		{
			transform.position = Goal.position;

		}
	}*/
}
