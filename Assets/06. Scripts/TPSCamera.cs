using UnityEngine;
using System.Collections;

[ExecuteInEditMode]
public class TPSCamera : MonoBehaviour {
	public enum Type { 
		BackView, QuarterView
	}

	[SerializeField]	
	Transform target;
	[SerializeField]
	Vector3 distance;
	[SerializeField]
	Type type;


	private Transform trans;

	void Awake() {
		trans = transform;
	}

	void LateUpdate() {

		if ( type == Type.BackView ) {
			transform.parent = target.transform;
			transform.localPosition = distance;
			trans.LookAt( target );
		}
		else if ( type == Type.QuarterView ) {
			transform.parent = null;
			trans.position = target.position + distance ;
			trans.LookAt( target );
		}
	}

	public void setTarget( Transform target ) {
		this.target = target;
		if ( type == Type.BackView ) {
			transform.parent = target.transform;
			transform.localPosition = distance;
		}
		else {
			transform.parent = null;
		}
	}

	public void setType( Type type) {
		this.type = type;
	}

	//public void setDistance

}
