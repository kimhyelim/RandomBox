using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AttackHit {
	public GameObject go;
	public float damage;
	public float spasticityTime;
}

public class PlayerController : MonoBehaviour {
	public enum State {
		Default,
		Attack,
		Hit
	}

	// 이동 관련.

	public float rotationScale = 1.0f; // 마우스로 캐릭터 회전시 회전속도 스케일.
	public float speed = 6.0F; // 이동 속도.
	public float jumpSpeed = 8.0F;
	public float gravity = 20.0F;

	private Vector3 moveDirection = Vector3.zero;
	private CharacterController controller;

	// 공격 관련.

	public AttackScope attackScope;
	public float attackDelay; // 공격 딜레이.
	public float attackDamage; // 공격력.
							   //public float spasticityTime; // 피격시 경직 시간.

	public int hp;
	private State state;

	private Transform trans;
	private Vector3 lastMousePos, lastPlayerPos;
	private int hitIdex = 0;


	void Awake() {
		trans = transform;
		controller = GetComponent<CharacterController>();
	}

	void Start() {
		state = State.Default;
		lastMousePos = Input.mousePosition;
		lastPlayerPos = trans.position;
	}

	void Update() {

		// 마우스 이동으로 캐릭터 회전.
		//float mouseDeltaX = (Input.mousePosition - lastMousePos).x;
		//trans.Rotate(Vector3.up, mouseDeltaX * rotationScale);

		//lastMousePos = Input.mousePosition;



		//Vector3 dir = new Vector3( Input.GetAxis( "Horizontal" ), 0, Input.GetAxis( "Vertical" ) );



		// 이동 처리.
		if (controller.isGrounded) {
			moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
			//moveDirection = trans.TransformDirection(moveDirection);
			moveDirection *= speed;
			//if (Input.GetButton("Jump"))
			//	moveDirection.y = jumpSpeed;

		}
		moveDirection.y -= gravity * Time.deltaTime;
		controller.Move(moveDirection * Time.deltaTime);
		
		Vector3 dir = new Vector3( moveDirection.x, 0f, moveDirection.z).normalized;
		if(dir.magnitude > 0f)
			trans.forward = dir;
		
		//trans.LookAt();

		//Vector3 deltaPos = trans.position - lastPlayerPos;
		//trans.LookAt( trans.position + deltaPos );

		//lastPlayerPos = trans.position;

		// 공격.
		if (Input.GetMouseButtonDown(0)) {
			StartCoroutine(attack());
		}
	}

	// 피격 처리.
	public IEnumerator hit(AttackHit hit) {
		state = State.Hit;
		hp -= Mathf.CeilToInt(hit.damage);

		Debug.Log("hit");
		if (hp <= 0) {
			die();
		}
		else {
			// 넉백 처리.
			var attackerTrans = hit.go.transform;
			Vector3 attackerPos = attackerTrans.localPosition;
			Vector3 hitDir = (trans.localPosition - attackerPos).normalized;

			float backwardTime = 0.5f;
			float t = 0;
			hitIdex++;
			int curHitIdex = hitIdex;
			while (t < backwardTime && curHitIdex == hitIdex) {
				t += Time.deltaTime;
				controller.Move((backwardTime - t) * 0.2f * hitDir);
				yield return null;
			}
		}
		state = State.Default;
	}
	
	void die() {
		Debug.Log("die player.");
		trans.GetComponentInChildren<Renderer>().enabled = false; // 캐릭터 랜더 끔. (임시)
	}

	// 공격처리.
	// 공격 범위에 있는 게임 오브젝트들에게 hit 메세지 보내기.
	IEnumerator attack() {
		state = State.Attack;
		yield return new WaitForSeconds( attackDelay );

		var targets = attackScope.getTargets();

		AttackHit ag = new AttackHit();
		ag.go = gameObject;
		ag.damage = attackDamage;
		ag.spasticityTime = 0.5f;

		foreach ( var e in targets ) {
			e.SendMessage( "hit", ag );
		}

		state = State.Default;
	}


	public void OnDrawGizmos() {
		var origin = Gizmos.color;
		Gizmos.color = Color.black;
		Gizmos.DrawLine( transform.position, ( transform.position + transform.forward * 3f ) );
		Gizmos.color = origin;
	}
}