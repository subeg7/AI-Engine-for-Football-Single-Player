using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;


public class Main : MonoBehaviour {

	private static string filePath;

	// Use this for initialization
	void Start () {

		//create the nerual netowrk
		NeuralNetwork nn  = new NeuralNetwork(10,7,5);
		nn.SetActivationFunction("sigmoid");

		//fetch the data from TrainingData.json file
		filePath = Path.Combine(Application.dataPath, "Script/TrainingGround/Data/TrainingData.json");
		string dataAsJson = File.ReadAllText(filePath);
		string[] data = dataAsJson.Split('\n');


		for(int i = 0;i<1;i++){
				//create a single Ground Object
				Ground groundData = JsonUtility.FromJson<Ground>(data[i]);

				float[] allPlayerInitX = new float[10];
				float[] myTeamTargetX = new float[5];
				//make separate array of x and z only
				for(int j =0;j<5;j++){

					allPlayerInitX[j]=groundData.myTeamInitialPos[j].x;
					allPlayerInitX[j+5]=groundData.oppTeamPos[j].x;

					myTeamTargetX[j]=groundData.myTeamTargetPos[j].x;


				}

				//train the NeuralNetwork from the dataObject for X
				nn.trainX(allPlayerInitX,myTeamTargetX,groundData.ballPlayerInd);
		//		nn.trainZ(myTeamX,oppTeamX,myTargetX,groundData.ballPlayerInd);

				// nn.trainZ(groundData.myTeamInitialPos,groundData.oppTeamPos,groundData.myTeamTargetPos,groundData.ballPlayerInd);
	}


	}

}
