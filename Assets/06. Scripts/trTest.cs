using UnityEngine;
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
	float maximum = 13.0f;

	[SerializeField]
	float suppressFactor = 5f;

	[SerializeField]
	float downFactor = 5f;

	[SerializeField]
	Rigidbody[] cars;

	[SerializeField]
	WheelCollider[] wc;

	void OnDrawGizmos () {
		Ray r = new Ray ( transform.position, Vector3.down );
		Gizmos.DrawRay ( r );
	}

	// Update is called once per frame
	void FixedUpdate () {

		Ray r = new Ray ( transform.position + Vector3.up * 0.1f, Vector3.down );
		RaycastHit[] hits = Physics.RaycastAll ( r, 1.0f );
		//foreach( var e in hits ) {
		//	Debug.Log ( e.collider.gameObject.name );
		//}

		if( hits.Length == 0 ) {
			return;
		}


		Vector3 floorNormal = hits[0].normal;
		Vector3 orthogonal = Vector3.Cross( Vector3.up, floorNormal );
		float angle = Vector3.Angle( Vector3.up, floorNormal );
		var quat = Quaternion.AngleAxis( angle , orthogonal);

		Vector3 forward = quat * transform.forward;
		Vector3 right = quat * transform.right;

		Debug.DrawRay( transform.position, hits[0].normal, Color.blue );
		Debug.DrawRay( transform.position, forward, Color.yellow );
		Debug.DrawRay( transform.position, right, Color.cyan );

		float h = Input.GetAxis ( "Horizontal" );
		float v = Input.GetAxis ( "Vertical" );

		if ( v != 0f ) {
			body.AddForce( v * forward * speed, mode );
		}


		if ( h != 0f ) {
			if ( v != 0f ) {
				body.AddForce( h * right * rotSpeed, mode );
			}
			//forces += h * transform.right * rotSpeed;
			body.AddTorque( 0f, h * rotSpeed, 0f, mode );
		}
		else {
			foreach( Rigidbody c in cars ) {
				float playerZ = transform.eulerAngles.z / 360f;
				float carZ = c.transform.eulerAngles.z / 360f;

				if ( playerZ > 0.5f ) playerZ -= 1.0f;
				if ( playerZ < -0.5f ) playerZ += 1.0f;
				if ( carZ > 0.5f ) carZ -= 1.0f;
				if ( carZ < -0.5f ) carZ += 1.0f;



				float gap = playerZ - carZ;
				Debug.Log(gap);

				float torque = gap * downFactor;

				//torque = c.transform.InverseTransformDirection( torque );

				c.AddTorque( c.transform.forward * torque, ForceMode.VelocityChange);
			 }
		}
		

		if (body.velocity.magnitude > maximum) {
			Vector3 dir = body.velocity.normalized;
			body.velocity = dir * maximum;
		}

		suppress( forward );


		//Debug.Log(h);

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


		//if ( body.velocity.magnitude < 0.1f )
		//	return;

		//Vector3 localVelocity = transform.InverseTransformDirection( body.velocity );

		//bool side_able = true;// Mathf.Abs( localVelocity.z ) > 1.0f;


		//if (forces.sqrMagnitude < 1f) return;

		//float fdotv = Vector3.Dot(forces.normalized, body.velocity.normalized);
		//float additiveForceRate = 1f - Mathf.Clamp01(fdotv);
		//body.AddForce(transform.forward * speed * additiveForceRate * 5f, mode);

		//if (fdotv <= 0f) {
		//	Debug.Log("<= 0f");
		//}
	}


	void suppress( Vector3 forward ) {
		if (Input.GetKey(KeyCode.Space)) return;

		//float h = Input.GetAxis("Horizontal");

		//if (h < 0.1f) {
			foreach (var e in cars) { 
				Vector3 fdir = forward.normalized;
				Vector3 cdir = e.velocity.normalized;

				float s = e.velocity.magnitude;

				Vector3 newDir = Vector3.Lerp(cdir, fdir, suppressFactor * Time.deltaTime);

				e.velocity = newDir* s;
			}
		//}

	}

	void OnGUI() {
		GUILayout.Label(body.velocity.magnitude.ToString());

	}

}
