using UnityEngine;
using System.Collections;

public class PopFish : MonoBehaviour {
	public Transform pivot;

	public float genRadius =1f;
		
	public void pop( int count, Vector3 veloc) {
		float maxRot = 30f; //degree

		//Debug.DrawLine(pivot.position, pivot.position + veloc, Color.cyan, 1.0f);

		for( int i = 0; i < count; ++i ) {
			GameObject go = Instantiate(GameData.Inst.dropFish);
			var body = go.GetComponent<Rigidbody>();

			var trans = go.transform;
			trans.rotation = new Quaternion(Random.Range(0f, 1f), 0f, 0f, Random.Range(0f, 1f));

			Vector3 randPos = new Vector3(Random.Range( -1f,1f), 0f, Random.Range( -1f,1f));
			randPos.Normalize();
			randPos *= genRadius * Random.Range(0.2f, 1f);

			trans.position = pivot.TransformPoint(randPos);

			Vector3 vec = trans.position - pivot.position;

			//Debug.DrawLine(pivot.position, trans.position, Color.black, 1.0f);
			//Debug.DrawLine(pivot.position, pivot.position + pivot.up , Color.black, 1.0f);
			
			float distFactor = vec.magnitude / genRadius;
			//Debug.DrawLine(pivot.position, pivot.position + cross, Color.blue, 1.0f);


			Vector3 newVeloc = Quaternion.AngleAxis(distFactor * maxRot, Vector3.Cross(pivot.up, vec.normalized)) * veloc;

			body.AddForce(newVeloc, ForceMode.Impulse);
		}

//		StartCoroutine(dd());
	}

	IEnumerator dd() {
		yield return null;
		Debug.Break();
	}

	//public void OnDrawGizmos() {
	//	Gizmos.DrawCube(pivot.position, genRegion);
	//}
}
