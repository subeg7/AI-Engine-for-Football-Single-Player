using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AI : MonoBehaviour {
	int setNo;
	private GameObject[] server;
	private GameObject[] client;
	private GameObject[] all;

	private GameObject ball;

	private Vector3[] corner;
	private Vector3[] goalie;
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
			client[i] = GameObject.Find ("ClientPlayer"+i);
			all[i]=server[i];
			all[i+5]=client[i];

			corner[i] = GameObject.Find ("corner"+i).transform.position;
			goalie[i] = GameObject.Find ("goalie"+i).transform.position;


		}
		 ball = GameObject.Find("Ball");






	}

	// Update is called once per frame
	void Update () {

	}

	public void randomize(){
		// print("randomize function activated");
		GameObject setNumber = GameObject.Find("Set Number");
		setNumber.GetComponent<Text>().text="Traning Data Set No:"+ ++setNo;

		//randomize the field player position
		for (int i = 0; i < 4; i++) {
//			print("corner"+i+" ="+corner[i]);
			 Vector3 position1 = new Vector3(Random.Range(corner[0].x,corner[3].x), -0.4882813f, Random.Range(corner[0].z,corner[1].z));
			 server [i].transform.position = position1;
			 Vector3 position2 = new Vector3(Random.Range(corner[0].x,corner[3].x), -0.4882813f, Random.Range(corner[0].z,corner[1].z));
			 client [i].transform.position = position2;
		}
		//randomize the goalie position
		 server [4].transform.position = new Vector3(Random.Range(goalie[0].x,goalie[1].x), -0.4882813f, Random.Range(goalie[0].z,goalie[1].z));
		 client [4].transform.position =new Vector3(Random.Range(goalie[2].x,goalie[3].x), -0.4882813f, Random.Range(goalie[2].z,goalie[3].z));

		RandomizeBall();

	}

	public void RandomizeBall(){
		//find the random player to possess ball
		int randInd = Random.Range(0,9);
//		print ("randInd="+randInd);
		//set the new position of ball
		ball.transform.position = all[randInd].transform.position+new Vector3(0.2f,-0.2f,0.3f);
		print ("randInd "+randInd+" pos="+all[randInd].transform.position);

		//make the hasBall attribute true of randInd player only
		for (int i = 0; i < 10; i++) {

			all [i].GetComponent<Single_PlayerBehaviour> ().hasBall = false;
		}
		all[randInd].GetComponent<Single_PlayerBehaviour>(). hasBall= true;

	}


}
