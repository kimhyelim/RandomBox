using UnityEngine;
using System.Collections;


// 걸어다니는 상태에서의 캐릭터의 이동 조작.
public class WalkCtrl : MonoBehaviour {
	[SerializeField]
	private Animation ani;


	[SerializeField]
	private CharacterController controller;

	[SerializeField]
	private float speed = 6.0f; // 이동 속도.
	[SerializeField]
	private float gravity = 20.0f; // 중력


	private Vector3 moveDirection = Vector3.zero;
	private Transform trans;

	bool moveState = false;


	void OnEnable() {
		controller.enabled = true;
	}

	void OnDisable() {
		controller.enabled = false;
	}

	void Awake() {
		trans = transform;
		ani.CrossFade( "Idle" );
		moveState = false;
	}

	void Update() {		
		if ( controller.isGrounded ) {
			//Debug.LogFormat("{0}", 1);
			moveDirection =  - new Vector3( Input.GetAxis( "Horizontal" ), 0, Input.GetAxis( "Vertical" ) );
			//moveDirection = trans.TransformDirection(moveDirection);
			moveDirection *= speed;
		}

		moveDirection.y -= gravity * Time.deltaTime;
		controller.Move( moveDirection * Time.deltaTime );


		Vector3 dir = new Vector3( moveDirection.x, 0f, moveDirection.z ).normalized;
		if ( dir.magnitude > 0f )
			trans.forward = dir;

		//if ( moveState == false && new Vector2( moveDirection.x, moveDirection.z ).magnitude > 0f ) {
		//	ani.CrossFade( "Walk" );
		//	moveState = true;
		//}
		//else if ( moveState && new Vector2( moveDirection.x, moveDirection.z ).magnitude <= 0.1f ) {
		//	ani.CrossFade( "Idle" );
		//	moveState = false;
		//}
	}
	
	public void OnDrawGizmos() {
		var origin = Gizmos.color;
		Gizmos.color = Color.black;
		Gizmos.DrawLine( transform.position, ( transform.position + transform.forward * 3f ) );
		Gizmos.color = origin;
	}

}
