using UnityEngine;
using System.Collections;

public class carTest : MonoBehaviour {
	[SerializeField]
	Rigidbody body;


	void OnDrawGizmos() {
		var origin = Gizmos.color;
		Gizmos.color = Color.red;

		var from = transform.position + Vector3.up ;
		Gizmos.DrawLine(from, from + body.velocity);


		Gizmos.color = origin ;
	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
