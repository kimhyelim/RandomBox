using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/* 캐릭터의 공격범위를 지정, 관리한다.
 * 지정된 태그의 게임오브젝트가 공격 범위에 들어와있는지 알 수 있다.
 * 공격 범위 : 충돌체에 들어와있고, 공격 범위각도를 만족.
 */
public class AttackScope : MonoBehaviour {
	public Transform pivot; // 검사 기준 위치.
	public string targetTag; 
	public float reach; // 공격 사정거리.
	public float angle; // 공격 영역각도.

	List<GameObject> targets = new List<GameObject>();
	
	List<GameObject> ret = new List<GameObject>();
	List<GameObject> remove = new List<GameObject>();


	void OnTriggerEnter( Collider collider ) {
		var go = collider.gameObject;
		if ( go.tag == targetTag ) {
			targets.Add( go );
		}
	}

	void OnTriggerExit( Collider collider ) {
		var go = collider.gameObject;
		if ( go.tag == targetTag ) {
			targets.Remove( go );
		}
	}

	// 현재 범위에 속한 오브젝트를 반환한다.
	public List<GameObject> getTargets() {
		ret.Clear();
		remove.Clear();
		foreach ( var e in targets ) {
			if ( e == null ) {
				remove.Add( e );
				continue;
			}
			Transform trans = e.transform;
			Vector3 dir = pivot.forward;
			Vector3 toTargetDir = (trans.position - pivot.position).normalized;

			if ( Vector3.Angle( dir, toTargetDir ) <= angle) {
				ret.Add( e );
			}
		}
		foreach ( var e in remove )
			targets.Remove(e);

		return ret;
	}

	//public void OnDrawGizmos() {
	//	var origin = Gizmos.color;
	//	Gizmos.color = Color.red;
	//	Vector3 forward = transform.forward;
		
	//	Gizmos.DrawLine( transform.position, ( transform.position + transform.forward * 3f ) );
	//	Gizmos.color = origin;
	//}

}