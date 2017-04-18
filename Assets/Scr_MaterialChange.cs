using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scr_MaterialChange : MonoBehaviour {

	public Material materialOne;
	public Material materialTwo;
	public GameObject FireflyHolder;
	public int matNumber;

	Renderer rend;

	// Use this for initialization
	void Awake () {
		rend = FireflyHolder.GetComponent<Renderer>();
	}


	void OnTriggerStay(Collider other){
		if (other.tag == "Player") {
			Material[] newMaterials =  rend.materials;
			newMaterials[matNumber] = materialTwo;
			rend.materials = newMaterials;
			Debug.Log("Changed Materail to: " + rend.materials[matNumber].name);
        }	
    }

	void OnTriggerExit(Collider other){
		if (other.tag == "Player") {
			Material[] newMaterials =  rend.materials;
			newMaterials[matNumber] = materialOne;
			rend.materials = newMaterials;
			Debug.Log("Changed Materail to: " + rend.materials[matNumber].name);
        }

	}

}
