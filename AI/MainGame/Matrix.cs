using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Matrix  {
  int rows;
  int cols;
   float[,] data;

  public Matrix(int _rows, int _cols){
    this.rows = _rows;
    this.cols = _cols;
		this.data = new float[_rows,_cols];
    this.Map(5.4f);

  }

  public void Display(string st){
    Debug.Log ("matrix="+st);
    Debug.Log ("rows="+this.rows);
    Debug.Log ("cols="+this.cols);
		this.DisplayArray ();
    Debug.Log("displaying ended for "+st);

  }

  public void Randomize(){
    for(int i =0;i<this.rows;i++){
      for(int j=0;j<this.cols;j++){
        // Debug.Log("inserting:"+x);
        this.data[i,j]= Random.Range(-1.0f,1.0f );
        // Debug.Log("value at array:"+this.data[i,j]);
      }
    }
  }

  public void Map(float x){
   for(int i =0;i<this.rows;i++){
     for(int j=0;j<this.cols;j++){
       // Debug.Log("inserting:"+x);
       this.data[i,j]= x;
       // Debug.Log("value at array:"+this.data[i,j]);
     }
   }
  }

  public void DisplayArray(){
    string singleRow="[";
   for(int i =0;i<this.rows;i++){
     for(int j=0;j<this.cols;j++){
       singleRow+="_"+this.data[i,j];
       // Debug.Log("["+i+","+j+"]:"+this.data[i,j]);
     }
     singleRow +="]";
     Debug.Log(singleRow);
     singleRow ="[";
   }

  }
  public void SigmoidMap(){

  }




}

// public class Function{
//
// }
