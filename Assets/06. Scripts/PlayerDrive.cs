using UnityEngine;
using System.Collections;

public class PlayerDrive : MonoBehaviour {
	public Rigidbody body;
	public float speed = 150.0f;
	public float rotSpeed = 30.0f;
	public ForceMode mode;
	

	// Update is called once per frame
	void FixedUpdate() {
		Vector3 dir = transform.forward;

		if ( Input.GetKey( KeyCode.W ) ) {
			body.AddForce( dir * speed, mode );
		}
		if ( Input.GetKey( KeyCode.S ) ) {
			body.AddForce( -dir * speed, mode );
		}
		if ( Input.GetKey( KeyCode.A ) ) {
			body.AddTorque( 0.0f, -rotSpeed, 0.0f, mode );

		}
		if ( Input.GetKey( KeyCode.D ) ) {
			body.AddTorque( 0.0f, rotSpeed, 0.0f, mode );
		}
		
	}


	public void OnDrawGizmos() {
		var origin = Gizmos.color;
		Gizmos.color = Color.black;
		Gizmos.DrawLine( transform.position, ( transform.position + transform.forward  ) );
		Gizmos.color = origin;
	}

}
