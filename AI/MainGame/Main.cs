using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Main : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
		NeuralNetwork nn  = new NeuralNetwork(5,5,1);
		// nn.predict();
	}

}
