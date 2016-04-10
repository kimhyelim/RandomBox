using UnityEngine;
using System.Collections;

// 수레의 위치
// 첫번째 수레는 모델이 달라야하기 때문(손잡이 붙음)에 따로 구분이 필요.
// Front : 제일 앞 수레.
// Other : Front를 제외한 나머지.
public enum HandcartPosType {
	Front, Other
}

/*
 * 수레 게임 오브젝트에게 작용할 수 있는 기능들의 집합.
 * 다른 리지드바디에 수레 연결 기능.
 * 충돌시 생선 드랍 처리 기능.
 * 바퀴 트렌스폼 적용 기능.
 */
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

	[SerializeField]
	private PopFish popFish;

	[SerializeField]
	private int maxSpeed;
	[SerializeField]
	private int maxDropCount;

	public Handcart data { get; private set; }

	Quaternion quat;
	Vector3 position;


	public Rigidbody Body { get { return body; } }
	public Vector3 OffsetFromTarget { get { return offsetFromTarget; } }

	private Vector2 lastVel;

	void Start() {
		Vector3 vel = body.velocity;
		lastVel = new Vector2( vel.x, vel.z);
	}

	void Update() {
		Vector3 vel = body.velocity;
		Vector2 vel2d = new Vector2( vel.x, vel.z);


		Vector2 sub = lastVel - vel2d;
		Vector3 vec = new Vector3(sub.x, 0f, sub.y);
		float gap = sub.magnitude / maxSpeed;
		Mathf.Clamp01(gap);

		if( gap > 0.4f ) {
			Debug.Log("gap : " + gap + "vec : " + ( vec ));
			
			Vector3 xzVec = new Vector3(vec.x, 1f, vec.z);
			Vector3 cross = Vector3.Cross( vec.normalized, xzVec.normalized);
			vec = Quaternion.AngleAxis(30f, cross) * vec;

			popFish.pop(Mathf.FloorToInt(maxDropCount * gap), vec);
			//popFish.pop(Mathf.FloorToInt(20), vec);
		}
		lastVel = vel2d;

		setWheelTransform();
	}

	void setWheelTransform() {
		left.GetWorldPose(out position, out quat);
		//leftMeshTrans.position = position;
		leftMeshTrans.rotation = quat;

		right.GetWorldPose(out position, out quat);
		//rightMeshTrans.position = position;
		rightMeshTrans.rotation = quat;
	}

	public void setData( Handcart data ) {
		this.data = data;
	}

	public void link( Rigidbody target ) {
		transform.position = target.transform.position + target.transform.TransformDirection(offsetFromTarget);
		joint.connectedBody = target;

	}

	void OnDrawGizmos() {
		if( Body == null ) return;

		var origin = Gizmos.color;
		Gizmos.color = Color.red;

		var from = transform.position + Vector3.up;
		Gizmos.DrawLine(from, from + Body.velocity);


		Gizmos.color = origin;
	}

	//public void OnCollisionEnter( Collision collision ) {
	//	Debug.Log(collision.impulse);
	//}
}
