using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZapOut_DomeLerping : MonoBehaviour {

	// visual functionality of the dome that zap casts with his power.
	// this script fades the renderer material and sets of an event during so.

	// Fade-in/out speeds
	public float speed = 1f;
	private float _speed;
    private float _speed2;
	
	// GameObjects
	public GameObject dome_inner;
	public GameObject dome_outer;
	
	// Renderer related
	private Renderer rend_inner;
	private Renderer rend_outer;
	private Color color;
	
	private float innerFull = 0.3176f;
	private float outerFull = 0.02f;
	private float innerAlpha = 0f;
	private float outerAlpha = 0f;
	
	// Event
    public delegate void SoundEvent(float t, Transform v);
    public static event SoundEvent EventDome;
	
	private bool active = false;
    private float soundVolume = 0f;

    
	private void Awake()		// get components, set variables
	{
		rend_inner = dome_inner.GetComponent<Renderer>();
		rend_outer = dome_outer.GetComponent<Renderer>();
		
		_speed = speed;
	}
	/*private void OnEnable()		// subscribe to events
	{
		SM.PlayUpdate += _Update;
		SM.DialogueUpdate += _Update;
	}
	private void OnDisable()	// unsubscribe from events
	{
		SM.PlayUpdate -= _Update;
		SM.DialogueUpdate -= _Update;
	}*/
	public void Activate()		// fade-in dome
	{
		active = true;
		_speed = speed;
        _speed2 = speed/4f;
	}
	public void Deactivate()	// fade-out dome
	{
		active = false;
		_speed = speed/3f;
        _speed2 = _speed;
	}
	void _Update()
	{
		// inner dome
		innerAlpha = Mathf.MoveTowards(innerAlpha, active == true? innerFull : 0f,innerFull*Time.deltaTime*_speed2);
		color = new Color(1f,0.31f,0f,innerAlpha);
		rend_inner.material.SetColor("_TintColor",color);
		// outer dome
		outerAlpha = Mathf.MoveTowards(outerAlpha, active == true? outerFull : 0f,outerFull*Time.deltaTime*_speed);
		rend_outer.material.SetVector ("_InvFadeParemeter",new Vector4(outerAlpha,1.36f,2f,2.16f));
		
		// destroy
		if(active == false && innerAlpha == 0f && outerAlpha == 0f)
			Destroy (this.gameObject);
		
		// sound event
        soundVolume = Mathf.MoveTowards(soundVolume, active == true ? 1f : 0f,Time.deltaTime * _speed);

		if (EventDome != null)
            EventDome(soundVolume,this.transform);
	}
}
