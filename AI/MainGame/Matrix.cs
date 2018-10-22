using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Matrix  {
  int rows;
  int cols;
  double[,] data;
  public Matrix(int _rows, int _cols){
    this.rows = _rows;
    this.cols = _cols;
		this.data = new double[_rows,_cols];

  }

  public void Display(string st){
    Debug.Log ("matrix="+st);
    Debug.Log ("rows="+this.rows);
    Debug.Log ("cols="+this.cols);
    Debug.Log ("data="+this.data);


  }

  public void Map(){

  }

  public void SigmoidMap(){

  }

}

// public class Function{
//
// }
