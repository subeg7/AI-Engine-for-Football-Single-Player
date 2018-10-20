
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class PlayerBehavior : MonoBehaviour {

	private Vector3 screenPoint;
	private Vector3 offset;
	private GameObject ball;
	public float  initialYPos;
	public float initialBallYpos;
	public bool amIServer;
	public bool isMovementAllowed;
	//public Transform State;
	public Transform attemptedState;
	public Plane plane;
	public bool hasBall;
	public Material material,ballMaterial;
	public LineRenderer line;
	private GameObject message;
	private Vector3 ballPos;


	void Start(){

		hasBall = false;
		if (transform.name == "ServerPlayer0") {
			hasBall = true;
		}
		isMovementAllowed = false;
		attemptedState = new GameObject ("attemptedState").transform;
		attemptedState.position = this.transform.position;

		GameObject ground = GameObject.Find ("Grass");
		Vector3 groundPos = ground.transform.position;
		plane = new Plane(Vector3.up,groundPos);
		ball = GameObject.Find ("Ball");
		initialBallYpos = ball.transform.position.y;


		line  =this.GetComponent<LineRenderer>();
		line.material = new Material (Shader.Find("Particles/Additive"));

		line.startWidth = 0.5f;
		line.endWidth = 0.1f;
		line.SetPosition(0,this.transform.position);
		line.SetPosition(1,this.transform.position);


		message = GameObject.Find ("Message");
//		message = GameObject.Find("Message");


	}


	public void ClearLine(){

		line.SetPosition(0,this.transform.position);
		line.SetPosition(1,this.transform.position);

	}

	void OnMouseDrag(){

		if (isMovementAllowed) {
			
			Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
			float distance;
			if (plane.Raycast (ray, out distance)) {
				Vector3 cursorPosition = ray.GetPoint (distance);

				if (this.hasBall) {
					ballPos = new Vector3 (cursorPosition.x, initialBallYpos, cursorPosition.z);


					line.startColor = Color.blue;
					line.endColor = Color.blue;

					line.SetPosition (0, this.transform.position);
					line.SetPosition (1, cursorPosition);

					message.GetComponent<Text>().text="moving the ball";


				} else {
					Vector3 correctPos = new Vector3 (cursorPosition.x, initialYPos, cursorPosition.z);
					attemptedState.position = correctPos;

					if (amIServer) {
						line.startColor = Color.white;
						line.endColor = Color.white;

					} else {
						line.startColor = Color.gray;
						line.endColor = Color.gray;

					}

					line.SetPosition (0, this.transform.position);
					line.SetPosition (1, cursorPosition);//end

					message.GetComponent<Text>().text="moving the player";

	
				}

			}

		} else {
			message.GetComponent<Text>().text="you can't move your opponent";
	}



}



}