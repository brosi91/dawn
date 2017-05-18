using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scr_Sunrise : MonoBehaviour {


	public float speed;
	//public GameObject light;
	public Light intLight;
	public float fadeSpeed;
	private float lerper;

	public Material normal_Mat;
	public Material faroff_Mat;
	public int matNumberOne;
	public int matNumberTwo;

	public SkinnedMeshRenderer rend;


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

    public void ChangeMat(){
		Material[] newMaterials =  rend.materials;
			newMaterials[matNumberOne] = faroff_Mat;
			newMaterials[matNumberTwo] = faroff_Mat;
			rend.materials = newMaterials;
     }
}
