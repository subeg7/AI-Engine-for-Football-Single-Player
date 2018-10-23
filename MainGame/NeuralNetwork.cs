using UnityEngine;

public class NeuralNetwork {
	int input_nodes;
	int hidden_nodes;
	int output_nodes;

	Matrix weights_ih;
	Matrix weights_ho;
	Matrix bias_h;
	Matrix bias_o;

	float learning_rate = 0.1f;


	public NeuralNetwork(int inputNodes, int hiddenNodes, int outputNodes){
		this.input_nodes = inputNodes;
		this.hidden_nodes = hiddenNodes;
		this.output_nodes = outputNodes;

		this.weights_ih = new Matrix(this.hidden_nodes, this.input_nodes);
		this.weights_ho = new Matrix(this.output_nodes, this.hidden_nodes);
		this.weights_ih.Randomize();
    this.weights_ho.Randomize();

		this.bias_h = new Matrix(this.hidden_nodes, 1);
    this.bias_o = new Matrix(this.output_nodes, 1);
    this.bias_h.Randomize();
    this.bias_o.Randomize();

		// weights_ih.Display("weights_ih");
	}
			public void  trainX(float[] myTeam, float[] oppTeam, float[] myTarget,int ballPlayer ){
				Matrix input_mat = Matrix.fromArray(myTeam);
				Matrix hidden = Matrix.Multiply(this.weights_ih,input_mat);

	}



}
