using UnityEngine;
using System.Collections.Generic;

public static class HandcartManager {

	public static HandcartSet current { get; private set; }
	

	public static HandcartSet generate( Handcart[] carts ) {
		Debug.Assert(carts.Length > 0, "count는 0 보다 커야됨.");

		HandcartCtrl[] ret = new HandcartCtrl[carts.Length];


		for( int i = 0; i < ret.Length; ++i ) {

			HandcartCtrl hc = GameData.Inst.getHandcartCtrl(carts[i].code, (i == 0) ? HandcartPosType.Front: HandcartPosType.Other);

			var go = Object.Instantiate<GameObject>( hc.gameObject );

			ret[i] = go.GetComponent<HandcartCtrl>();
			if( i != 0 ) {
				ret[i].link(ret[i - 1].Body);
			}
		}

		current = new HandcartSet(ret);

		return current;

	}


	public static void delete() {

	}

}



//public static HandcartSet generate(Handcart[] carts) {
//	Debug.Assert(carts.Length > 0, "count는 0 보다 커야됨." );

//	HandcartCtrl[] ret = new HandcartCtrl[carts.Length];

//	HandcartCtrl[] hcs = GameData.Inst.handcarts;

//	for ( int i = 0 ; i < ret.Length ; ++i ) {
//		var go = Object.Instantiate<GameObject>( hcs[i].gameObject );

//		ret[i] = go.GetComponent<HandcartCtrl>();
//		if ( i != 0 ) {
//			ret[i].transform.position = ret[i - 1].transform.position + GameData.Inst.handcartInerval;
//			ret[i].link( ret[i - 1].Body );
//		}
//	}


//	GameObject driver = Object.Instantiate<GameObject>( GameData.Inst.driver );
//	driver.transform.position = ret[0].transform.position - GameData.Inst.firstHandcartOffset;

//	ret[0].link( driver.GetComponent<Rigidbody>() );

//	cur = new HandcartSet( driver, ret );

//	cur.activeDriver( false );

//	return cur;
//}