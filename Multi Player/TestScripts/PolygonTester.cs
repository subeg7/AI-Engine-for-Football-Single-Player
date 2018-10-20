using UnityEngine;

public class PolygonTester : MonoBehaviour {

	public GameObject p1;
	public GameObject p2;
	public GameObject p3;
	public GameObject p4;
	public GameObject p5;

	void Start () {

		Vector2 v1 = new Vector2(p1.transform.position.x,p1.transform.position.y);
		Vector2 v2 = new Vector2(p2.transform.position.x,p2.transform.position.y);
		Vector2 v3 = new Vector2(p3.transform.position.x,p3.transform.position.y);
		Vector2 v4 = new Vector2(p4.transform.position.x,p4.transform.position.y);
		Vector2 v5 = new Vector2(p5.transform.position.x,p5.transform.position.y);

		Vector2[] vertices2D = new Vector2[] {v1,v2,v3,v4,v5};

		// Use the triangulator to get indices for creating triangles
		Triangulator tr = new Triangulator(vertices2D);
		int[] indices = tr.Triangulate();

		// Create the Vector3 vertices
		Vector3[] vertices = new Vector3[vertices2D.Length];
		for (int i=0; i<vertices.Length; i++) {
			vertices[i] = new Vector3(vertices2D[i].x, vertices2D[i].y, 0);
		}

		// Create the mesh
		Mesh msh = new Mesh();
		msh.vertices = vertices;
		msh.triangles = indices;
		msh.RecalculateNormals();
		msh.RecalculateBounds();

		// Set up game object with mesh;
		gameObject.AddComponent(typeof(MeshRenderer));
		MeshFilter filter = gameObject.AddComponent(typeof(MeshFilter)) as MeshFilter;
		filter.mesh = msh;
	}
}