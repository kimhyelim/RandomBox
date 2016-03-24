using UnityEngine;
using System.Collections;

// 운송의 시작지, 도착지 처리.
// 아직 임시. 기획 되는대로 수정 ㄱㄱ
public class TransportPoint : MonoBehaviour {
	public float checkRadius;
	public int type = 0; // 0 = start,  1 = end

	bool complete = false;

	
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

