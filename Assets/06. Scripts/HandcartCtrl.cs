using UnityEngine;
using System.Collections;

public enum HandcartPosType {
	Front, Other
}

public class HandcartCtrl : MonoBehaviour {

	[SerializeField]
	HandcartPosType posType;

	[SerializeField]
	private Rigidbody body;

	[SerializeField]
	private ConfigurableJoint joint;

	[SerializeField]
	private Vector3 offsetFromTarget;

	[SerializeField]
	private WheelCollider left, right;

	[SerializeField]
	private Transform leftMeshTrans, rightMeshTrans;



	public Handcart data { get; private set; }

	Quaternion quat;
	Vector3 position;


	public Rigidbody Body { get { return body; } }
	public Vector3 OffsetFromTarget { get { return offsetFromTarget; } }

	void Update() {

		left.GetWorldPose( out position, out quat );
		//leftMeshTrans.position = position;
		leftMeshTrans.rotation = quat;

		right.GetWorldPose( out position, out quat );
		//rightMeshTrans.position = position;
		rightMeshTrans.rotation = quat;
	}

	public void setData(Handcart data) {
		this.data = data;
	}

	public void link( Rigidbody target ) {
		//	transform.position = target.transform.position;
		//if( target.CompareTag("Player") ) {
		//	transform.position = target.transform.position + new Vector3(0f, 1.1f, -2.5f);
		//	joint.anchor = new Vector3(0f, 0f, 1.5f);
		//}
		//else if( target.CompareTag("Handcart") ) {
		//	transform.position = target.transform.position + new Vector3(0f, 0f, -3f);
		//	joint.anchor = new Vector3(0f, 0f, 2f); ;
		//}

		//joint.connectedAnchor = new Vector3( 0f, 0f, -1f );

		transform.position = target.transform.position + offsetFromTarget;
		joint.connectedBody = target;

	}

	void OnDrawGizmos() {
		if ( Body == null ) return;

		var origin = Gizmos.color;
		Gizmos.color = Color.red;

		var from = transform.position + Vector3.up;
		Gizmos.DrawLine( from, from + Body.velocity );


		Gizmos.color = origin;
	}


}
