using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using System;

public class SessionManager : MonoBehaviour {

	// Use this for initialization
	public int sessionTime = 15;


	public string timeUI;
	public bool toStartCount;
	public string oppTeam;
	public bool amIServer;
	public Text timeRem;

	private bool isTimeUp;
	private int sessionCount;
	private bool hasSent ;
	private GameObject[] server;
	private GameObject[] client;
	private float timeLeft;

	void Start () {
		toStartCount = false;
		sessionCount = 1;
		timeUI = "Game Hasn't Started Yet";
		isTimeUp = false;
		hasSent = false;

		server = new GameObject[5];
		client = new GameObject[5];
		timeLeft = (float)sessionTime;


		for (int i = 0; i < 5; i++) {
			server[i]=GameObject.Find("ServerPlayer"+i);
			client[i]=GameObject.Find("ClientPlayer"+i);

		}
	}
	
	// Update is called once per frame
	void Update(){


		//if time Ended	
		if (!hasSent && toStartCount) {
			if (isTimeUp) {
				


				timeUI = "Session:"+sessionCount+"Ended";	
				toStartCount = false;
				hasSent = true;

				for (int i = 0; i < 5; i++) {
					client[i].GetComponent<PlayerBehaviour> ().isMovementAllowed = false;
					server[i].GetComponent<PlayerBehaviour> ().isMovementAllowed = false;


					server [i].GetComponent<PlayerBehaviour> ().ClearLine ();
					client [i].GetComponent<PlayerBehaviour> ().ClearLine ();


				}

				GameObject linker = GameObject.FindWithTag("Linker");
				linker.GetComponent<Linker>().Send();

			} else {
				timeLeft -= Time.deltaTime;

				timeUI = sessionCount+":" + (int)timeLeft;
				if (timeLeft<0) {
					isTimeUp = true;
				}



			}

		}
	}

	public void StartNextSession(){
		hasSent = false;
		toStartCount = true;
		isTimeUp = false;
		sessionCount++;
		//print ("activating movements for next session");
		for (int i = 0; i < 5; i++) {


			if (amIServer) {
				//Server Sever Server
				client [i].GetComponent<PlayerBehaviour> ().isMovementAllowed = false;
				server [i].GetComponent<PlayerBehaviour> ().isMovementAllowed = true;

			} else {
				//Client Client Client
				client [i].GetComponent<PlayerBehaviour> ().isMovementAllowed = true;
				server [i].GetComponent<PlayerBehaviour> ().isMovementAllowed = false;


			}
		}
		timeLeft = (float)sessionTime;
	}


}
