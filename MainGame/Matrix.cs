using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Matrix  {
  int rows;
  int cols;
  public float[,] data;

  public Matrix(int _rows, int _cols){
    this.rows = _rows;
    this.cols = _cols;
		this.data = new float[_rows,_cols];

    // this.Map(5.4f);

  }

  public void Display(string st){
    Debug.Log ("matrix="+st);
    Debug.Log ("rows="+this.rows);
    Debug.Log ("cols="+this.cols);
		this.DisplayArray ();
    Debug.Log("........................displaying ended for."+st);

  }

  public void Randomize(){
    for(int i =0;i<this.rows;i++){
      for(int j=0;j<this.cols;j++){
        // Debug.Log("inserting:"+x);
        this.data[i,j]= UnityEngine.Random.Range(-1.0f,1.0f );
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

  public  void DisplayArray(){
    string singleRow="[";
   for(int i =0;i<this.rows;i++){
     for(int j=0;j<this.cols;j++){
       singleRow+="  "+this.data[i,j];
       // Debug.Log("["+i+","+j+"]:"+this.data[i,j]);
     }
     singleRow +="]";
     Debug.Log(singleRow);
     singleRow ="[";
   }

  }

  public  static void DisplayArray(float[] arr){
    string singleRow="[";
   for(int i =0;i<arr.Length;i++){
       singleRow+="  "+arr[i];
     }
     singleRow +="]";
     Debug.Log(singleRow);

   }




  public void Map(string func){
    if(func=="sigmoid")
      this.SigmoidMap();
    else if (func=="relu")
      this.ReluMap();
    else if(func=="tanh")
      this.TanhMap();


  }
  public void Add(Matrix mat){
    for(int i =0;i<mat.rows;i++){
      for(int j=0;j<mat.cols;j++)
        this.data[i,j]+=mat.data[i,j];
    }
  }

  public static  Matrix  Substract(Matrix a, Matrix b){
    Matrix result  = new Matrix(a.rows,a.cols);
    for(int i =0;i<a.rows;i++){
      for(int j=0;j<a.cols;j++)
        result.data[i,j]= a.data[i,j]-b.data[i,j];
    }
    return result;
  }
  public static Matrix Multiply(Matrix a , Matrix b){

    if (a.cols != b.rows) {
      Debug.Log("a.cols must equal b.rows");
      return null;
    }else{
      Matrix result = new Matrix(a.rows,b.cols);
      // result.Display("result_of_mults");
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
  private void SigmoidMap(){
    for(int i =0;i<this.rows;i++){
      for(int j=0;j<this.cols;j++)
        this.data[i,j]=1/(1+(float)Math.Exp( this.data[i,j]) );
    }
  }

  private void ReluMap(){

  }

  private void TanhMap(){
  //   for(int i =0;i<this.rows;i++){
  //     for(int j=0;j<this.cols;j++)
  //       this.data[i,j]=1/(1+Math.exponential(-this.data[i,j]))
  //   }
  }
}
