using UnityEngine;
using System.Collections;

[ExecuteInEditMode]
public class CameraCtrl : MonoBehaviour {
	public enum Type { 
		BackView, QuarterView
	}

	[SerializeField]	
	Transform target;
	[SerializeField]
	float angle = 45f;
	[SerializeField]
	float distance = 10f;
	//[SerializeField]
	float delayTime = 0.15f;
	[SerializeField]
	Type type;


	private Transform trans;

	void Awake() {
		trans = transform;
	}

	void LateUpdate() {
		Vector3 dest = new Vector3 ();

		if( type == Type.BackView ) {
			Quaternion q = Quaternion.AngleAxis (angle, target.right);
			Vector3 dir = q * -target.forward;
			dest = target.position + dir * distance;
			
			//dest = target.position + target.TransformDirection ( distance );
		}
		else if( type == Type.QuarterView ) {
			Quaternion q = Quaternion.AngleAxis ( angle, Vector3.right );
			Vector3 dir = q * Vector3.back;
			dest = target.position + dir * distance;
		}		

		var delta = dest - transform.position;
		transform.position += delta * Time.deltaTime / delayTime;
		trans.LookAt ( target );

#if UNITY_EDITOR
		if(!Application.isPlaying)
			transform.position = dest;
#endif
	}

	public void setTarget( Transform target ) {
		this.target = target;
	}

	public void setType( Type type) {
		this.type = type;
	}
	
}
