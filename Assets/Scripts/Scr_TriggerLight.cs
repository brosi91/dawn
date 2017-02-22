using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scr_TriggerLight : MonoBehaviour {


    public GameObject LightOn;

    void Awake() {
        LightOn.SetActive(false);
    }

    public void SwitchLightOn()
    {
        LightOn.SetActive(true);
        Debug.Log("Gotcha");
    }

    public void SwitchLightOff() {
        LightOn.SetActive(false);

    }
}
