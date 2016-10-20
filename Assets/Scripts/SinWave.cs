using UnityEngine;
using System.Collections;

public class SinWave : MonoBehaviour {

    public Vector3[] verts;

    void Start() {
        verts = GetComponent<MeshFilter>().mesh.vertices;
    }
	// Update is called once per frame
	void Update () {
        // Alter verts here

        // Add verts back into Mesh
        GetComponent<MeshFilter>().mesh.vertices = verts;
    }
}
