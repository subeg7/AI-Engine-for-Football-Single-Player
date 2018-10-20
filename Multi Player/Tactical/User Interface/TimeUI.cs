using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;


public class TimeUI : MonoBehaviour {

	// Use this for initialization
	public string time;
	public Text timeRem;
	void Start () {
		timeRem = this.GetComponent<Text> ();


	}
	
	// Update is called once per frame
	void Update () {
		GameObject controller = GameObject.FindGameObjectWithTag ("GameController");
//		time = controller.GetComponent<SessionManager> ().timeUI;
		timeRem.text ="Time:x"+time;


		//print ("update:"+time);
	}
}
