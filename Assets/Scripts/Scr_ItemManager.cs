using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scr_ItemManager : MonoBehaviour {

    // Um dieses Script einbauen zu können muss dem Spieler ein Trigger-Collider zugewiesen werden, der seine Reichweite definiert.
    // Ausserdem musst du die Tags "ItemHand" und "ItemLantern" erstellen und den jeweiligen Items zuweisen.


    // Die Tasten für Aktionen mit Hand oder Laterne
    public KeyCode HandKey;
    public KeyCode LanternKey;


    // Die Zeitverzögerungen mit der Items aufgenommen oder abgelegt werden sollen.
    // Diese solten auf die jeweiligen Animationen abgestimmt werden.
    // Sprich: Die Zeit bis die Hand ausgestreckt ist etc.
    public float PickUpDelayHand;
    public float PickUpDelayLantern;

    public float TossDelayHand;
    public float TossDelayLantern;


    // Die Objekte der Hand und der Laterne.
    // An diese werden aufgenommene Items geparentet (welch undeutsches Wort ...)
    public Transform Hand;
    public Transform Lantern;

    //Position für Jellyfish_Lightsource
    public Transform Goal;
    bool inTrigger = false;
    bool GoalRemove = false;
    private Scr_TriggerLight triggerLight;

    // Die Referenzen zu den Items, die sich gegenwärtig im Inventar befinden.
    // InLantern ist eine "List" - Das bedeutet ein Stapel von Objekten des angegebenen Typs.
    // Wenn du nicht weisst wie sich Listen von Arrays unterscheiden ... ähm ... Frag mich einfach.
    GameObject InHand;
    List<GameObject> InLantern;


    // Die Referenzen zu den Items die sich jetzt gerade in Reichweite befinden.
    GameObject ActiveItemHand;
    GameObject ActiveItemLantern;

    
	// Im Update fragen wir die beiden Tasten ab, die für eine Aktion mit der Hand oder der Laterne stehen.
    // Wenn wir feststellen, dass ein Item zum Aufnehmen bereit steht starten wir die PickUp-Coroutine.
    // Diese ist deshalb eine Coroutine, dass wir einen Delay einbauen können, damit das Item erst dann genommen wird, wenn die Animation soweit ist.
    // Für die Laterne ist es fast das selbe, bloss dass wir da nocht festellen müssen ob bereits 3 Items in der Laterne sind.
    // Wenn es nichts zum aufnehmen gibt, legen wir stattdessen ein Item ab.

	void Update () {
        if (Input.GetKeyDown(HandKey)) {
            if (ActiveItemHand != null)
            {
                Debug.Log("Pick up");
                StartCoroutine(PickUpHand());
                // Hier soll später die PickUp-Animation getriggert werden
            }
            else if (inTrigger == true) {
                StartCoroutine(TossHandToLight());
            }
            else
            {
                StartCoroutine(TossHand());
                Debug.Log("Toss");
                // Hier soll später die Ablege-Animation getriggert werden
            }
        }

        if (Input.GetKeyDown(LanternKey)) {
            if (ActiveItemLantern != null && InLantern.Count < 3) {
                StartCoroutine(PickUpLantern());
                // Hier soll später die PickUp-Animation getriggert werden
            }
            else {
                StartCoroutine(TossLantern());
                // Hier soll später die Ablege-Animation getriggert werden
            }
        }
    }

    

    IEnumerator PickUpHand() {
        yield return new WaitForSeconds(PickUpDelayHand);

        InHand = ActiveItemHand;
        WorldToHand(ActiveItemHand);
		ActiveItemHand = null;
    }

    IEnumerator PickUpLantern() {
        yield return new WaitForSeconds(PickUpDelayLantern);

        InLantern.Add(ActiveItemLantern);
        WorldToLantern(ActiveItemLantern);
		ActiveItemLantern = null;
    }



    IEnumerator TossHand() {
        yield return new WaitForSeconds(TossDelayHand);

		HandToWorld(InHand);
        InHand = null;
    }

    IEnumerator TossLantern() {
        yield return new WaitForSeconds(TossDelayHand);

        if (InLantern.Count > 0) {
            HandToWorld(InLantern[0]);
			InLantern.RemoveAt(0);
        }

     
    }

    IEnumerator TossHandToLight()
    {
        yield return new WaitForSeconds(TossDelayHand);

        HandToLight(InHand);
        InHand = null;
    }



    // Über OnTriggerEnter checken wir, ob sich ein getagtes Item für Hand oder Laterne in Reichweite befindet.
    // In dem Fall wird dieses Item als ActiveItem gespeichert.
    // Auf dieses aktive Item greifen wir zu wenn wir dann eine Taste drücken.
    // In OnTriggerExit wird das ActiveItem wieder null gesetzt und es gibt nichts mehr zum Aufnehmen.

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "ItemHand") {
            ActiveItemHand = other.gameObject;
        }
        else if (other.tag == "ItemLantern") {
            ActiveItemLantern = other.gameObject;
        }
        else if (other.tag == "TriggerJellyfish")
        {
            inTrigger = true;
            Goal = other.transform;
            triggerLight = other.GetComponentInChildren<Scr_TriggerLight>();
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.tag == "ItemHand") {
            ActiveItemHand = null;
        }
        else if (other.tag == "ItemLantern") {
            ActiveItemLantern = null;
        }

        else if (other.tag == "TriggerJellyfish")
        {
            inTrigger = false;
        }
    }



    // Diese Methoden rufen wir auf, wenn wir ein Item aus der Welt ins Inventar nehmen oder umgekehrt.
    // In WorldToHand & WorldToLantern legen wir fest was alles an den Gameobjects geändert wird, wenn sie im Inventar sind.
    // HandToWorld & LanternToWorld machen diese Änderungen wieder rückgängig wenn das Item abgelegt wird.

    void WorldToHand (GameObject item) {
        item.tag = "Untagged";
        item.GetComponent<Scr_Jellyfish>().Hand = Hand;
        item.transform.position = Hand.position;
        if (GoalRemove == true) {
            triggerLight.SwitchLightOff();
            // stop swimming as soon as pick up
            GetComponent<Scr_PlayerInput>().DisableSwim();
            GoalRemove = false; 
        }
    }
    void HandToWorld(GameObject item) {
        item.tag = "ItemHand";
		item.GetComponent<Scr_Jellyfish>().Hand = null;
		Rigidbody[] rigidBodies = item.GetComponentsInParent<Rigidbody>();
		foreach (Rigidbody rig in rigidBodies){
			rig.velocity = Vector3.zero; //Jellyfish hat keine geschwindigkeit mehr (im falle von ablegen bei bewegung)
			rig.angularVelocity = Vector3.zero; 
		}
		item.GetComponent<Rigidbody>().velocity = Vector3.zero; //für letztes child
		item.GetComponent<Rigidbody>().angularVelocity = Vector3.zero; 
    }

    void HandToLight(GameObject item) {
        item.tag = "ItemHand";
        item.GetComponent<Scr_Jellyfish>().Hand = null;
        item.GetComponent<Scr_Jellyfish>().Goal = Goal;
        item.transform.position = Goal.position;
        item.GetComponent<Scr_Jellyfish>().JellyToGoal();
        triggerLight.SwitchLightOn();
        GoalRemove = true;
    }

    void WorldToLantern(GameObject item) {
        item.tag = "Untagged";
        item.transform.parent = Lantern;
        item.transform.position = Lantern.position;
    }
    void LanternToWorld(GameObject item) {
        item.tag = "ItemLantern";
        item.transform.parent = null;
    }

}
