using UnityEngine;
using System.Collections.Generic;

public class HandcartSet {
	public GameObject driver { get; private set; }
	public HandcartCtrl[] carts { get; private set; }

	public DriverCtrl driverCtrl { get; private set; }
	public Renderer[] driverRenderer { get; private set; }

	public HandcartSet( GameObject driver, HandcartCtrl[] carts ) {
		this.driver = driver;
		this.carts = carts;


		driverCtrl = driver.GetComponentInChildren<DriverCtrl>();
		driverRenderer = driver.GetComponentsInChildren<Renderer>();

		List<Rigidbody> r = new List<Rigidbody>();
		foreach ( var e in carts )
			r.Add( e.body );

		driverCtrl.cars = r.ToArray();
	}

	public void setPosition( Vector3 pos ) {
		driver.transform.position = pos;
		carts[0].transform.position = driver.transform.position + GameData.Inst.firstHandcartOffset;
		for ( int i = 1 ; i < carts.Length ; ++i ) {
			carts[i].transform.position = carts[i - 1].transform.position + GameData.Inst.handcartInerval;
		}
	}

	public void rotate(float angle) {
		driver.transform.Rotate( Vector3.up , angle);
		for ( int i = 0 ; i < carts.Length ; ++i ) {
			carts[i].transform.RotateAround( driver.transform.position, Vector3.up, angle );
		}

	}

	// 모두 지우기.
	public void clear() {

	}

	public Vector3 getDriverPosition() {
		return carts[0].transform.position + GameData.Inst.firstHandcartOffset;
	}

	public void activeDriver( bool state ) {
		driverCtrl.enabled = state;
		for ( int i = 0 ; i < driverRenderer.Length ; ++i )
			driverRenderer[i].enabled = state;
	}

	//public void setDriver(trTest drive) {
	//	if ( drive != null ) {
	//		driver.SetActive( false );
	//		drive.transform.position = getDriverPosition();
	//		drive.transform.rotation = carts[0].transform.rotation;
	//		carts[0].link( drive.body );
	//	}
	//	else {
	//		driver.transform.position = getDriverPosition();
	//		driver.transform.rotation = carts[0].transform.rotation;
	//		driver.SetActive( true );
	//	}
	//}
}

public static class HandcartManager {

	static HandcartSet cur;

	public static HandcartSet getCurrentHandcart() {
		return cur;
	}

	public static HandcartSet generate(int count) {
		Debug.Assert( count > 0, "count는 0 보다 커야됨." );

		HandcartCtrl[] ret = new HandcartCtrl[count];

		HandcartCtrl[] hcs = GameData.Inst.handcarts;

		for ( int i = 0 ; i < ret.Length ; ++i ) {
			var go = Object.Instantiate<GameObject>( hcs[i].gameObject );

			ret[i] = go.GetComponent<HandcartCtrl>();
			if ( i != 0 ) {
				ret[i].transform.position = ret[i - 1].transform.position + GameData.Inst.handcartInerval;
				ret[i].link( ret[i - 1].body );
			}
		}


		GameObject driver = Object.Instantiate<GameObject>( GameData.Inst.driver );
		driver.transform.position = ret[0].transform.position - GameData.Inst.firstHandcartOffset;

		ret[0].link( driver.GetComponent<Rigidbody>() );

		cur = new HandcartSet( driver, ret );

		cur.activeDriver( false );

		return cur;
	}

}
