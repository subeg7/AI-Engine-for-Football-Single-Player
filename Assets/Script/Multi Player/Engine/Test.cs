using UnityEngine;
using System.Collections;

public class Test : MonoBehaviour {


	public GameObject p1;
	public GameObject p2;
	public GameObject p3;
	public GameObject p4;
	public GameObject p5;
	public GameObject p6;

	public GameObject testPoint;

	public Material traingleMat;


	private int size;
	private MeshRenderer meshRenderer;
	private Vector3 checkP;
	// Use this for initialization
	void Start () {

		gameObject.AddComponent<MeshFilter>();
		meshRenderer = gameObject.AddComponent<MeshRenderer>();



		Vector3 v1 = p1.transform.position;
		Vector3 v2 = p2.transform.position;
		Vector3 v3 = p3.transform.position;
		Vector3 v4 = p4.transform.position;
	 	//Vector3 v5 = p5.transform.position;
	
		checkP = testPoint.transform.position;
	





		
		size = 3;
	}


	void Update(){
		Vector3 v1 = p1.transform.position;
		Vector3 v2 = p2.transform.position;

		if(this.transform.name=="T1")
			CreateRect (v1, v2);

	}

	void Traingle (Vector3 v1, Vector3 v2, Vector3 v3){
		//init vairables
		Mesh mesh;
	
		Vector3[] vertices;
		int[] triangles;




//		meshRenderer.material = traingleMat;

		mesh = new Mesh();
		GetComponent<MeshFilter>().mesh = mesh;

		vertices = new[] {v1,v2,v3};

		mesh.vertices = vertices;

		triangles = new[]{0, 1, 2};

		mesh.triangles = triangles;

	}



	bool isInside(Vector3[] polyPoints, Vector3  p){
		int  j = polyPoints.Length-1; 
		bool inside = false; 

		for (int i = 0; i < polyPoints.Length; j = i++) { 
			if ( ((polyPoints[i].z <= p.z && p.z < polyPoints[j].z) || (polyPoints[j].z <= p.z && p.z < polyPoints[i].z)) && 
				(p.x < (polyPoints[j].x - polyPoints[i].x) * (p.z - polyPoints[i].z) / (polyPoints[j].z - polyPoints[i].z) + polyPoints[i].x)) 
				inside = !inside; 
		}

		return inside;
	}


	void CreateRect(Vector3 ball,Vector3 target){

//		int size1  = size;

		Vector3 pRot2 =new Vector3(0,0,0);
		Vector3 pRot1 = new Vector3 (0, 0, 0);
		Vector3 b1 = new Vector3 (0, 0, 0);
		Vector3 b2 = new Vector3 (0, 0, 0);
		Vector3 b3 = new Vector3 (0, 0, 0);
		Vector3 b4 = new Vector3 (0, 0, 0);
//		1 radian = 57.2958 degrees
		float angle = -Mathf.PI/2;
		if(target.x - ball.x !=0)
			angle += Mathf.Atan( (target.z - ball.z)/(target.x - ball.x) );
//		else
//			angle = Mathf.PI/2;

		//print("Angle : "+(angle*57.2958f-90));
		//rotate form origin;
		float xRot1=size * Mathf.Cos (-angle) ;//+ size * Mathf.Sin (angle);
		float zRot1=-size *Mathf.Sin (-angle) ;//+ size * Mathf.Cos (angle);

//		Vector3 pRot1= p1.transform.position.x;//to get the plane of y;
		pRot1.x= xRot1;
		pRot1.z = zRot1;


		int  size1 = -size;
		float xRot2 = size1 * Mathf.Cos (-angle);//+ size * Mathf.Sin (angle);
		float zRot2= -size1 *Mathf.Sin (-angle) ;//+ size * Mathf.Cos (angle);
		//Vector3 pRot2 = p1.transform.position.x;//to get the plane of y;
		pRot2.x= xRot2;
		pRot2.z = zRot2;

		//transform the rotated points to the ball and target;
		b1 = ball -pRot2;
		b2 = target - pRot2;
		b3 = target - pRot1;
		b4 = ball - pRot1;


		p3.transform.position = b1;
		p4.transform.position = b2;
		p5.transform.position = b3;
		p6.transform.position = b4;

		if (isInside (new[] {b1,b2,b3,b4}, checkP))
			print ("INSIDE");
		else
			print ("OUTSIDE");
	
	}





}