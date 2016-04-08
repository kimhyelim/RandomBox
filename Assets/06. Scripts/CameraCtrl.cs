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
	float distance;

	[SerializeField]
	float yCameraUp;        //카메라가 얼만큼 위에있는가.

	[SerializeField]
	float delayTime = 0.15f;

	[SerializeField]
	Type type;

	[SerializeField]
	string obstacleTagName;
	[SerializeField]
	float minimumDistance;
	


	private Transform trans;

	
	Vector3 dest; // 카메라 목적 위치.
	Vector3 targetPos; // 카메라가 바라볼 위치.
	Vector3 dir; // 타겟의 위치로 부터의 카메라의 방향.

	Quaternion quat; // 회전 임시 변수.

	void Awake() {
		trans = transform;
	}

	void LateUpdate() {
		targetPos = target.position;
		targetPos.y += yCameraUp;


		if ( type == Type.BackView ) {
			quat = Quaternion.AngleAxis( angle, target.right );
			dir = quat * -target.forward;

			//dest = targetPos + target.TransformDirection ( distance );
		}
		else if ( type == Type.QuarterView ) {
			quat = Quaternion.AngleAxis( -angle, Vector3.right );
			dir = quat * Vector3.forward;
		}

		bool existObstacle = selectDistance(out dest);

		if( !existObstacle )
			dest = targetPos + dir * distance;


		var delta = dest - trans.position;
		trans.position += delta * Time.deltaTime / delayTime;
		

		trans.LookAt(targetPos);

#if UNITY_EDITOR
		if ( !Application.isPlaying )
			trans.position = dest;
#endif
	}

	//플레이어가 카메라가 원래 있어야하는 위치로  레이를쏜다
	bool selectDistance( out Vector3 position ) {
		RaycastHit[] hits = Physics.RaycastAll(targetPos, dir, distance);
		
		//if( hits.Length != 0 ) {
		//	Debug.Log("obstacle " + hits[0].collider.gameObject.name);
		//}

		for( int i = 0; i < hits.Length; ++i ) {
			var hitDist = hits[i].distance;

			if( hits[i].collider.CompareTag(obstacleTagName) 
				&& hitDist < distance) {

				if( hitDist > minimumDistance ) {
					position = hits[i].point;
				}
				else {
					position = targetPos + dir * minimumDistance;
				}
				
				return true;
			}
		}

		position = Vector3.zero;
		return false;
	}
	

	public void setTarget( Transform target ) {
		this.target = target;
	}

	public void setType( Type type ) {
		this.type = type;
	}

}
