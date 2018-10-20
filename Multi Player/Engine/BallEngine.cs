
using UnityEngine;

public class BallEngine {

	// Use this for initialization

	//ball information
	public Vector3 targetBall;
	public Vector3 sourceBall;



	public float near;
	private GameObject ball;


	//flags 
	public bool isBallWithServer;
	public bool hasBallMoved;
	private bool isLeftPostServer;
	private bool isTargetPostServer;
	public bool hasServerScored;

	public int targetPlayer;
	public int sourcePlayer;
	public Vector3[] passZoneVertices;


	public int clientScore;
	public int serverScore;
	public Vector3  testBall;

	public int interruptIndex;
	public void UpdateInfo(){
		GameObject ball = GameObject.Find ("Ball");
		sourceBall = ball.transform.position;




	}


//	public bool HasBallMoved(){
//		this.hasBallMoved = true;
//		if (proposedBall - sourceBall == Vector3.zero) {
//			this.hasBallMoved = false;
//		}
//		return this.hasBallMoved;
//	}


	public int NearestPlayer(Vector3[] players){
		int startPos = 0;
		if (!isBallWithServer)
			startPos = 5;

		int index=-1;
//		Debug.Log ("near = " + near);
		for (int i = startPos; i <= (startPos + 4); i++) {
//			Debug.Log("player" + i + "=" + players [i]);
//			Debug.Log ("targetBall = " + targetBall);
			float dis = Vector3.Distance (players [i], targetBall);
//			Debug.Log ("Distance of Player["+i+"]=" + dis);
			if (near>dis && i!=sourcePlayer) {
				index =i;
				break;
			}

		}
		return index;
	}

	public bool HasShoot (float pLeft, float pRight){
		bool hasShoot = false;

		if (targetBall.x <= pLeft || targetBall.x >= pRight)
			hasShoot = true;



		return  hasShoot;
	}

	public int GoalStatus(Vector3[] postL, Vector3[] postR,Vector3[] players){
		int status = 0;
		isLeftPostServer = true;
		isTargetPostServer = true;
		Vector3 shotBall = this.targetBall;
	
		int intrIndex =ShotInterruption(players, shotBall);

		interruptIndex = -1;
		if(intrIndex>-1){
			interruptIndex = intrIndex;
			status = 1;
			return status;
		}

//		Debug.Log ("GoalStatus method called");
		if (targetBall.x >= postR [0].x) {
			//Client Post
			isTargetPostServer = false;
			shotBall.x = postR [0].x;
			shotBall.z = targetBall.z / targetBall.x * shotBall.x;

			testBall = shotBall;

			if (isInsidePost (postR, shotBall)) {
				serverScore++;
				hasServerScored = true;
				return 0;		
			} 

		} else {
			Debug.Log ("ball is shot to LeftPost");

			//ServerPost home
			shotBall.x = postL [0].x;

			shotBall.z = targetBall.z / targetBall.x * shotBall.x;
			testBall = shotBall;


			if (isInsidePost (postL, shotBall)) {
				hasServerScored = false;
				clientScore++;
				return 0;
			} 
		}

		return -1;


	}

	private int ShotInterruption (Vector3[] players ,Vector3 shotBall ){
		InitPassZone (shotBall);

		int[] intPlayers=new int[10] ;
		int intCount=0;
		int index = -1;
		bool isInterrupted=false;

		for (int i = 0; i < 10; i++) {
			if (isInside (players [i])) {
				if (i != sourcePlayer) {
						Debug.Log ("Probable SAVE on shooting lane by:" + i);
					isInterrupted = true;
					intPlayers [intCount++] = i;
				}
			
			}
		}

		if (isInterrupted) {
			float minDis = 99999;//Vector3.Distance (sourceBall, players [intPlayers [0]]);
			for (int i = 0; i < intCount; i++) {

				float tempDis = Vector3.Distance (sourceBall, players [intPlayers [i]]);
				//	Debug.Log ("distance to interruption no:" + tempDis);
				if (tempDis < minDis) {
					minDis = tempDis;
					index = intPlayers [i];
				}

			}
		} else {
			return -1;
		}

		
		return index;

		}



	private bool isInsidePost(Vector3 [] post,Vector3 shotBall){
		int  j = post.Length-1; 
		bool inside = false; 
		Vector3 p = shotBall;

		for (int i = 0; i < post.Length; j = i++) { 
			if ( ((post[i].z <= p.z && p.z < post[j].z) || (post[j].z <= p.z && p.z < post[i].z)) && 
				(p.y < (post[j].y - post[i].y) * (p.z - post[i].z) / (post[j].z - post[i].z) + post[i].y)) 
				inside = !inside; 
		}

		return inside;
	}

