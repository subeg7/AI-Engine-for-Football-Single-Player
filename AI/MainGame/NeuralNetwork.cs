using UnityEngine;

public class NeuralNetwork {
	int input_nodes;
	int hidden_nodes;
	int output_nodes;



	public NeuralNetwork(int inputNodes, int hiddenNodes, int outputNodes){
		Debug.Log("print suceess");
		this.input_nodes = inputNodes;
		this.hidden_nodes = hiddenNodes;
		this.output_nodes = outputNodes;

		Matrix weights_ih = new Matrix(this.hidden_nodes, this.input_nodes);
		weights_ih.Display("weights_ih");
	}

}
