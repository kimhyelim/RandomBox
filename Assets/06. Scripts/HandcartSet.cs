using UnityEngine;
using System.Collections;


/*
 * 수레의 한 세트를 관리하기 위한 목적.
 * 기본적으로 운전자를 생성함. 
 * (리디지바디나, 조인트의 문제로 처음부터 운전자를 갖고있음 ㅠㅜ )
 * 탑승시에는 운전자를 활성화, 탑승안할때는 비화성 방식.
 * 수레들의 위치를 조정하거나 회전가능.
 */
public class HandcartSet {
	public DriverCtrl driver { get; private set; }
	public HandcartCtrl[] carts { get; private set; }


	public HandcartSet( HandcartCtrl[] carts ) {
		this.carts = carts;

		driver = createDriver(carts[0].transform.position - carts[0].transform.TransformDirection(carts[0].OffsetFromTarget));

		carts[0].link(driver.body);

		activeDriver(false);

		Rigidbody[] rb = new Rigidbody[carts.Length];

		for( int i = 0; i < carts.Length; ++i ) {
			rb[i] = carts[i].Body;
		}

		driver.cars = rb;
	}


	private DriverCtrl createDriver( Vector3 pos ) {
		GameObject driver = Object.Instantiate<GameObject>( GameData.Inst.driver );
		driver.transform.position = pos;

		return driver.GetComponent<DriverCtrl>();
	}


	// 모두 지우기.
	public void clear() {

	}

	public Vector3 getDriverPosition() {
		return driver.transform.position;
	}


	public void activeDriver( bool state ) {
		driver.enabled = state;
	}



	//	public void setPosition( Vector3 pos ) {
	//		driver.transform.position = pos;
	//		carts[0].transform.position = driver.transform.position + GameData.Inst.firstHandcartOffset;
	//		for( int i = 1; i < carts.Length; ++i ) {
	//			carts[i].transform.position = carts[i - 1].transform.position + GameData.Inst.handcartInerval;
	//		}
	//	}

	//	public void rotate( float angle ) {
	//		driver.transform.Rotate(Vector3.up, angle);
	//		for( int i = 0; i < carts.Length; ++i ) {
	//			carts[i].transform.RotateAround(driver.transform.position, Vector3.up, angle);
	//		}

	//	}

}