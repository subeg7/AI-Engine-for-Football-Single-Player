using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;


public class Execution:MonoBehaviour {
	private GameObject[] server;
	private GameObject[] client;
	private GameObject ball;
	private bool isBallWithServer;
	public int ballPlayerIndexPrev;
	private int ballPlayerIndexNew;
	private int[] scorePrev;
	private int[] scoreNew;

	public bool amIServer;
	public float anTime;
	private bool isAnimationEnabled;


	private Vector3[] newServerPos;
	private Vector3[] newClientPos;
	private Vector3[] prevServerPos;
	private Vector3[] prevClientPos;

	private float[] xServerVel;
	private float[] zServerVel;

	private float[] xClientVel;	
	private float[] zClientVel;

	private Vector3 prevBallPos;
	private Vector3 newBallPos;

	public  float xBallVel;
	public float zBallVel;

	public GameObject instServer;

	public GameObject instClient;
	public GameObject instClient1;
	public GameObject instClient2;
	public GameObject instClient3;
	public GameObject instClient4;

	private bool[] isClientAnimated;
	private bool[] isServerAnimated;
	private bool isBallAnimated;



	void Start(){

		isClientAnimated = new bool[5];
		isServerAnimated = new bool[5];

		server = new GameObject[5];
		client = new GameObject[5];


		xServerVel = new float[5];
		zServerVel = new float[5];

		xClientVel = new float[5];
		zClientVel = new float[5];


		newServerPos = new Vector3[5];
		newClientPos = new Vector3[5];

		prevServerPos = new Vector3[5];
		prevClientPos = new Vector3[5];

		ball = GameObject.Find ("Ball");
		Vector3 ballInitPos= ball.transform.position;

		prevBallPos = ballInitPos;

		for (int i = 0; i < 5; i++) {
			server[i]=GameObject.Find("ServerPlayer"+i);
			prevServerPos [i] = server [i].transform.position;
			client[i]=GameObject.Find("ClientPlayer"+i);
			prevClientPos [i] = client [i].transform.position;


			//			instServer.transform.position = server [i].transform.position;
			//			Instantiate (instServer);
			//			instClient.transform.position = client [i].transform.position;
			//			Instantiate (instClient);

		}


		//					instServer.transform.position = server [1].transform.position;
		//		Instantiate (instServer);
		//
		//					instClient.transform.position = client [1].transform.position;
		//		Instantiate (instClient);


		isBallWithServer = false;
		scorePrev = new int[2];
		scoreNew = new int[2];

		isAnimationEnabled = false;



	}


	public void Execute(Vector3[] sPos,Vector3[] cPos,Vector3 bPos,int plIndex,int[] score){
		//		print ("Inside the execution function");
		//update the new position of ball and player
		ballPlayerIndexNew=plIndex;
		scoreNew = score;
		//		instClient.transform.position = cPos[0];
		//		instClient1.transform.position = cPos[1];
		//		instClient2.transform.position = cPos[2];
		//		instClient3.transform.position = cPos[3];
		//		instClient4.transform.position = cPos[4];


		//		instClient.transform.position = sPos [1];

		if (plIndex > 4)
			newBallPos = cPos [plIndex - 5];
		else
			newBallPos = sPos [plIndex];

		//		instServer.transform.position = newBallPos;
		//newBallPos=bPos;


		for (int i = 0; i < 5; i++) {
			newServerPos [i] = sPos [i];
			newClientPos [i] = cPos [i];

			//	instServer.transform.position = sPos[1];
			//Instantiate (instServer);
			//	instClient.transform.position = cPos[1];
			//Instantiate (instClient);

			//			client[i].transform.position=cPos[i];
			//			server[i].transform.position=sPos[i];
			//
			//
			//
		}
		//print ("attempting animation");
		InitVelocity ();

	}


