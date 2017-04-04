using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scr_CamLookSun : MonoBehaviour {

	public Transform Sun;


	// Use this for initialization
	void Start () {

				}
	
	// Update is called once per frame
	void Update () {
		transform.LookAt(Sun);
	}

}
