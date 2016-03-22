using UnityEngine;
using System.Collections;

public class TransportPoint : MonoBehaviour {
	public float checkRadius;
	public int type = 0; // 0 = start,  1 = end

	bool complete = false;

	// Use this for initialization
	void Start() {

	}
	
	public void contact() {
		if ( complete ) return;

		if ( type == 0 ) {
			GameMng.Inst.addItem(new Item(0, 100));
		}
		else {
			int c = GameMng.Inst.getItem( 0 ).count;
			if ( c > 0 ) {
				GameMng.Inst.money += c * 100;
				GameMng.Inst.removeItem( 0 );
			}
		}

		complete = true;
	}
}

