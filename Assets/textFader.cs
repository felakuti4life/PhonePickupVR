using UnityEngine;
using System.Collections;

public class textFader : MonoBehaviour {
	private TextMesh mesh;
	// Use this for initialization
	void Start () {
		mesh = gameObject.GetComponent<TextMesh> ();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		if (mesh.color.a > 0.0) {
			Color c = mesh.color;
			c.a -= 0.01f;
			mesh.color = new Color( c.r,c.g,c.b, c.a);
		}
	}
}
