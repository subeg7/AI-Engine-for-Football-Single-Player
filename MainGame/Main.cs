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
		nn.activation_function="sigmoid";

		//fetch the data from TrainingData.json file
		filePath = Path.Combine(Application.dataPath, "Script/TrainingGround/Data/TrainingData.json");
		string dataAsJson = File.ReadAllText(filePath);
		string[] data = dataAsJson.Split('\n');


		for(int i = 0;i<1;i++){
				//create a single Ground Object
				Ground groundData = JsonUtility.FromJson<Ground>(data[i]);

				//init X array and Z array
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
		//			for x
					myTeamX[j] = groundData.myTeamInitialPos[j].x;
					oppTeamX[j] = groundData.oppTeamPos[j].x;
					myTargetX[j] = groundData.myTeamTargetPos[j].x;
					//for z
					myTeamZ[j] = groundData.myTeamInitialPos[j].z;
					oppTeamZ[j] = groundData.oppTeamPos[j].z;
					myTargetZ[j] = groundData.myTeamTargetPos[j].z;
				}

				//train the NeuralNetwork from the dataObject for X
				nn.trainX(myTeamX,oppTeamX,myTargetX,groundData.ballPlayerInd);
		//		nn.trainZ(myTeamX,oppTeamX,myTargetX,groundData.ballPlayerInd);

				// nn.trainZ(groundData.myTeamInitialPos,groundData.oppTeamPos,groundData.myTeamTargetPos,groundData.ballPlayerInd);
	}


	}

}
