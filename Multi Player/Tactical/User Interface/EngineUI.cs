using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class EngineUI : MonoBehaviour {

	public string message;
	public Text Message;
	void Start () {
//		Message = this.GetComponent<Text> ();
	}

	public void Display(string st){
//		Message.text = "engine: "+st;
	}
}
