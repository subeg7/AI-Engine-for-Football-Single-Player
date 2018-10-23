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

		//train the NeuralNetwork from the dataObject
		nn.train(groundData);
	}


	}

}
