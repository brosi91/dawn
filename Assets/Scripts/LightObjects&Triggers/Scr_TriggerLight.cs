using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scr_TriggerLight : MonoBehaviour {


    public GameObject LightOn;
    public float FadeSpeed;
    public AnimationCurve FadeCurve;
    public GameObject LightSphere;
	public SphereCollider LightCollider;
	public ParticleSystem[] Particles;
	public Light light;

    Material OriginalMaterial;
    Material tempMaterial;
    bool lightOn;
    bool fading;
    MeshRenderer render;
    float alpha;
    float startalpha;
    float startIntensity;


    void Awake() {
    	if( light == null){
    	light = GetComponentInChildren<Light>();
    	}
        //LightOn.SetActive(false);
		render = LightSphere.GetComponent<MeshRenderer>();
		OriginalMaterial = render.material;
		tempMaterial = new Material(OriginalMaterial);
		startalpha = OriginalMaterial.GetVector("_InvFadeParemeter").x;
		startIntensity = light.intensity;

		render.enabled = false;
		light.intensity = 0;
		light.enabled = false;
		tempMaterial.SetVector("_InvFadeParemeter", Vector4.zero);
		LightCollider.enabled = false;
		foreach (ParticleSystem ps in Particles) {
			ps.Stop();
		}
    }

    public void SwitchLightOn()
    {
    	Debug.Log ("LIGHT ON");
        light.intensity = 0;
        light.enabled = true;
		render.enabled = true;
        render.material = tempMaterial;
        lightOn = true;
		fading = true;
        alpha = 0;
		LightCollider.enabled = true;
		foreach (ParticleSystem ps in Particles) {
			ps.Play();
		}
    }

    public void SwitchLightOff() {
		Debug.Log ("LIGHT OFF");
		light.intensity = startIntensity;
		render.material = tempMaterial;
		lightOn = false;
		fading = true;
		alpha = 1f;
		LightCollider.enabled = false;
		foreach (ParticleSystem ps in Particles) {
			ps.Stop();
		}
    }

	void FixedUpdate(){
		if (fading){
			if (lightOn){
				if (alpha < 1f){
					alpha += FadeSpeed;
					tempMaterial.SetVector("_InvFadeParemeter", new Vector4 (FadeCurve.Evaluate(alpha) * startalpha, 0, 0, 0));
					light.intensity = Mathf.Lerp(0, startIntensity, alpha);
				}
				else {
					render.material = OriginalMaterial;
					fading = false;
				}
			}
			else {
				if (alpha > 0){
					alpha -= FadeSpeed;
					tempMaterial.SetVector("_InvFadeParemeter", new Vector4 (FadeCurve.Evaluate(alpha) * startalpha, 0, 0, 0));
					light.intensity = Mathf.Lerp(startIntensity, 0, alpha);
				}
				else {
					render.enabled = false;
					light.enabled = false;
					fading = false;
				}
			}
		}
    }

}
