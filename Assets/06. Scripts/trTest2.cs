using UnityEngine;
using System.Collections;

public class trTest2 : MonoBehaviour {

	[SerializeField]
	WheelCollider[] wheels;


	[SerializeField]
	float currentTorque;



	void FixedUpdate() {

		float h = Input.GetAxis("Horizontal");
		float v = Input.GetAxis("Vertical");


		foreach (var e in wheels) {
			e.motorTorque = v * currentTorque;
		}

	}


}