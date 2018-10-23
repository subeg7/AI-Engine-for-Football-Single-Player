using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;


public class Main : MonoBehaviour {

	private static string filePath;

	// Use this for initialization
	void Start () {

		//create the nerual netowrk
		NeuralNetwork nn  = new NeuralNetwork(5,5,1);

		//fetch the data from TrainingData.json file
		filePath = Path.Combine(Application.dataPath, "Script/TrainingGround/Data/TrainingData.json");
		string dataAsJson = File.ReadAllText(filePath);
		string[] data = dataAsJson.Split('\n');


		for(int i = 0;i<5;i++){
		//create a single Ground Object
		Ground groundData = JsonUtility.FromJson<Ground>(data[i]);


		float[] myTeamX,oppTeamX,myTargetX;
		float[] myTeamZ,oppTeamZ,myTargetZ;

		myTeamX=new float[5];
		oppTeamX=new float[5];
		myTargetX=new float[5];
		
		myTeamZ=new float[5];
		oppTeamZ=new float[5];
		myTargetZ=new float[5];

		//make separate array of x and z only
		for(int j =0;j<5;j++){
			myTeamX[i] = groundData.myTeamInitialPos[i].x;
			oppTeamX[i] = groundData.oppTeamPos[i].x;
			myTargetX[i] = groundData.myTeamTargetPos[i].x;
			//for z
			myTeamZ[i] = groundData.myTeamInitialPos[i].z;
			oppTeamZ[i] = groundData.oppTeamPos[i].z;
			myTargetZ[i] = groundData.myTeamTargetPos[i].z;

		}

		//train the NeuralNetwork from the dataObject for X
		nn.trainX(myTeamX,oppTeamX,myTargetX,groundData.ballPlayerInd);
		// nn.trainZ(groundData.myTeamInitialPos,groundData.oppTeamPos,groundData.myTeamTargetPos,groundData.ballPlayerInd);
	}


	}

}
