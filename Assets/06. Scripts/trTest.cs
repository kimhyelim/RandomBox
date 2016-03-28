﻿using UnityEngine;
using System.Collections;

public class trTest : MonoBehaviour {
	[SerializeField]
	Rigidbody body;

	[SerializeField]
	float speed = 150.0f;

	[SerializeField]
	float rotSpeed = 30.0f;

	[SerializeField]
	ForceMode mode;


	[SerializeField]
	WheelCollider[] wc;

	void OnDrawGizmos () {
		Ray r = new Ray ( transform.position, Vector3.down );
		Gizmos.DrawRay ( r );
	}

	// Update is called once per frame
	void FixedUpdate () {
		//Vector3 c = ( wc[0].transform.position + wc[1].transform.position ) * 0.5f;
		//c.y = transform.position.y;
		//      Vector3 cToPosVec = (transform.position - c).normalized;
		//Quaternion q =  Quaternion.AngleAxis ( 90f, Vector3.up );
		//Vector3 targetRot = q* cToPosVec;

		//Vector3 dir = transform.forward;

		Ray r = new Ray ( transform.position + Vector3.up * 0.1f, Vector3.down );
		RaycastHit[] hits = Physics.RaycastAll ( r, 1.0f );
		//foreach( var e in hits ) {
		//	Debug.Log ( e.collider.gameObject.name );
		//}

		if( hits.Length == 0 ) {
			return;
		}

		float h = Input.GetAxis ( "Horizontal" );
		float v = Input.GetAxis ( "Vertical" );

		//Vector3 d = new Vector3 ( h, 0.0f, v ).normalized;
		//d = transform.TransformDirection ( d );
		//Debug.Log ( d );

		//body.AddForce ( new Vector3 ( d.x * rotSpeed, 0f, d.z * speed ), mode );
		//return;



		//float x = Input.GetKey ( KeyCode.D ) ? 1f : ( Input.GetKey ( KeyCode.A ) ? -1f : 0f );
		//float z = Input.GetKey ( KeyCode.W ) ? 1f : ( Input.GetKey ( KeyCode.S ) ? -1f : 0f );

		//Vector3 dir = new Vector3 ( x, 0f, z ).normalized;
		//dir = transform.TransformDirection ( dir );

		//body.AddForce ( new Vector3 ( dir.x * rotSpeed, 0f, dir.z * speed ), mode );

		if( Input.GetKey ( KeyCode.W ) ) {
			body.AddForce ( transform.forward * speed, mode );
		}
		if( Input.GetKey ( KeyCode.S ) ) {
			body.AddForce ( -transform.forward * speed, mode );
		}

		if( body.velocity.magnitude < 0.1f )
			return;		

		if( Input.GetKey ( KeyCode.D ) ) {
			body.AddForce ( h * transform.right * rotSpeed, mode );
			//return;
		}
		if( Input.GetKey ( KeyCode.A ) ) {
			body.AddForce ( h * transform.right * rotSpeed, mode );
			//return;
		}

	}

}
