
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerBehaviour : MonoBehaviour {

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
	private GameObject messageUI;


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


		messageUI = GameObject.Find ("Message");

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
					Vector3 correctPos = new Vector3 (cursorPosition.x, initialBallYpos, cursorPosition.z);
					ball.GetComponent<BallBehaviour> ().attemptedPos = correctPos;
					ball.GetComponent<BallBehaviour> ().hasBallmoved = true;


					line.startColor = Color.blue;
					line.endColor = Color.blue;

					line.SetPosition (0, this.transform.position);
					line.SetPosition (1, cursorPosition);

					messageUI.GetComponent<MessageUI> ().Display ("Could be a great Pass");


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

					messageUI.GetComponent<MessageUI> ().Display ("This moves looks a real danger");

	
				}

			}

		} else {
			messageUI.GetComponent<MessageUI> ().Display ("Hey!! you can't move this player");
	}



}



}