using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scr_Firefly : MonoBehaviour {

	[HideInInspector] public Transform Goal;

	public void FireflyToGoal() {
		if (Goal != null)
		{
			transform.position = Goal.position;

		}
	}
}
