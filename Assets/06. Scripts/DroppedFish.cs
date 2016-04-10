using UnityEngine;
using System.Collections;

/*
 * 떨어진 생선을 몇초 후 파괴함.
 */
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
	
	//void OnDrawGizmos() {
	//	if( body == null ) return;

	//	var origin = Gizmos.color;
	//	Gizmos.color = Color.green;

	//	Gizmos.DrawLine(transform.position, transform.position + body.velocity.normalized);


	//	Gizmos.color = origin;
	//}

}
