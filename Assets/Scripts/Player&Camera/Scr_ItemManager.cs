using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;

public class Scr_ItemManager : MonoBehaviour {

    // Um dieses Script einbauen zu können muss dem Spieler ein Trigger-Collider zugewiesen werden, der seine Reichweite definiert.
    // Ausserdem musst du die Tags "ItemHand" und "ItemLantern" erstellen und den jeweiligen Items zuweisen.


    // Die Tasten für Aktionen mit Hand oder Laterne
    //public KeyCode HandKey;
  	//public KeyCode HandKeyXbox;
   // public KeyCode LanternKey;
   // public KeyCode LanternKeyXbox;
   //wird jetzt via inControl geregelt


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
    bool inTriggerJelly = false;
	bool inTriggerFirefly = false;
    bool inDoubleTrigger = false;
    // bool GoalRemove = false;
    private Scr_TriggerLight triggerLight;
    private Scr_DoubleLightSource doubleLight;
    private Scr_Firefly Firefly;
    private Scr_Jellyfish Jellyfish;
	private Scr_ContainItem ContainItem;
	private int counterJelly = 0;

	private Animator ani;
	private bool handHolding;
	public float handUpSpeed;
	public float handDownSpeed;

    // Die Referenzen zu den Items, die sich gegenwärtig im Inventar befinden.
    // InLantern ist eine "List" - Das bedeutet ein Stapel von Objekten des angegebenen Typs.
    // Wenn du nicht weisst wie sich Listen von Arrays unterscheiden ... ähm ... Frag mich einfach.
    GameObject InHand;
	public List<GameObject> InLantern = new List<GameObject>();


    // Die Referenzen zu den Items die sich jetzt gerade in Reichweite befinden.
    GameObject ActiveItemHand;
    GameObject ActiveItemLantern;

    //inControl inputs
    private MyPlayerAction characterActions;

    //für LanternGui
    public Image lanternImage;
	public GameObject LanternItem1;
	public GameObject LanternItem2;
	public GameObject LanternItem3;

	private float fadeLanternTime = float.MaxValue;
	public float fadeSpeedLantern;
	private float faderLantern = 0.5f;

	//für Laterne texturenwechsel
	public Material matLantern;
	public GameObject objLantern;
	public Texture lanternTexture00;
	public Texture lanternTexture01;
	public Texture lanternTexture02;
	public Texture lanternTexture03;

	private Renderer rend;


	//für Sonne
	public GameObject Sun;
	public Camera camPlayer;
	public Camera camSun;
	public Camera camThree;
	public Camera camFour;
	public Camera camFive;

	private float endTime = float.MaxValue;

	public Image whiteScreen;
	public float fadeSpeed;
	private float fader;


	//Audio
	public AudioClip[] PickUpJelly;
	public AudioClip[] DropJelly;
	public AudioClip[] PickUpFirefly;
	public AudioClip[] DropFirefly;

	//Music change
	public AudioMixerSnapshot changeEnd;
	public AudioClip endTrack;
	private bool endMusicOn;



    void Awake(){

    	ani = GetComponent<Animator>();
		camPlayer.enabled = true;
		camSun.enabled = false;
		camThree.enabled = false;

		rend = objLantern.GetComponent<Renderer>();
		endMusicOn = false;

    }

	void OnEnable()
	{
		// See PlayerActions.cs for this setup.
		characterActions = MyPlayerAction.CreateWithDefaultBindings();
	}


	void OnDisable()
	{
		// This properly disposes of the action set and unsubscribes it from
		// update events so that it doesn't do additional processing unnecessarily.
		characterActions.Destroy();
	}
    
