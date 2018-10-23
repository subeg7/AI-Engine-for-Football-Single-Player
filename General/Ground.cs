using System.Collections;
using System.IO;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
class Ground{
	public Vector3[] myTeamInitialPos;
	public Vector3[] oppTeamPos;
	public Vector3[] myTeamTargetPos;
	public Vector3 ballPos;
	public int ballPlayerInd;


	public Ground(Vector3[] myTeam,Vector3[] oppTeam,Vector3[] target, Vector3 ball,int ballPlayer){
		myTeamInitialPos = myTeam;
		oppTeamPos=oppTeam;
		myTeamTargetPos=target;
		ballPos = ball;
		ballPlayerInd = ballPlayer;
	}

	public static string Serialize(Ground obj){
		string json = JsonUtility.ToJson(obj);
		string filePath = Path.Combine(Application.dataPath, "Script/AI/TrainingGround/Data/TrainingData.json");
		File.AppendAllText (filePath,json+"\n");

		return "null";
	}

	public static Ground Desieralize(string json){

		return null;
	}


}
