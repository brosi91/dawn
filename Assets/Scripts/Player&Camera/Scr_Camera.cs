using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scr_Camera : MonoBehaviour {

public float MaxDistance;	//Maximale Diszanz von Kamera zum Target
public float MinDistance;	//Minimale Distanz von Kamera zum Target
public float StartMaxDistance; //Ursprünglicher Maximal Abstand
public float LookSpeed;		//Geschwindigkeit Kamerabewegung
public float MaxAngle;		//Höchster Winkel auf Figur
public float MinAngle;		//Tiefster Winkel auf Figur
public float ZoomSpeed;
public float LerpSpeed;

public Transform Player;

public Transform camera;

private MyPlayerAction characterActions;


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

	void Awake(){
		//camera = GetComponentInChildren<Camera>().transform;
		StartMaxDistance = MaxDistance;
		}

	void Update (){
		//transform.position = Player.position;
	}

	void FixedUpdate(){

		//Abstand der Kamera zum Target

		//Speicherort von Raycast-Daten
		RaycastHit hit; 
		//Vector3.back wäre von Worldspace, durch transform.TransformDirection wird es Local-Sapce = immer Rückwärts vom Objekt aus, "out hit" wird im RaycastHit gespeichert
		// RayCast(eigenePosition, richtung nach hinten, speichern im hit, länge von ray)

		Vector3 target;

		if(Physics.Raycast(transform.position, transform.TransformDirection(Vector3.back), out hit, MaxDistance)){

			if(hit.distance > MinDistance){
				target = hit.point;
			}
			else {
				target = transform.position + transform.TransformDirection(Vector3.back)*MinDistance;
			}
		} 

		else {
			target = transform.position + transform.TransformDirection(Vector3.back)*MaxDistance;
		}

		if(Vector3.Distance(camera.position,target)> 0){
				camera.position = Vector3.MoveTowards(camera.position, target, LerpSpeed);
			}

		MaxDistance = Mathf.Clamp(MaxDistance-(characterActions.lookZoomForward - characterActions.lookZoomBackward)*ZoomSpeed, MinDistance, StartMaxDistance);


		// Rotation der Kamera um das Target

		// Die Aktuelle Rotation in EulerAngles
		Vector3 euler = transform.rotation.eulerAngles;
		// Die Rotation um die Y Achse + der Mouse X Input
		float LookX = euler.y + characterActions.lookVector2.X* LookSpeed;
		// Die Rotation um die X Achse + der Mouse Y Input
		float LookY = Mathf.Clamp( TrueAngle(euler.x - characterActions.lookVector2.Y* LookSpeed), MinAngle, MaxAngle);
		// Anwednung der zuvor definierten Werte auf die Rotation
		transform.rotation = Quaternion.Euler(LookY, LookX, 0);
	}


	// Diese Funktion gibt ein Float zurück der von 0 bis 360 in 180 bis -180 umgewandelt wird
	float TrueAngle(float Angle){
		if(Angle > 180f){
			float newAngle = Angle -360f;
			return newAngle;
		}
		else{
			return Angle;
		}

	}

}