	void InitVelocity(){
		for (int i = 0; i < 5; i++) {

			xServerVel[i] = (newServerPos[i].x- prevServerPos[i].x) / anTime;
			zServerVel[i] = (newServerPos[i].z - prevServerPos[i].z) / anTime;

			xClientVel[i] = (newClientPos[i].x- prevClientPos[i].x) / anTime;
			zClientVel[i] = (newClientPos[i].z - prevClientPos[i].z) / anTime;

		}

		xBallVel = (newBallPos.x - prevBallPos.x) / anTime;
		zBallVel = (newBallPos.z - prevBallPos.z) / anTime;


		//instClient.transform.position = client [1].transform.position;
		isAnimationEnabled = true;


	}


	void Update(){

		if (isAnimationEnabled) {
			//			print ("animating");	
			const float near = 1f;
			//	timeUI = "Session:"+sessionCount+"Ended";	
			for (int i = 0; i < 5; i++) {
				//animate server
				if (Vector3.Distance (server [i].transform.position, newServerPos [i]) >= near) {
					isServerAnimated [i] = false;
					server [i].transform.Translate (xServerVel [i], 0, zServerVel [i]);

				} else {
					isServerAnimated [i] = true;
					//print (server [i].transform.name + " has halted");
				}

				//animate client
				if (Vector3.Distance (client [i].transform.position, newClientPos [i]) >= near) {
					isClientAnimated [i] = false;
					client [i].transform.Translate (xClientVel [i], 0, zClientVel [i]);

				} else {
					isClientAnimated [i] = true;
					//	print (server [i].transform.name + " has halted");
				}
			}

			//animate ball
			if (Vector3.Distance (ball.transform.position, newBallPos) >= near/1.5) {
				isBallAnimated = false;
				ball.transform.Translate (xBallVel, 0, zBallVel);
			} else {
				isBallAnimated = true;
			}


			//turn UPDATE animation off
			if (isBallAnimated && isClientAnimated [0] && isClientAnimated [1] && isClientAnimated [2] && isClientAnimated [3] && isClientAnimated [4]) {
				if (isServerAnimated [0] && isServerAnimated [1] && isServerAnimated [2] && isServerAnimated [3] && isServerAnimated [4]) {
					//stop  the animation update
					isAnimationEnabled = false;
					ProceedForNextSession ();
				}
			} 


		} else {
			//			print ("animation is disabled now");
		}

	}



	void ProceedForNextSession(){

		//set the current position ;
		for (int i = 0; i < 5; i++) {
			server [i].GetComponent<PlayerBehaviour> ().attemptedState.transform.position = server [i].transform.transform.position;
			client [i].GetComponent<PlayerBehaviour> ().attemptedState.transform.position =client [i].transform.transform.position;
		}

		ball.GetComponent<BallBehaviour> ().attemptedPos = ball.transform.position;
		ball.GetComponent<BallBehaviour> ().ballPlayerIndexPrev = ball.GetComponent<BallBehaviour> ().ballPlayerIndex;
		ball.GetComponent<BallBehaviour> ().ballPlayerIndex = ballPlayerIndexNew;


		//score
		//		if (amIServer) {
		GameObject scoreUI = GameObject.Find ("ScoreBoard");
		scoreUI.GetComponent<ScoreUI> ().score.text = "you :  [" + scoreNew [0] + "-" + scoreNew [1] + "]   : friend";
		//		} else {
		//			GameObject scoreUI = GameObject.Find ("ScoreBoard");
		//			scoreUI.GetComponent<ScoreUI> ().score.text = "you :  [" + scoreNew [1] + "-" + scoreNew[0] + "]  : friend";
		//		}
		//		print ("PlayeIndex on Execution:"+plIndex);


		//updating prev values
		for (int i = 0; i < 5; i++) {
			prevServerPos [i] = newServerPos [i];

			prevClientPos [i] = newClientPos [i];


		}

		prevBallPos = newBallPos;
		ballPlayerIndexPrev = ballPlayerIndexNew;
		scorePrev = scoreNew;
		//isAnimationEnabled = false;
		//Command Session Manager to start next session
		ball.GetComponent<BallBehaviour> ().setBallPlayer (ballPlayerIndexNew);
		this.GetComponent<SessionManager> ().StartNextSession ();	
	}

}





