using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scr_Sunrise : MonoBehaviour {


	public float speed;
	//public GameObject light;
	public Light intLight;
	public float fadeSpeed;
	private float lerper;


	// Use this for initialization
	void Start () {
		intLight.intensity = 0.0f;
		//light.SetActive(true);
		//start with 0 intensity now so enabling of light no longer needed

		}

	// Update is called once per frame
	void Update () {

		transform.Translate(0, speed * Time.deltaTime, 0);
		if(intLight.intensity < 1.5f){
			LightIntensity ();
		}

	}

	void LightIntensity (){
		lerper += fadeSpeed * Time.deltaTime;
        intLight.intensity = Mathf.Lerp (0.0f , 1.7f , lerper);
     }
}
