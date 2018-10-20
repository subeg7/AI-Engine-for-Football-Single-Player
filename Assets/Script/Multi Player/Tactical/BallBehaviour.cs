using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallBehaviour : MonoBehaviour {


	public GameObject ballPlayer;
	public bool isBallWithServer;
	public GameObject passAttemptedTO;
	public Vector3 attemptedPos;

	public bool hasBallmoved;


	public int ballPlayerIndex;
	public int ballPlayerIndexPrev;
	// Use this for initialization
	void Start () {
		//print ("Ball start run");
		ballPlayer=GameObject.Find ("ServerPlayer0");
		ballPlayer.GetComponent<PlayerBehaviour> ().hasBall = true;
		isBallWithServer = true;

		attemptedPos = transform.position;
	}


	public void setBallPlayer(int index){
		for (int i = 0; i < 5; i++) {
			GameObject sPl = GameObject.Find ("ServerPlayer" + i);
			sPl.GetComponent<PlayerBehaviour> ().hasBall = false;

			GameObject cPl = GameObject.Find ("ClientPlayer" + i);
			cPl.GetComponent<PlayerBehaviour> ().hasBall = false;
		}
		this.attemptedPos = new Vector3 ();

		isBallWithServer = true;
		if (index > 4) {
			isBallWithServer = false;
			index %= 5;

			GameObject cPl = GameObject.Find ("ClientPlayer" + index);
			cPl.GetComponent<PlayerBehaviour> ().hasBall = true;
		
			//print ("SetBallPlayer: ClientPlayer "+ index + "has the ball");

		} else {
			GameObject sPl = GameObject.Find ("ServerPlayer" + index);
			sPl.GetComponent<PlayerBehaviour> ().hasBall = true;
			//print ("ServerPlayer" + index + "has the ball");

		}
		this.hasBallmoved = false;
		this.attemptedPos = this.transform.position;

	}
}
