using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scr_AnimationLayers : MonoBehaviour {

public float[] Weights;
public float[] Blendspeed;

private Animator Anim;

	void Awake () {
		Anim = GetComponent<Animator>();
	}
	
	void FixedUpdate () {
		for(int i = 0; i < Weights.Length; i++){
			if(Weights[i]< Anim.GetLayerWeight(i)){
				float NewWeight = Mathf.Clamp01(Anim.GetLayerWeight(i)-Blendspeed[i]);
				Anim.SetLayerWeight(i, NewWeight);
			}
			else if(Weights[i]> Anim.GetLayerWeight(i)){
				float NewWeight = Mathf.Clamp01(Anim.GetLayerWeight(i)+Blendspeed[i]);
				Anim.SetLayerWeight(i, NewWeight);
			}
		}
	}
}
