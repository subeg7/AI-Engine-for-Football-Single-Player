using System.Collections;
using System.IO;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class AI : MonoBehaviour {
	int setNo;
	private GameObject[] server;
	private GameObject[] client;
	private GameObject[] all;

	private GameObject ball;
	private GameObject message;


	private Vector3[] corner;
	private Vector3[] goalie;

	 private int ballPlayerInd;

	private float ypos;
	public GameObject inst;
	// Use this for initialization
	void Start () {
		setNo =0;
		server = new GameObject[5];
		client = new GameObject[5];
		all = new GameObject[10];

		corner = new Vector3[5];
		goalie = new Vector3[5];

		for (int i = 0; i < 5; i++) {
			server[i] = GameObject.Find ("ServerPlayer" + i);
			server [i].GetComponent<PlayerBehavior> ().isMovementAllowed = false;

			client[i] = GameObject.Find ("ClientPlayer"+i);
			client [i].GetComponent<PlayerBehavior> ().isMovementAllowed = true;

			all[i]=server[i];
			all[i+5]=client[i];

			corner[i] = GameObject.Find ("corner"+i).transform.position;
			goalie[i] = GameObject.Find ("goalie"+i).transform.position;


		}
		 ball = GameObject.Find("Ball");
		 message = GameObject.Find("Message");


		ypos = -0.48828f;
	}

	public void randomize(){
		//randomize the field player position
		for (int i = 0; i < 4; i++) {
			//for team-Server(white)
			Vector3 position1 = new Vector3(Random.Range(corner[0].x,corner[3].x), ypos, Random.Range(corner[0].z,corner[1].z));
			server [i].transform.position = position1;
			server [i].GetComponent<PlayerBehavior>(). attemptedState = position1;

			//for team-client(black)
			Vector3 position2 = new Vector3(Random.Range(corner[0].x,corner[3].x), ypos, Random.Range(corner[0].z,corner[1].z));
			client [i].transform.position = position2;
			client [i].GetComponent<PlayerBehavior>().attemptedState = position2;
		}
		//randomize the goalie position within D-Box
		server [4].transform.position = new Vector3(Random.Range(goalie[0].x,goalie[1].x), ypos, Random.Range(goalie[0].z,goalie[1].z));
		client [4].transform.position =new Vector3(Random.Range(goalie[2].x,goalie[3].x), ypos, Random.Range(goalie[2].z,goalie[3].z));

		//also change the position of ball
		RandomizeBall();

		//display message
		message.GetComponent<Text>().text="Players and Ball randomized";
	}

	public void RandomizeBall(){
		//find the random player to possess ball
		int ballPlayerInd = Random.Range(0,9);

		//set the new position of ball
		Vector3 newBallPos = new Vector3(all[ballPlayerInd].transform.position.x, ball.transform.position.y,all[ballPlayerInd].transform.position.z) +new Vector3(0.3f,0,0.3f);

		ball.transform.position = newBallPos;



		//make the hasBall attribute true of randInd player only
		for (int i = 0; i < 10; i++) {

			all [i].GetComponent<PlayerBehavior> ().hasBall = false;
			all [i].GetComponent<PlayerBehavior> ().ClearLine ();

		}
		all[ballPlayerInd].GetComponent<PlayerBehavior>(). hasBall= true;

		//display message
		message.GetComponent<Text>().text="Ball position changed";
	}

	public void Submit(){

		//create array of position of all players
		Vector3[] currentPlayerPos = new Vector3[10];
		for(int i=0;i<10;i++)
			currentPlayerPos[i]= all[i].GetComponent<PlayerBehavior>().attemptedState;

			//create a Serializable object of currentstate of field
		Field currentState = new Field(currentPlayerPos,all[ballPlayerInd].transform.GetComponent<PlayerBehavior>().attemptedState,ballPlayerInd);
		Field.Serialize(currentState);


		GameObject setNumber = GameObject.Find("Set Number");
		setNumber.GetComponent<Text>().text="Traning Data Set No:"+ ++setNo;
		message.GetComponent<Text>().text="successfully written";


	}

	[System.Serializable]
	class Field{
		public Vector3[] playerPos;
		public Vector3 ballPos;
		public int ballPlayerInd;


		public Field(Vector3[] players, Vector3 ball,int ballPlayer){
			playerPos = players;
			ballPos = ball;
			ballPlayerInd = ballPlayer;
		}

		public static string Serialize(Field obj){
			string json = JsonUtility.ToJson(obj);
			string filePath = Path.Combine(Application.dataPath, "Script/AI/Data/TrainingData.json");
			File.AppendAllText (filePath,"\n"+json);

			return "null";
		}

		public static Field Desieralize(string json){

			return null;
		}


	}


}
