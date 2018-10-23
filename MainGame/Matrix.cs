using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Matrix  {
  int rows;
  int cols;
  public float[,] data;

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

  public static Matrix Multiply(Matrix a , Matrix b){

    if (a.cols != b.rows) {
      Debug.Log("a.cols must equal b.rows");
      return null;
    }else{
      Matrix result = new Matrix(a.rows,b.cols);
      for(int i =0;i<a.rows;i++){
        for(int j=0;j<b.cols;j++){
          float sum=0;
          for(int k=0;k<b.rows;k++)
            sum+=a.data[i,k]*b.data[k,j];
          result.data[i,j]=sum;
          sum=0;
        }

      }
    return result;
    }
  }
  public static Matrix fromArray(float[] arr){
    Matrix mat = new Matrix(arr.Length,1);
      for(int i =0;i<arr.Length;i++){
        mat.data[i,0]=arr[i];
      }
    return mat;
  }
  public void SigmoidMap(){

  }
}
