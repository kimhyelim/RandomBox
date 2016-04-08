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
	private Transform trans;


	//float distBuilding = 1000.0f;
	//bool IsBuilding = false;
	//Vector3 pos;
	//Ray ray;
	float basicDist;

	Vector3 dir;
	void Awake() {
		distance = 10f;
		trans = transform;
		basicDist = distance;
	}

	void LateUpdate() {

		Vector3 dest = new Vector3();

		Vector3 vCenter;        //카메라가 보는 곳
		vCenter = target.position;
		vCenter.y += yCameraUp;
		trans.LookAt( vCenter );

		Quaternion q;

		seleteDistance();


		if ( type == Type.BackView ) {
			q = Quaternion.AngleAxis( angle, target.right );
			dir = q * -target.forward;
			dest = target.position + dir * distance;

			//dest = target.position + target.TransformDirection ( distance );
		}
		else if ( type == Type.QuarterView ) {
			q = Quaternion.AngleAxis( -angle, Vector3.right );
			dir = q * Vector3.forward;
			dest = target.position + dir * distance;
		}




		var delta = dest - transform.position;
		transform.position += delta * Time.deltaTime / delayTime;

		Vector3 lookAtPos = target.position;
		lookAtPos.y += yCameraUp;
		trans.LookAt( lookAtPos );


#if UNITY_EDITOR
		if ( !Application.isPlaying )
			transform.position = dest;
#endif
	}


	void seleteDistance() {
		//-------------------------------------------------------------------------------------------------------------------------------
		//플레이어가 카메라가 원래 있어야하는 위치로  레이를쏜다
		//거리내에 충돌한 건물이 없으면 다시 원상태로

		float distBuilding = 1000.0f;
		bool IsBuilding = false;


		Vector3 _dest = target.position + dir * basicDist;
		Ray _ray = new Ray( target.position, ( _dest - target.position ) );
		Debug.DrawRay( _ray.origin, _ray.direction * 100.0f, Color.blue );
		Vector3 pos = Vector3.zero;



		RaycastHit[] hits = Physics.RaycastAll( _ray );            //Hit정보를 가져온다.
		foreach ( RaycastHit hit in hits ) {
			if ( hit.collider.tag == "BUILDING" ) {
				if ( distBuilding > Vector3.Distance( target.position, hit.point ) ) {
					distBuilding = Vector3.Distance( target.position, hit.point );
					pos = hit.point;

				}
				IsBuilding = true;

			}
		}

		if ( distBuilding != 1000 ) {
			distance = Vector2.Distance( new Vector2( target.position.x, target.position.z ), new Vector2( pos.x, pos.z ) );
			yCameraUp = 1;
		}
		if ( IsBuilding == false ) {
			distBuilding = 1000f;
			distance = basicDist;
		}
		if(distance>basicDist)
		{
			distance = basicDist;
		}
		if ( ( distance ) < 2 ) {
			distance = 2f;
		}
		////-------------------------------------------------------------------------------------------------
	}
	public void setTarget( Transform target ) {
		this.target = target;
	}

	public void setType( Type type ) {
		this.type = type;
	}

}
