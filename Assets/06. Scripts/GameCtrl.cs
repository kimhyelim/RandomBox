using UnityEngine;
using System.Collections;

// 전반적인 게임흐름 및 조작.
public class GameCtrl : MonoBehaviour {

	[SerializeField]
	GameObject driveObj, walkObj;	// 운송중인 플레이어 게임오브젝트, 걷는중인 플레이어 게임오브젝트.
									// active 토글용.

	[SerializeField]
	Renderer drivePlayerRenderer; // 운송중인 플레이어 게임오브젝트의 렌더러.

	[SerializeField]
	PlayerDrive playerDrive; // 운송조작 컴포넌트.

	[SerializeField]
	Rigidbody drivePlayerRigidBody; // 운송 풀레이어의 리지드바디.

	[SerializeField]
	float startDriveMinimumLength; // 운송을 시작하기 위한 운송석을 중심으로하는 영역길이(반지름)

	[SerializeField]
	CameraCtrl camCtrl;

	GameMng data;

	TransportPoint[] tpp;

	// Use this for initialization
	void Start () {
		data = GameMng.Inst;
		activeState(data.state);

		tpp = Object.FindObjectsOfType<TransportPoint>();
	}
	
	// 플레이어 상태에따라 오브젝트 변경및 카메라 조작.
	void activeState(PlayerState state) {

		if (data.state == PlayerState.Walk) {

			playerDrive.enabled = false;
			drivePlayerRenderer.enabled = false;
			walkObj.SetActive(true);

			camCtrl.setTarget(walkObj.transform);
			camCtrl.setType(CameraCtrl.Type.QuarterView);
		}
		else if (data.state == PlayerState.Drive) {

			playerDrive.enabled = true;
			drivePlayerRenderer.enabled = true;
			walkObj.SetActive(false);

			camCtrl.setTarget(driveObj.transform);
			camCtrl.setType(CameraCtrl.Type.BackView);
		}

	}

	// Update is called once per frame
	void Update () {

		if ( Input.GetKeyDown(KeyCode.E) ) {

			if ( data.state == PlayerState.Walk ) {

				float dist = Vector3.Distance( driveObj.transform.position, walkObj.transform.position );

				if (dist <= startDriveMinimumLength) { // 영역안에 포함되면 수송상태로 변경.
					data.state = PlayerState.Drive;

					activeState(data.state);
				}
				else { // npc 대화 가능 영역인지 체크하여 포함되면 대화 ㄱㄱ

					for ( int i = 0, imax = tpp.Length ; i < imax ; ++i ) {
						if ( Vector3.Distance( walkObj.transform.position, tpp[i].transform.position ) <  3f) {
							tpp[i].contact();
							break;
						}
					}

				}
			}
			else if ( data.state == PlayerState.Drive ) {
				// 운송상태에서 이동중일때는 못내리게 하기위해
				// 운송 플레이어의 현재 이동 속력을 체크.
				if (drivePlayerRigidBody.velocity.magnitude < 1f) {
					data.state = PlayerState.Walk;

					activeState(data.state);
					walkObj.transform.position = driveObj.transform.position + driveObj.transform.right * 2f;
				}
			}

		}

	}



	void OnGUI() {
		GUILayout.TextField( "소지금 : " + data.money );
		Item item = data.getItem(0);
		if ( item == null ) {
			GUILayout.TextField( "생선 : " + 0 );
		}
		else {
			GUILayout.TextField( "생선 : " + item.count );
		}
	}
}