	// Im Update fragen wir die beiden Tasten ab, die für eine Aktion mit der Hand oder der Laterne stehen.
    // Wenn wir feststellen, dass ein Item zum Aufnehmen bereit steht starten wir die PickUp-Coroutine.
    // Diese ist deshalb eine Coroutine, dass wir einen Delay einbauen können, damit das Item erst dann genommen wird, wenn die Animation soweit ist.
    // Für die Laterne ist es fast das selbe, bloss dass wir da nocht festellen müssen ob bereits 3 Items in der Laterne sind.
    // Wenn es nichts zum aufnehmen gibt, legen wir stattdessen ein Item ab.

	void Update () {
		if (characterActions.interact2.WasPressed) {
            if (ActiveItemHand != null)
            {
            	if(counterJelly <1){
					ani.SetTrigger("GrabRight");
              		StartCoroutine(PickUpHand());
              		handHolding = true;
                }
            }
			else if (inTriggerJelly == true && InHand != null) {
                StartCoroutine(TossHandToLight());
                handHolding = false;
            }
			else if (InHand != null)
            {
				StartCoroutine(TossHand());
				handHolding = false;
                // Hier soll später die Ablege-Animation getriggert werden
            }
        }

		if (characterActions.interact1.WasPressed) {
			if (ActiveItemLantern != null && InLantern.Count < 3) {

				ani.SetTrigger("GrabLeft");
				StartCoroutine (PickUpLantern ());

			} 
			else if (inTriggerFirefly == true && InLantern.Count > 0) {
				StartCoroutine (TossLanternToLight ());
			}
			else if (inDoubleTrigger == true && InLantern.Count > 0){
				StartCoroutine(TossLanternToDouble());
			}
			else if(InLantern.Count > 0){
				StartCoroutine(TossLantern());
                // Hier soll später die Ablege-Animation getriggert werden
            }
        }

		if(Time.time - endTime > 5f){
			camSun.enabled = false;
			camThree.enabled = true;
			}
		if(Time.time - endTime > 10f){
			camThree.enabled = false;
			camFour.enabled = true;
			Sun.GetComponent<Scr_Sunrise>().ChangeMat();
			}
		if(Time.time - endTime > 15f){
			camFour.enabled = false;
			camFive.enabled = true;
			}
		if(Time.time - endTime > 20f){
			fader += fadeSpeed*Time.deltaTime;
			whiteScreen.color = new Color(1,1,1, fader);
		}
		if(Time.time - endTime > 31f){
			SceneManager.LoadScene(0);
		}

		if( Time.time - fadeLanternTime > 3f){
			faderLantern -= fadeSpeedLantern*Time.deltaTime;
			lanternImage.color = new Color( lanternImage.color.r, lanternImage.color.g, lanternImage.color.b, faderLantern);
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

        //InLantern.Add(ActiveItemLantern);
        WorldToLantern(ActiveItemLantern);
		//ActiveItemLantern = null;
    }



    IEnumerator TossHand() {
        yield return new WaitForSeconds(TossDelayHand);

		HandToWorld(InHand);
        InHand = null;
    }

    IEnumerator TossLantern() {
        yield return new WaitForSeconds(TossDelayHand);

        if (InLantern.Count > 0) {
            LanternToWorld(InLantern[0]);


        }  
    }

    IEnumerator TossHandToLight()
    {
        yield return new WaitForSeconds(TossDelayHand);

        HandToLight(InHand);
        InHand = null;
    }

	IEnumerator TossLanternToLight(){
		yield return new WaitForSeconds(TossDelayHand);

		if (InLantern.Count > 0) {
			LanternToLight(InLantern[0]);

		}
	}

	IEnumerator TossLanternToDouble(){
		yield return new WaitForSeconds(TossDelayHand);

		if (InLantern.Count > 0) {
			LanternToDoubleLight(InLantern[0]);

		}

	}

	void FixedUpdate(){

		float weight =  ani.GetLayerWeight(3);
		if (handHolding == true && weight <1f ){
			ani.SetLayerWeight(3, weight + handUpSpeed);
		}
		else if (handHolding == false && weight >0){
			ani.SetLayerWeight(3, weight - handDownSpeed);
		}



		switch (InLantern.Count){
			case 1:
				rend.material.mainTexture = lanternTexture01;
				break;
			case 2:
				rend.material.mainTexture = lanternTexture02;
				break;
			case 3:
				rend.material.mainTexture = lanternTexture03;
				break;
			default:
				rend.material.mainTexture = lanternTexture00;
				break;
		}

		LanternItem1.SetActive(InLantern.Count >=1);
		LanternItem2.SetActive(InLantern.Count >= 2);
		LanternItem3.SetActive(InLantern.Count >= 3);

	}


    // Über OnTriggerEnter checken wir, ob sich ein getagtes Item für Hand oder Laterne in Reichweite befindet.
    // In dem Fall wird dieses Item als ActiveItem gespeichert.
    // Auf dieses aktive Item greifen wir zu wenn wir dann eine Taste drücken.
    // In OnTriggerExit wird das ActiveItem wieder null gesetzt und es gibt nichts mehr zum Aufnehmen.

    void OnTriggerStay(Collider other)
    {
        if (other.tag == "ItemHand") {
            ActiveItemHand = other.gameObject;
            Jellyfish = other.GetComponent<Scr_Jellyfish>();
        }
        else if (other.tag == "ItemLantern") {
            ActiveItemLantern = other.gameObject;
            Firefly = other.GetComponent<Scr_Firefly>();
        }
		else if (other.tag == "TriggerJellyfish")
        {
            inTriggerJelly = true;
            Goal = other.transform;
            triggerLight = other.GetComponentInChildren<Scr_TriggerLight>();
			ContainItem = other.GetComponent<Scr_ContainItem> ();
        }
		else if ( other.tag == "TriggerFirefly")
		{
			inTriggerFirefly = true;
			Goal = other.transform;
			triggerLight = other.GetComponentInChildren<Scr_TriggerLight>();
			ContainItem = other.GetComponent<Scr_ContainItem> ();
		}
		else if (other.tag == "TriggerDoubleFirefly")
        {
            inDoubleTrigger = true;
            Goal = other.transform;
            triggerLight = other.GetComponentInParent<Scr_TriggerLight>();
            doubleLight = other.GetComponentInParent<Scr_DoubleLightSource>();
			ContainItem = other.GetComponent<Scr_ContainItem> ();
        }

		else if( other.tag == "Sun" && endMusicOn == false){
			endTime = Time.time;

			GetComponent<Scr_PlayerInput>().TurnOff();
			Sun.GetComponent<Scr_Sunrise>().enabled = true;
			//camPlayer.tag = "Untagged";
			//camSun.tag = "MainCamera";
			camPlayer.enabled = false;
			camSun.enabled = true;
			lanternImage.enabled = false;
			changeEnd.TransitionTo(1f);
			Scr_Soundmanager.Sound.Play( endTrack, gameObject,0.8f, 0.8f, 1f, 1f, 1000f, 0f);
			endMusicOn = true;

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
            inTriggerJelly = false;
			if (Jellyfish != null) {
				Jellyfish.Goal = null;
			}
			Goal = null;
        }
			

		else if (other.tag == "TriggerFirefly")
		{
			inTriggerFirefly = false;
			if (Firefly != null) {
				Firefly.Goal = null;
			}
			Goal = null;
		}

		else if (other.tag == "TriggerDoubleFirefly")
		{
			inDoubleTrigger = false;
			if (Firefly != null) {
				Firefly.Goal = null;
			}
			Goal = null;

		}

    }



    // Diese Methoden rufen wir auf, wenn wir ein Item aus der Welt ins Inventar nehmen oder umgekehrt.
    // In WorldToHand & WorldToLantern legen wir fest was alles an den Gameobjects geändert wird, wenn sie im Inventar sind.
    // HandToWorld & LanternToWorld machen diese Änderungen wieder rückgängig wenn das Item abgelegt wird.

    void WorldToHand (GameObject item) {
		if (counterJelly < 1) {
			item.tag = "Untagged";
			Jellyfish.Hand = Hand;
			item.transform.position = Hand.position;
			if (Jellyfish.inGoal == true && ContainItem.containItemCount > 0) {
				triggerLight.SwitchLightOff ();
				// stop swimming as soon as pick up
				GetComponent<Scr_PlayerInput> ().DisableSwim ();
				Jellyfish.inGoal = false; 
				ContainItem.containItemCount -= 1;
			}
			counterJelly++;
			Scr_Soundmanager.Sound.Play(PickUpJelly[Random.Range(0,PickUpJelly.Length)], gameObject, 0.3f, 0.5f, 0.8f, 1.2f);
		}
    }
    void HandToWorld(GameObject item) {
		if (counterJelly > 0) {
			Jellyfish = item.GetComponent<Scr_Jellyfish> ();
			item.tag = "ItemHand";
			Jellyfish.Hand = null;
			Rigidbody[] rigidBodies = item.GetComponentsInParent<Rigidbody> ();
			foreach (Rigidbody rig in rigidBodies) {
				rig.velocity = Vector3.zero; //Jellyfish hat keine geschwindigkeit mehr (im falle von ablegen bei bewegung)
				rig.angularVelocity = Vector3.zero; 
			}
			item.GetComponent<Rigidbody> ().velocity = Vector3.zero; //für letztes child
			item.GetComponent<Rigidbody> ().angularVelocity = Vector3.zero; 
			counterJelly--;
			Scr_Soundmanager.Sound.Play(DropJelly[Random.Range(0,DropJelly.Length)], gameObject, 0.3f, 0.5f, 0.8f, 1.2f);
		}
    }

    void HandToLight(GameObject item) {
		if (item.GetComponent<Scr_Jellyfish> () != null && ContainItem.containItemCount < 1 && counterJelly > 0) {
			Jellyfish = item.GetComponent<Scr_Jellyfish> ();
			item.tag = "ItemHand";
			Jellyfish.Hand = null;
			Jellyfish.Goal = Goal;
			item.transform.position = Goal.position;
			//Jellyfish.JellyToGoal ();
			triggerLight.SwitchLightOn ();
			//GoalRemove = true;
			Jellyfish.inGoal = true;
			ContainItem.containItemCount += 1;
			counterJelly--;
			Scr_Soundmanager.Sound.Play(DropJelly[Random.Range(0,DropJelly.Length)], gameObject, 0.3f, 0.5f, 0.8f, 1.2f);
		}
    }

    void WorldToLantern(GameObject item) {
		Firefly = item.GetComponent<Scr_Firefly> ();
		InLantern.Add(ActiveItemLantern);
		item.GetComponentInChildren<Animator>().SetBool("WithinLantern", true);
		item.GetComponentInChildren<Animator>().SetBool("WithinGoal", false);
        item.tag = "Untagged";
        item.GetComponentInChildren<SkinnedMeshRenderer>().enabled = false;
        item.GetComponentInChildren<ParticleSystem>().Stop();
        item.transform.parent = Lantern;
        item.transform.position = Lantern.position;
        item.GetComponent<SphereCollider>().enabled = false;
		Firefly.PlayMusic();
		if (Firefly.inGoal && ContainItem.containItemCount > 0) {
			if(Firefly.inDouble){
				Firefly.inDouble = false;
				doubleLight.counter -=1;
				doubleLight.DoubleOnOff();
			}
			else{
				triggerLight.SwitchLightOff ();
			}
			// stop swimming as soon as pick up
			ContainItem.containItemCount -= 1;
			GetComponent<Scr_PlayerInput> ().DisableSwim ();
			Firefly.inGoal = false; 
		}
		Scr_Soundmanager.Sound.Play(PickUpFirefly[Random.Range(0,PickUpFirefly.Length)], gameObject, 0.3f, 0.5f, 0.8f, 1.2f);

		ActiveItemLantern = null;
		LanternFade();

    }

    void LanternToWorld(GameObject item) {
		Firefly = item.GetComponent<Scr_Firefly> ();
		item.GetComponentInChildren<Animator>().SetBool("WithinLantern", false);
		item.GetComponentInChildren<Animator>().SetBool("WithinGoal", false);
		item.GetComponentInChildren<SkinnedMeshRenderer>().enabled = true;
		item.GetComponentInChildren<ParticleSystem>().Play();
        item.tag = "ItemLantern";
        item.GetComponent<SphereCollider>().enabled = true;
        item.transform.parent = null;
        item.transform.rotation = Quaternion.identity;
		InLantern.RemoveAt(0);
		Scr_Soundmanager.Sound.Play(DropFirefly[Random.Range(0,DropFirefly.Length)], gameObject, 0.3f, 0.5f, 0.8f, 1.2f);
		Firefly.PlayMusic();
		LanternFade();

    }

	void LanternToLight(GameObject item){
		if (item.GetComponent<Scr_Firefly> () != null && ContainItem.containItemCount < 1) {
			item.GetComponentInChildren<Animator>().SetBool("WithinLantern", false);
			item.GetComponentInChildren<Animator>().SetBool("WithinGoal", true);
			item.GetComponentInChildren<SkinnedMeshRenderer>().enabled = true;
			item.GetComponentInChildren<ParticleSystem>().Play();
			Firefly = item.GetComponent<Scr_Firefly>();
			item.GetComponent<SphereCollider> ().enabled = true;
			item.tag = "ItemLantern";
			item.transform.parent = null;
			Firefly.Goal = Goal;
			item.transform.position = Goal.position;
			//Firefly.FireflyToGoal ();
			triggerLight.SwitchLightOn ();
			//GoalRemove = true;
			Firefly.inGoal = true;
			ContainItem.containItemCount += 1;
			item.transform.rotation = Quaternion.identity;
			InLantern.RemoveAt(0);
			Scr_Soundmanager.Sound.Play(DropFirefly[Random.Range(0,DropFirefly.Length)], gameObject, 0.3f, 0.5f, 0.8f, 1.2f);
			Firefly.PlayMusic();
			LanternFade();

		}
	}

	void LanternToDoubleLight(GameObject item){
		if (item.GetComponent<Scr_Firefly> () != null && ContainItem.containItemCount < 1) {
			item.GetComponentInChildren<Animator>().SetBool("WithinLantern", false);
			item.GetComponentInChildren<Animator>().SetBool("WithinGoal", true);
			item.GetComponentInChildren<SkinnedMeshRenderer>().enabled = true;
			item.GetComponentInChildren<ParticleSystem>().Play();
			Firefly = item.GetComponent<Scr_Firefly>();
			item.GetComponent<SphereCollider> ().enabled = true;
			item.tag = "ItemLantern";
			item.transform.parent = null;
			Firefly.Goal = Goal;
			item.transform.position = Goal.position;
			//Firefly.FireflyToGoal ();
			Firefly.inGoal = true;
			Firefly.inDouble = true;
			doubleLight.counter += 1;
			doubleLight.DoubleOnOff ();
			ContainItem.containItemCount += 1;
			item.transform.rotation = Quaternion.identity;
			InLantern.RemoveAt(0);
			Scr_Soundmanager.Sound.Play(DropFirefly[Random.Range(0,DropFirefly.Length)], gameObject, 0.3f, 0.5f, 0.8f, 1.2f);
			Firefly.PlayMusic();
			LanternFade();

		}
	}

	void LanternFade(){

		if ( InLantern.Count < 1){

			fadeLanternTime = Time.time;
		}
		else {

			fadeLanternTime = float.MaxValue;
			lanternImage.color = new Color( lanternImage.color.r, lanternImage.color.g, lanternImage.color.b, 0.5f);
			faderLantern = 0.5f;
		}

	}
}
