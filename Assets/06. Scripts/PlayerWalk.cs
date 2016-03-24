using UnityEngine;
using System.Collections;


// 걸어다니는 상태에서의 캐릭터의 이동 조작.
public class PlayerWalk : MonoBehaviour {
	[SerializeField]
	private CharacterController controller;

	[SerializeField]
	private float speed = 6.0f; // 이동 속도.
	[SerializeField]
	private float gravity = 20.0f; // 중력


	private Vector3 moveDirection = Vector3.zero;
	private Transform trans;	
	
	void Awake() {
		trans = transform;
	}

	void Update() {		
		if ( controller.isGrounded ) {
			moveDirection = new Vector3( Input.GetAxis( "Horizontal" ), 0, Input.GetAxis( "Vertical" ) );
			//moveDirection = trans.TransformDirection(moveDirection);
			moveDirection *= speed;
		}

		moveDirection.y -= gravity * Time.deltaTime;
		controller.Move( moveDirection * Time.deltaTime );


		Vector3 dir = new Vector3( moveDirection.x, 0f, moveDirection.z ).normalized;
		if ( dir.magnitude > 0f )
			trans.forward = dir;
	}
	
	public void OnDrawGizmos() {
		var origin = Gizmos.color;
		Gizmos.color = Color.black;
		Gizmos.DrawLine( transform.position, ( transform.position + transform.forward * 3f ) );
		Gizmos.color = origin;
	}

}
