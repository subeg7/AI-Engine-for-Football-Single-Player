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
		filePath = Path.Combine(Application.dataPath, "Script/AI/TrainingGround/Data/TrainingData.json");
		string dataAsJson = File.ReadAllText(filePath);
		Ground ground = JsonUtility.FromJson<Ground>(dataAsJson);
		//train the NeuralNetwork
		// nn.train(input_array,target array);


		// nn.predict();
	}

}
