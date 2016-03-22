using UnityEngine;
using System.Collections;

public class GameCtrl : MonoBehaviour {
	[SerializeField]
	GameObject driveObj, walkObj;

	[SerializeField]
	Renderer drivePlayerRenderer;
	[SerializeField]
	PlayerDrive playerDrive;
	[SerializeField]
	Rigidbody drivePlaterRigidBody;

	[SerializeField]
	private float startDriveMinimumLength;

	[SerializeField]
	TPSCamera tpsCam;

	GameMng data;

	TransportPoint[] tpp;

	// Use this for initialization
	void Start () {
		data = GameMng.Inst;
		activeWalk( true );
		activeDrive( false );

		tpp = Object.FindObjectsOfType<TransportPoint>();
	}

	void activeDrive( bool state ) {
		playerDrive.enabled = state;
		drivePlayerRenderer.enabled = state;
	}

	void activeWalk( bool state ) {
		walkObj.SetActive( state );
	}

	// Update is called once per frame
	void Update () {
		if ( Input.GetKeyDown(KeyCode.E) ) {
			if ( data.state == PlayerState.Walk ) {
				float dist = Vector3.Distance( driveObj.transform.position, walkObj.transform.position );
				if ( dist <= startDriveMinimumLength ) {
					activeWalk( false );
					activeDrive( true );
					tpsCam.setTarget( driveObj.transform );
					tpsCam.setType( TPSCamera.Type.BackView );
					data.state = PlayerState.Drive;
				}
				else {
					for ( int i = 0, imax = tpp.Length ; i < imax ; ++i ) {
						if ( Vector3.Distance( walkObj.transform.position, tpp[i].transform.position ) <  3f) {
							tpp[i].contact();
							break;
						}
					}
				}
			}
			else if ( data.state == PlayerState.Drive ) {
				if ( drivePlaterRigidBody.velocity.magnitude < 1f ) {
					activeWalk( true );
					activeDrive( false );
					tpsCam.setTarget( walkObj.transform );
					tpsCam.setType( TPSCamera.Type.QuarterView );
					data.state = PlayerState.Walk;

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
