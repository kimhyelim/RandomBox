using UnityEngine;
using System.Collections.Generic;

/*
   수레 단계
  스텟?
  물리적 특성 (질량, 부피, 마찰)

	 */
public class Handcart {
	public int code { get; private set; }

	public Handcart( int code ) {
		this.code = code;
	}

}

public class HandcartSet {
	public DriverCtrl driver { get; private set; }
	public HandcartCtrl[] carts { get; private set; }
	

	public HandcartSet( HandcartCtrl[] carts ) {
		this.carts = carts;

		driver = createDriver(carts[0].transform.position - carts[0].OffsetFromTarget);
		
		carts[0].link(driver.body);

		activeDriver(false);
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


	public void activeDriver(bool state) {
		driver.enabled = state;
	}

	
}