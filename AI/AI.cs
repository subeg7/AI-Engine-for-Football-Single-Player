using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AI : MonoBehaviour {
	int setNo;

	// Use this for initialization
	void Start () {
		setNo =0;

	}

	// Update is called once per frame
	void Update () {

	}

	public void randomize(){
		print("randomize funciton activated");
		GameObject setNumber = GameObject.Find("Set Number");
		// if (!setNumber)
		// 	print ("null");
		// else
		// 	print("UI found");
		setNumber.GetComponent<Text>().text="Traning Data Set No:"+ ++setNo;


	}


}
