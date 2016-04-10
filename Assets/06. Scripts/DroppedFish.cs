using UnityEngine;
using System.Collections;

public class DroppedFish : MonoBehaviour {
	public Rigidbody body;

	float timer = 0f;

	void Update() {
		if( body.velocity.sqrMagnitude < 0.25f ) {
			timer += Time.deltaTime;

			if( timer > 3f ) {
				DestroyObject(gameObject);
			}
		}
		else {
			timer = 0f;
		}
	}

	//IEnumerator Start() {
	//	yield return new WaitForSeconds(3.0f);
	//	DestroyObject(gameObject);
	//}

	//void OnDrawGizmos() {
	//	if( body == null ) return;

	//	var origin = Gizmos.color;
	//	Gizmos.color = Color.green;

	//	Gizmos.DrawLine(transform.position, transform.position + body.velocity.normalized);


	//	Gizmos.color = origin;
	//}

}
