using UnityEngine;
using System.Collections;

public class ScreenShotManager : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

		if(Input.GetKeyDown(KeyCode.Tab)) {
			ScreenCap.Take(6);
		}
	
	}
}
