using UnityEngine;
using System.Collections;

[ExecuteInEditMode]
public class TPSCamera : MonoBehaviour {
	public Transform target;
	public Vector3 distance;

	private Transform trans;

	void Awake() {
		trans = transform;
	}

	void LateUpdate() {
		trans.position = target.position + distance;
		trans.LookAt(target);
	}

}
