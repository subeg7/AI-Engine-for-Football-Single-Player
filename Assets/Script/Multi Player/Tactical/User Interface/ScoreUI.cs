using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ScoreUI : MonoBehaviour {
	// Use this for initialization
	public string message;
	public Text score;
	void Start () {
		score = this.GetComponent<Text> ();
	}

}