	public int LaneInterruption(Vector3[] players,int targetIndex){
		int[] intPlayers=new int[10] ;
//		for (int i = 0; i < 10; i++)
//			intPlayers [i] = -1;

		int intCount=0;
		int index = -1;
		InitPassZone(players[targetIndex]);
		bool isInterrupted=false;



		//Debug.Log ("Checking for interruption");
		for (int i = 0; i < 10; i++) {
			if (isInside (players [i])) {
				if (i != targetPlayer && i != sourcePlayer) {
					Debug.Log ("Probable interruption on passing lane by:" + i);
					isInterrupted = true;
					intPlayers [intCount++] = i;
				}
			} else {
//				Debug.Log (" player[" + i + "] didn't interrupt");
			}
		}

//		Debug.Log ("this passing is interrupted by:" + intPlayers.Length + " players");
//		for (int i = 0; i < 10; i++) {
//			Debug.Log ("interruption["+i+"]" + intPlayers[i]);
//		}

		if (isInterrupted) {
			float minDis = 99999;//Vector3.Distance (sourceBall, players [intPlayers [0]]);
			for (int i = 0; i < intCount; i++) {
				
				float tempDis = Vector3.Distance (sourceBall, players [intPlayers [i]]);
			//	Debug.Log ("distance to interruption no:" + tempDis);
				if (tempDis < minDis) {
					minDis = tempDis;
					index = intPlayers [i];
				}
					
			}
		} else {
			return -1;
		}

//		Debug.Log ("Interruption by player" +index);

		return index;
	}

	public Vector3 LocateBall(Vector3[] players){
		
		float xBall,zBall;
		float len = Vector3.Distance (sourceBall, players [targetPlayer]);
		xBall =  players [targetPlayer].x+(near/len) *(sourceBall.x- players [targetPlayer].x);
		zBall =  players [targetPlayer].z+(near/len) * (sourceBall.z- players [targetPlayer].z);

		return new Vector3 (xBall, 0, zBall);
	}




	private bool isInside(Vector3  p){
		int  j = passZoneVertices.Length-1; 
		bool inside = false; 

		for (int i = 0; i < passZoneVertices.Length; j = i++) { 
			if ( ((passZoneVertices[i].z <= p.z && p.z < passZoneVertices[j].z) || (passZoneVertices[j].z <= p.z && p.z < passZoneVertices[i].z)) && 
				(p.x < (passZoneVertices[j].x - passZoneVertices[i].x) * (p.z - passZoneVertices[i].z) / (passZoneVertices[j].z - passZoneVertices[i].z) + passZoneVertices[i].x)) 
				inside = !inside; 
		}

		return inside;
	}

	private void InitPassZone( Vector3 target){
		
		Vector3 ball=sourceBall;

		Vector3 pRot2 =new Vector3(0,0,0);
		Vector3 pRot1 = new Vector3 (0, 0, 0);
		Vector3 b1 = new Vector3 (0, 0, 0);
		Vector3 b2 = new Vector3 (0, 0, 0);
		Vector3 b3 = new Vector3 (0, 0, 0);
		Vector3 b4 = new Vector3 (0, 0, 0);
		//		1 radian = 57.2958 degrees
		float angle = -Mathf.PI/2;
		if(target.x - ball.x !=0)
			angle += Mathf.Atan( (target.z - ball.z)/(target.x - ball.x) );
		
		float xRot1=near * Mathf.Cos (-angle) ;//+ size * Mathf.Sin (angle);
		float zRot1=-near *Mathf.Sin (-angle) ;//+ size * Mathf.Cos (angle);

		pRot1.x= xRot1;
		pRot1.z = zRot1;


		float  size1 = -near;
		float xRot2 = size1 * Mathf.Cos (-angle);//+ size * Mathf.Sin (angle);
		float zRot2= -size1 *Mathf.Sin (-angle) ;//+ size * Mathf.Cos (angle);
		pRot2.x= xRot2;
		pRot2.z = zRot2;

		b1 = ball -pRot2;
		b2 = target - pRot2;
		b3 = target - pRot1;
		b4 = ball - pRot1;


		b1.y = b2.y = b3.y = b4.y = -1.02f;
		passZoneVertices = new[] { b1, b2, b3, b4 };



	}

}
