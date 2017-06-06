using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scr_HideCursor : MonoBehaviour {

	// Use this for initialization
	void Start () {
		Cursor.visible = false;
		Cursor.lockState = CursorLockMode.Locked;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
