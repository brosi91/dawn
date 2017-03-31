using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scr_PlayerInput : MonoBehaviour {

	public bool Swim;
	public Transform cameraTarget;
	public Transform Hand;

	private Scr_Character m_Character; // A reference to the ThirdPersonCharacter on the object
    private Vector3 m_CamForward;             // The current forward direction of the camera
    private Vector3 m_Move;
    private bool m_Jump;                      // the world-relative desired move direction, calculated from the camForward and user input.


	//UI shizzle
    public GameObject Canvas;

    
    private void Start()
    {
        m_Character = GetComponent<Scr_Character>();
    }


    private void Update()
    {
        if (!m_Jump)
        {
            m_Jump = Input.GetAxis("Jump")>0.5f;
        }

		if (Input.GetKey(KeyCode.P)){
			Canvas.SetActive(true);
			m_Character.Move(Vector3.zero, false, false);
			GetComponent<Rigidbody>().velocity = Vector3.zero;
			ToggleActive();
		}

    }


    // Fixed update is called in sync with physics
    private void FixedUpdate()
    {
        // read inputs
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");
        bool crouch = Input.GetKey(KeyCode.C);

		// walk speed multiplier
	    if (Input.GetKey(KeyCode.LeftShift)) m_Move *= 0.5f;

        if(Swim){
			// calculate camera relative direction to move while Swimming:
			m_CamForward = cameraTarget.forward;	//übernimmt jegliche Richtungen von cameraTarget
  		    m_Move = v*m_CamForward + h*cameraTarget.right;

			// pass all parameters to the character control script
        	m_Character.Swim(m_Move);
        }
        else{
			// calculate camera relative direction to move while on Floor:
      		m_CamForward = Vector3.Scale(cameraTarget.forward, new Vector3(1, 0, 1)).normalized; //ignoriert Auf&Ab bewegung von cameraTarget
       		m_Move = v*m_CamForward + h*cameraTarget.right;

			// pass all parameters to the character control script
			m_Character.Move(m_Move, crouch, m_Jump);
        }

        m_Jump = false;
    }

	void OnTriggerStay(Collider other) {
		if (other.gameObject.tag == "Light"){
			m_Character.SetSwim(true);
			Swim = true;
		}
    }

    void OnTriggerExit(Collider other){
    if(other.gameObject.tag == "Light"){
	    	DisableSwim();
	    	m_Character.DolphinJump(m_Move);
    	}
    }

    public void DisableSwim(){
		m_Character.SetSwim(false);
	    Swim = false;
    }

    public void ToggleActive(){
		enabled = !enabled;
    }
}