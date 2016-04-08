using UnityEngine;
using System.Collections;

public class HandcartCtrl : MonoBehaviour {

	public Rigidbody body;

	[SerializeField]
	private ConfigurableJoint joint;

	[SerializeField]
	private WheelCollider left, right;
	[SerializeField]
	private GameObject leftMesh, rightMesh;


	void Update() {

		Quaternion quat;
		Vector3 position;
		left.GetWorldPose( out position, out quat );
		//leftMesh.transform.position = position;
		if( leftMesh != null)
			leftMesh.transform.rotation = quat;

		right.GetWorldPose( out position, out quat );
		if ( rightMesh != null )
			rightMesh.transform.rotation = quat;
	}

	public void link( Rigidbody target ) {
		//	transform.position = target.transform.position;
		//if ( target.CompareTag( "Player" ) ) {
		//	transform.position = target.transform.position + new Vector3( 0f, 1.1f, -2.5f );
		//	joint.anchor = new Vector3( 0f, 0f, 1.5f );
		//}
		//else if ( target.CompareTag( "Handcart" ) ) {
		//	transform.position = target.transform.position + new Vector3( 0f, 0f, -3f );
		//	joint.anchor = new Vector3( 0f, 0f, 2f ); ;
		//}

		//joint.connectedAnchor = new Vector3( 0f, 0f, -1f );
		joint.connectedBody = target;

	}

	void OnDrawGizmos() {
		if ( body == null ) return;

		var origin = Gizmos.color;
		Gizmos.color = Color.red;

		var from = transform.position + Vector3.up;
		Gizmos.DrawLine( from, from + body.velocity );


		Gizmos.color = origin;
	}


}
