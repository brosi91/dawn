using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scr_PlayerController : MonoBehaviour {

	public float WalkSpeed;
	public float TurnSpeed;

	// Das Objekt zu dessen Ausrichtung man sich relativ Bewegen soll.
	public Transform MovementSpace;
	// Das CameraTarget Objekt
	public Transform CameraTarget;


	Rigidbody rig;





	void Awake () {
		rig = GetComponent<Rigidbody>();
	}


	void FixedUpdate() {
		MovementSpace.rotation = Quaternion.Euler(0, CameraTarget.rotation.eulerAngles.y, 0);

		float MoveX = Input.GetAxis("Horizontal") * WalkSpeed;
		float MoveZ = Input.GetAxis("Vertical") * WalkSpeed;

		//Bewegt sich um Vector3 relativ zu MovementSpace(rotiert um y achse)
		transform.Translate(new Vector3 (MoveX, 0, MoveZ), MovementSpace);

		//rig.MovePosition(MovementSpace.TransformDirection(new Vector3 (MoveX, 0, MoveZ) + transform.position));
		}

}
