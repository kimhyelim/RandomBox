using UnityEngine;
using System.Collections;

public class PhyTest : MonoBehaviour {
	public Rigidbody body;
	public float speed = 150.0f;
	public float rotSpeed = 30.0f;
	public Vector3 dir;
	public ForceMode mode;

	void Start() {

		// body = GetComponent<Rigidbody>();

	}
	// Update is called once per frame
	void FixedUpdate() {

		Vector3 dir = transform.forward;
		//  Debug.Log(dir);

		if ( Input.GetKey( KeyCode.W ) ) {
			body.AddForce( dir * speed, mode );
			//	body.velocity = dir * speed;
			//             GameObject.Find("smoke").SetActive(true);
		}
		if ( Input.GetKey( KeyCode.S ) ) {
			//body.velocity = -dir * speed;
			body.AddForce( -dir * speed, mode );
			//             GameObject.Find("smoke").SetActive(true);
		}
		if ( Input.GetKey( KeyCode.A ) ) {
			body.AddTorque( 0.0f, -rotSpeed, 0.0f, mode );
			//body.angularVelocity = new Vector3( 0.0f, -rotSpeed, 0.0f );
			//body.r.Rotate(Vector3.up, -rotSpeed, Space.Self);

		}
		if ( Input.GetKey( KeyCode.D ) ) {
			body.AddTorque( 0.0f, rotSpeed, 0.0f, mode );
			//body.angularVelocity = new Vector3( 0.0f, rotSpeed, 0.0f );
			//transform.Rotate(Vector3.up, rotSpeed, Space.Self);

		}


	}
}
