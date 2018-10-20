using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking;
using UnityEngine;
using System;


public class Linker : NetworkBehaviour {
	
	// Use this for initialization
	private GameObject[] server;
	private GameObject[] client;
	private Transform[] state;

	//public GameObject inst;


	void Start () {
//		print ("linker started");
		GameObject controller = GameObject.FindGameObjectWithTag ("GameController");
		controller.GetComponent<SessionManager> ().toStartCount = true;


//		inst = GameObject.Find ("Inst");
		//enabling respective player controls
		server = new GameObject[5];
		client = new GameObject[5];
		state = new Transform[5];


		for (int i = 0; i < 5; i++) {
			server [i] = GameObject.Find ("ServerPlayer" + i);
			client [i] = GameObject.Find ("ClientPlayer" + i);

			GameObject eMessage = GameObject.Find ("EngineMessage");
			GameObject pMessage = GameObject.Find ("Message");

			if (isServer) {
				//Server Sever Server
				client [i].GetComponent<PlayerBehaviour> ().isMovementAllowed = false;
				server [i].GetComponent<PlayerBehaviour> ().isMovementAllowed = true;


				server [i].GetComponent<PlayerBehaviour> ().amIServer = true;
				client [i].GetComponent<PlayerBehaviour> ().amIServer = false;

				controller.GetComponent<SessionManager> ().oppTeam = "ClientPlayer";
				controller.GetComponent<SessionManager> ().amIServer = true;
				controller.GetComponent<Execution> ().amIServer = true;
				controller.GetComponent<Engine> ().enabled = true;

				//UI

				eMessage.GetComponent<EngineUI> ().Display ("Online");
				pMessage.GetComponent<MessageUI> ().Message.color = Color.yellow;
				pMessage.GetComponent<MessageUI> ().Display ("you are YELLOW. Good Luck");



			} else {
				//Client Client Client
				//print ("I am a client");
				client [i].GetComponent<PlayerBehaviour> ().isMovementAllowed = true;
				server [i].GetComponent<PlayerBehaviour> ().isMovementAllowed = false;

				server [i].GetComponent<PlayerBehaviour> ().amIServer = true;
				client [i].GetComponent<PlayerBehaviour> ().amIServer = false;


				controller.GetComponent<SessionManager> ().oppTeam = "ServerPlayer";
				controller.GetComponent<SessionManager> ().amIServer = false;
				controller.GetComponent<Execution> ().amIServer = false;

				//UI
				eMessage.GetComponent<EngineUI> ().Display ("Offline");
				pMessage.GetComponent<MessageUI> ().Message.color = Color.blue;
				pMessage.GetComponent<MessageUI> ().Display ("you are BLUE. Good Luck");

			}
		}
	}


	public 	void Send()
	{
		//print ("Invoking Command");
		GameObject ball = GameObject.Find ("Ball");
		bool isbChanged = ball.GetComponent<BallBehaviour> ().hasBallmoved;
//		if (!isbChanged)
//			ball.GetComponent<BallBehaviour> ().attemptedPos = ball.transform.position;
		Vector3 ballPos = Vector3.back;





		if (isClient) {
			for (int i = 0; i < 5; i++) {
				state [i]= client[i].GetComponent<PlayerBehaviour>().attemptedState;
			}

			if (!ball.GetComponent<BallBehaviour> ().isBallWithServer ) {
				ballPos =ball.GetComponent<BallBehaviour> ().attemptedPos;
//				print ("ballplayerIndex in LInker Before sending:" + ball.GetComponent<BallBehaviour> ().ballPlayerIndex);
			}


			CmdSendToEngine (state[0].transform.position, state[0].transform.rotation,
				state[1].transform.position, state[1].transform.rotation,
				state[2].transform.position, state[2].transform.rotation,
				state[3].transform.position, state[3].transform.rotation,
				state[4].transform.position, state[4].transform.rotation,
				ballPos,isbChanged
			
			);
		}
		else {
			//Server to ServerEngine

			for (int i = 0; i < 5; i++) {
				state [i] = server[i].GetComponent<PlayerBehaviour>().attemptedState;

			}

			if (ball.GetComponent<BallBehaviour> ().isBallWithServer) {
				ballPos =ball.GetComponent<BallBehaviour> ().attemptedPos;
//				print ("ballplayerIndex in LInker Before sending:" + ball.GetComponent<BallBehaviour> ().ballPlayerIndex);

			}


			GameObject Engine = GameObject.FindWithTag ("GameController");
			Engine.GetComponent<Engine> ().ServerPosition (state[0].transform.position, state[0].transform.rotation,
				state[1].transform.position, state[1].transform.rotation,
				state[2].transform.position, state[2].transform.rotation,
				state[3].transform.position, state[3].transform.rotation,
				state[4].transform.position, state[4].transform.rotation,
				ballPos,isbChanged


			);

		}


		//print ("Invoking attempt completed");

	}

	[Command]
	public void CmdSendToEngine(Vector3 x0,Quaternion y0,
		Vector3 x1,Quaternion y1,
		Vector3 x2,Quaternion y2,
		Vector3 x3,Quaternion y3,
		Vector3 x4,Quaternion y4,
		Vector3 bPos, bool isbChanged
	){
		

		GameObject Engine = GameObject.FindWithTag ("GameController");
		Engine.GetComponent<Engine> ().ClientPosition (x0,y0,x1,y1,x2,y2,x3,y3,x4,y4,bPos,isbChanged);

	}


	//From Engine to the Linker ulitmately to the client
	public void RecieveEngineOutput(Vector3[] serverUpdate , Vector3[] clientUpdate,Vector3 bPos,int plIndex,int[] score){
		RpcClientEngineOutput (serverUpdate,clientUpdate,bPos,plIndex,score);
		GameObject controller = GameObject.FindGameObjectWithTag ("GameController");
		controller.GetComponent<Execution> ().Execute (serverUpdate,clientUpdate,bPos,plIndex,score);

	}
	[ClientRpc]
	public void RpcClientEngineOutput(Vector3[] serverUpdate ,Vector3[] clientUpdate,Vector3 bPos,int plIndex, int[] score){
		GameObject controller = GameObject.FindGameObjectWithTag ("GameController");
		controller.GetComponent<Execution> ().Execute (serverUpdate, clientUpdate,bPos,plIndex,score);
	}
}
