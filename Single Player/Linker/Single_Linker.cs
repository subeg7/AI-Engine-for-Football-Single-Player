using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking;
using UnityEngine;
using System;


public class Single_Linker : MonoBehaviour {
	
	// Use this for initialization
	private GameObject[] server;
	private GameObject[] client;
	private Transform[] stateServer;
	private Transform[] stateClient;

	//public GameObject inst;


	void Start () {
	print ("Single_linker started");


//		inst = GameObject.Find ("Inst");
		//enabling respective player controls
		server = new GameObject[5];
		client = new GameObject[5];
		stateServer = new Transform[5];
		stateClient = new Transform[5];


		for (int i = 0; i < 5; i++) {
			server [i] = GameObject.Find ("ServerPlayer" + i);
			client [i] = GameObject.Find ("ClientPlayer" + i);

			GameObject eMessage = GameObject.Find ("EngineMessage");
			GameObject pMessage = GameObject.Find ("Message");

//			if (isServer) {
//				//Server Sever Server
//				client [i].GetComponent<PlayerBehaviour> ().isMovementAllowed = false;
//				server [i].GetComponent<PlayerBehaviour> ().isMovementAllowed = true;
//
//
//				server [i].GetComponent<PlayerBehaviour> ().amIServer = true;
//				client [i].GetComponent<PlayerBehaviour> ().amIServer = false;
//
//				controller.GetComponent<SessionManager> ().oppTeam = "ClientPlayer";
//				controller.GetComponent<SessionManager> ().amIServer = true;
//				controller.GetComponent<Execution> ().amIServer = true;
//				controller.GetComponent<Engine> ().enabled = true;
//
//				//UI
//
//				eMessage.GetComponent<EngineUI> ().Display ("Online");
//				pMessage.GetComponent<MessageUI> ().Message.color = Color.yellow;
//				pMessage.GetComponent<MessageUI> ().Display ("you are YELLOW. Good Luck");
//


//			} else {
				//Client Client Client
				//print ("I am a client");
				client [i].GetComponent<PlayerBehaviour> ().isMovementAllowed = true;
				server [i].GetComponent<PlayerBehaviour> ().isMovementAllowed = true;


			GameObject controller = GameObject.Find ("Single_GameController");
			controller.GetComponent<Single_SessionManager> ().toStartCount = true;

				server [i].GetComponent<PlayerBehaviour> ().amIServer = true;
				client [i].GetComponent<PlayerBehaviour> ().amIServer = false;


//				controller.GetComponent<SessionManager> ().oppTeam = "ServerPlayer";
//				controller.GetComponent<SessionManager> ().amIServer = false;
//				controller.GetComponent<Execution> ().amIServer = false;

				//UI
				eMessage.GetComponent<EngineUI> ().Display ("Offline");
//				pMessage.GetComponent<MessageUI> ().Message.color = Color.blue;
//				pMessage.GetComponent<MessageUI> ().Display ("you are BLUE. Good Luck");
//
//			}
		}
	}


	public 	void Send()
	{
		//print ("Invoking Command");
		GameObject ball = GameObject.Find ("Ball");
		bool isbChanged = ball.GetComponent<BallBehaviour> ().hasBallmoved;
//		if (!isbChanged)
//			ball.GetComponent<BallBehaviour> ().attemptedPos = ball.transform.position;
		Vector3 serverBallPos = Vector3.back;
		Vector3 clientBallPos = Vector3.back;

//		if (isClient) {

//		send client info
			for (int i = 0; i < 5; i++) {
				stateClient[i]= client[i].GetComponent<PlayerBehaviour>().attemptedState;
			}
//
			if (!ball.GetComponent<BallBehaviour> ().isBallWithServer ) {
//				print ("Linker....ball is with CLIENT");
				clientBallPos =ball.GetComponent<BallBehaviour> ().attemptedPos;
				serverBallPos = Vector3.back;
//				print ("ballplayerIndex in LInker Before sending:" + ball.GetComponent<BallBehaviour> ().ballPlayerIndex);
			}
//
		GameObject Engine = GameObject.Find ("Single_GameController");
		Engine.GetComponent<Single_Engine> ().ClientPosition (stateClient[0].transform.position, stateClient[0].transform.rotation,
			stateClient[1].transform.position, stateClient[1].transform.rotation,
			stateClient[2].transform.position, stateClient[2].transform.rotation,
			stateClient[3].transform.position, stateClient[3].transform.rotation,
			stateClient[4].transform.position, stateClient[4].transform.rotation,
			clientBallPos,isbChanged


		);
//			
//			CmdSendToEngine (state[0].transform.position, state[0].transform.rotation,
//				state[1].transform.position, state[1].transform.rotation,
//				state[2].transform.position, state[2].transform.rotation,
//				state[3].transform.position, state[3].transform.rotation,
//				state[4].transform.position, state[4].transform.rotation,
//				ballPos,isbChanged
//			
//			);
//		}
//		else {
			//Server to ServerEngine

			//Send server Info
			for (int i = 0; i < 5; i++) {
				stateServer [i] = server[i].GetComponent<PlayerBehaviour>().attemptedState;

			}

			if (ball.GetComponent<BallBehaviour> ().isBallWithServer) {
//				print ("Linker....ball is with Server");
				serverBallPos =ball.GetComponent<BallBehaviour> ().attemptedPos;
				clientBallPos = Vector3.back;
//				print ("ballplayerIndex in LInker Before sending:" + ball.GetComponent<BallBehaviour> ().ballPlayerIndex);

			}


		Engine.GetComponent<Single_Engine> ().ServerPosition (stateServer[0].transform.position, stateServer[0].transform.rotation,
			stateServer[1].transform.position, stateServer[1].transform.rotation,
			stateServer[2].transform.position, stateServer[2].transform.rotation,
			stateServer[3].transform.position, stateServer[3].transform.rotation,
			stateServer[4].transform.position, stateServer[4].transform.rotation,
				serverBallPos,isbChanged


			);

//		
	}


	//From Engine to the Linker ulitmately to the client
	public void RecieveEngineOutput(Vector3[] serverUpdate , Vector3[] clientUpdate,Vector3 bPos,int plIndex,int[] score){
//		RpcClientEngineOutput (serverUpdate,clientUpdate,bPos,plIndex,score);
		GameObject controller = GameObject.Find ("Single_GameController");
		controller.GetComponent<Single_Execution> ().Execute (serverUpdate,clientUpdate,bPos,plIndex,score);

	}
//	[ClientRpc]
//	public void RpcClientEngineOutput(Vector3[] serverUpdate ,Vector3[] clientUpdate,Vector3 bPos,int plIndex, int[] score){
//		GameObject controller = GameObject.Find ("GameController");
//		controller.GetComponent<Execution> ().Execute (serverUpdate, clientUpdate,bPos,plIndex,score);
//	}
}
