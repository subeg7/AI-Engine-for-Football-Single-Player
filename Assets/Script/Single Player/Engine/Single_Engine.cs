using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Single_Engine : MonoBehaviour {

	//for instantation
	public GameObject inst;
	public GameObject proposedBall;
	public GameObject inst1;

	//Engine switching on and off  parameters
	private bool hasServerSent;
	private bool hasClientSent;
	private bool hasEngineStarted;


	//proposed tactics info
	private Vector3[] server;
	private Vector3[] client;
	private Vector3[] allPlayer;
	private Vector3[] serverStart;
	private Vector3[] clientStart;

	//previous position
	private Vector3[] serverPrev;
	private Vector3[] clientPrev;
	private Vector3 playerSize;
	private Vector3[] allPlayerPrev;
	public Vector3 startBallServer;
	public Vector3 startBallClient;



	//private int ballPlayerIndex;
	//All the information of the game
	private Plane plane;
	private float groundPlane;
	//	private

	private GameObject messageUI;

	public Vector3[] postVerticesLeft;
	public Vector3[] postVerticesRight;
	public int[] score;

	//engine flags
	private  bool wasLastGoal;



	//sub-engines
	private Single_BallEngine bEngine;
	void Start(){
		hasEngineStarted = false;
		wasLastGoal = false;

		//init new Position array
		server = new Vector3[5];
		client = new Vector3[5];
		serverStart = new Vector3[5];
		clientStart = new Vector3[5];

		//init sarting pos of players
		serverPrev = new Vector3[5];
		clientPrev = new Vector3[5];
		allPlayer = new Vector3[10];
		allPlayerPrev = new Vector3[10];

		for (int i = 0; i < 5; i++) {
			serverPrev[i] = GameObject.Find ("ServerPlayer" + i).transform.position;
			clientPrev [i] = GameObject.Find ("ClientPlayer"+i).transform.position;

			serverStart[i] = GameObject.Find ("ServerPlayer" + i).transform.position;
			clientStart[i] = GameObject.Find ("ClientPlayer"+i).transform.position;

			allPlayerPrev [i] = serverPrev [i];
			allPlayerPrev [i + 5] = clientPrev [i];
		}
		//init size of player
		GameObject sample = GameObject.Find ("ServerPlayer0");
		playerSize = sample.GetComponent<Collider> ().bounds.size;



		//init ground plane
		GameObject ground = GameObject.Find ("Grass");
		Vector3 groundPos = ground.transform.position;
		groundPlane = 0;//groundPos.y;
		plane = new Plane(Vector3.up,groundPos);

		bEngine = new Single_BallEngine ();
		bEngine.near = playerSize.x;
		bEngine.sourcePlayer = 0;
		bEngine.UpdateInfo ();



		messageUI = GameObject.Find ("EngineMessage");

		postVerticesRight = new Vector3[4];
		postVerticesLeft = new Vector3[4];

		for (int i = 0; i < 4; i++) {
			GameObject pointRight= GameObject.Find ("p1" + i);
			GameObject pointLeft = GameObject.Find ("p0" + i);

			postVerticesRight [i] = pointRight.transform.position;
			postVerticesLeft [i] = pointLeft.transform.position;
		}
		score = new int[2];
		score [0] =0;
		score [1] = 0;
		bEngine.clientScore = 0;
		bEngine.serverScore = 0;


		//initalize start pos for ball after each team scoring
		GameObject startClientBall = GameObject.Find ("ClientStartBall");
		startBallClient = startClientBall.transform.position;

		GameObject startServerBall = GameObject.Find ("ServerStartBall");
		startBallServer =startServerBall.transform.position;


	}

	public void ClientPosition(Vector3 cPos0,Quaternion cRot0,
		Vector3 cPos1,Quaternion cRot1,
		Vector3 cPos2,Quaternion cRot2,
		Vector3 cPos3,Quaternion cRot3,
		Vector3 cPos4,Quaternion cRot4,
		Vector3 bPos,bool isBallMoved

	){

		//		print ("ClientPosition Recieved in Engine");
		hasClientSent = true;

		client[0]=cPos0;
		client[1]=cPos1;
		client[2]=cPos2;
		client[3]=cPos3;
		client[4]=cPos4;

		client [4].y = client [3].y = client [2].y = client [1].y = client [0].y = -groundPlane;

		//		bEngine.hasBallMoved = false;
		//bEngine.hasBallMoved = isBallMoved;

		if (bPos != Vector3.back) {
			//			print ("Udating ball info form Client"+bPos);
			bEngine.targetBall = bPos;
			bEngine.isBallWithServer = false;
			bEngine.hasBallMoved = isBallMoved;
			print ("Engine>>>>Ball received from client");


		}

		for (int i = 0; i < 5; i++) {
			allPlayer [i + 5] = client [i];

			//			if (wasLastGoal) {
			//				inst.transform.position = allPlayer [i+5];
			//				Instantiate (inst);
			//			}

		}

	}


	public void ServerPosition(Vector3 cPos0,Quaternion cRot0,
		Vector3 cPos1,Quaternion cRot1,
		Vector3 cPos2,Quaternion cRot2,
		Vector3 cPos3,Quaternion cRot3,
		Vector3 cPos4,Quaternion cRot4,
		Vector3 bPos,bool isBallMoved
	){

		//		print ("ServerPosition Recieved in Engine");
		hasServerSent = true;

		server[0]=cPos0;
		server[1]=cPos1;
		server[2]=cPos2;
		server[3]=cPos3;
		server[4]=cPos4;

		if (bPos != Vector3.back) {
			bEngine.hasBallMoved = isBallMoved;
			print ("Engine>>>>Ball received from server");
			//print ("Udating ball info form Server: "+bPos);

			bEngine.targetBall = bPos;
			//			print ("hasBallmoved set in bENinge :" + bEngine.hasBallMoved);
			bEngine.isBallWithServer = true;

		}

		for (int i = 0; i < 5; i++) {
			server [i].y = -groundPlane;
			allPlayer [i] = server [i];


			//			if (wasLastGoal) {
			//				inst.transform.position = allPlayer [i];
			//				Instantiate (inst);
			//			}


		}


	}




	void Update(){

		if (hasServerSent && hasClientSent && !hasEngineStarted) {
			hasEngineStarted = true;

			//ballProcessing
			Vector3 accurateBallPos = Vector3.back;
			int passedPlayerIndex=bEngine.sourcePlayer;




			if (bEngine.hasBallMoved ) {
				passedPlayerIndex = bEngine.NearestPlayer (allPlayer);

				if (passedPlayerIndex > -1) {
					bEngine.targetPlayer = passedPlayerIndex;
					bEngine.targetBall = allPlayer [passedPlayerIndex];
					int intrIndex =  bEngine.LaneInterruption (allPlayer, passedPlayerIndex);


					if (intrIndex > -1) {
						bEngine.targetPlayer = intrIndex;
						print ("interupted by:" + intrIndex);
						messageUI.GetComponent<EngineUI> ().Display ("interupted by:" + intrIndex);

					} else {
						print ("No interruption on Pass");
						messageUI.GetComponent<EngineUI> ().Display ("No interruption on pass");

					}
					accurateBallPos = bEngine.LocateBall (allPlayer);
					//					print("Accurate Ball just sent from BallENgine:"+accurateBallPos);
				} else {

					if (bEngine.HasShoot (postVerticesLeft[0].x,postVerticesRight[0].x)) {
						messageUI.GetComponent<EngineUI> ().Display ("a power shot for Goal");



						int status = bEngine.GoalStatus (postVerticesLeft, postVerticesRight,allPlayer);
//						inst.transform.position = bEngine.testBall;

						//						for (int i = 0; i < 4; i++) {
						//							inst1.transform.position = bEngine.passZoneVertices [i];
						//
						//							Instantiate (inst1);
						//						}

						if (status == 0) {
							//Goal Scored
							score[0]= bEngine.serverScore;
							score[1]= bEngine.clientScore;

							//set the starting formation
							for (int i = 0; i < 5; i++) {
								server[i] = serverStart[i];
								client[i] = clientStart[i];
								wasLastGoal = true;
							}
							if (bEngine.hasServerScored) {
								//give ball to Clinet
								bEngine.targetPlayer = 5;
								accurateBallPos = startBallClient;
								wasLastGoal = true;


							} else {
								//start ball with Server
								bEngine.targetPlayer = 0;
								accurateBallPos = startBallServer;


							}
							messageUI.GetComponent<EngineUI> ().Display ("!!!Goal!!!");


							//reset all player position
						}else if(status == 1){
							//Goal saved or interrupted
							bEngine.targetPlayer = bEngine.interruptIndex;
							wasLastGoal = false;


							accurateBallPos = bEngine.LocateBall (allPlayer);
							messageUI.GetComponent<EngineUI> ().Display ("Worldclass  Save by Opponent:"+bEngine.interruptIndex);

						}else{
							//Shot not on target
							bEngine.targetPlayer = bEngine.sourcePlayer;
							accurateBallPos = bEngine.sourceBall;
							messageUI.GetComponent<EngineUI> ().Display ("Out of Target! Shoot again");
							wasLastGoal = false;
						}




						//bEngine.targetPlayer = bEngine.sourcePlayer;




					}else{

						print ("The ball is misspassed");
						messageUI.GetComponent<EngineUI> ().Display ("The ball is MissPassed");

						bEngine.targetPlayer = bEngine.sourcePlayer;
						//print("passedPlayerIndex  just after misspassing="
						accurateBallPos = bEngine.sourceBall;//this should be to the opponent later
					}
				}
			} else {
				Debug.Log ("No pass attempted");
				messageUI.GetComponent<EngineUI> ().Display ("NO pass is attempted");

				accurateBallPos = bEngine.targetBall;
				bEngine.targetPlayer = passedPlayerIndex;

			}




			//adjust values for next EngineProcess
			bEngine.sourcePlayer=bEngine.targetPlayer;
			bEngine.sourceBall = accurateBallPos;

			GameObject linker = GameObject.FindWithTag ("Single_Linker");
			linker.GetComponent<Single_Linker>().RecieveEngineOutput(server,client,accurateBallPos,bEngine.targetPlayer,score);
			bEngine.targetPlayer = -1;
			bEngine.targetBall =  new Vector3();

			hasClientSent = false;
			hasServerSent = false;
			hasEngineStarted = false;
		}

	}

}