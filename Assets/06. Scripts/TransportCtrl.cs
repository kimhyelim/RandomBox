using UnityEngine;
using System.Collections;

// 수송의 흐름 및 조작.
public class TransportCtrl : MonoBehaviour {

	[SerializeField]
	GameObject walkObj;	// 운송중인 플레이어 게임오브젝트, 걷는중인 플레이어 게임오브젝트.
						// active 토글용.
						
	[SerializeField]
	float startDriveMinimumLength; // 운송을 시작하기 위한 운송석을 중심으로하는 영역길이(반지름)

	[SerializeField]
	CameraCtrl camCtrl;

	GameMng data;
	PlayerState state;

	TransportPoint[] tpp;
	
	HandcartSet handcartSet;


	// Use this for initialization
	IEnumerator Start () {
		data = GameMng.Inst;

		tpp = Object.FindObjectsOfType<TransportPoint>();
		
		yield return new WaitForSeconds( 0.2f );
		
		handcartSet = HandcartManager.generate(data.handcarts.ToArray());

		activeState(PlayerState.Walk);
	}


	// 플레이어 상태에따라 오브젝트 변경및 카메라 조작.
	void activeState( PlayerState state ) {
		this.state = state;
		if( state == PlayerState.Walk ) {
			handcartSet.activeDriver(false);

			walkObj.SetActive(true);

			camCtrl.setTarget(walkObj.transform);
			camCtrl.setType(CameraCtrl.Type.QuarterView);
		}
		else if( state == PlayerState.Drive ) {
			handcartSet.activeDriver(true);

			walkObj.SetActive(false);


			camCtrl.setTarget(HandcartManager.current.driver.transform);
			camCtrl.setType(CameraCtrl.Type.BackView);
		}

	}

	// Update is called once per frame
	void Update () {


		if( Input.GetKeyDown(KeyCode.E) ) {

			if( state == PlayerState.Walk ) {

				float dist = Vector3.Distance( walkObj.transform.position, HandcartManager.current.getDriverPosition() );

				if( dist <= startDriveMinimumLength ) { // 영역안에 포함되면 수송상태로 변경.
					activeState(PlayerState.Drive);
				}
				else { // npc 대화 가능 영역인지 체크하여 포함되면 대화 ㄱㄱ

					for( int i = 0, imax = tpp.Length; i < imax; ++i ) {
						if( Vector3.Distance(walkObj.transform.position, tpp[i].transform.position) < 3f ) {
							tpp[i].contact();
							break;
						}
					}

				}
			}
			else if( state == PlayerState.Drive ) {
				// 운송상태에서 이동중일때는 못내리게 하기위해
				// 운송 플레이어의 현재 이동 속력을 체크.
				if( HandcartManager.current.driver.body.velocity.magnitude < 1f ) {
					activeState(PlayerState.Walk);
					
					var driver = HandcartManager.current.driver;
					walkObj.transform.position = driver.transform.position + driver.transform.right * 2f;
				}
			}

		}

	}



	//void OnGUI() {
	//	GUILayout.TextField( "소지금 : " + data.money );
	//	Item item = data.getItem(0);
	//	if ( item == null ) {
	//		GUILayout.TextField( "생선 : " + 0 );
	//	}
	//	else {
	//		GUILayout.TextField( "생선 : " + item.count );
	//	}
	//}
}
