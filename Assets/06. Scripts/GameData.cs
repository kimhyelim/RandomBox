using UnityEngine;
using System.Collections;

[System.Serializable]
public class HandcartPosSetData {
	public HandcartCtrl front, other;
}


public class GameData : MonoBehaviour {
	private static GameData inst;
	public static GameData Inst {
		get {
			if ( inst == null ) {
				inst = GameObject.FindObjectOfType<GameData>();
			}
			return inst;
		}
	}

	public HandcartPosSetData[] handcartPosSetDatas;	// 수레 정보. 배열 인덱스 == 수레코드.
	public GameObject driver; // 운전자 프리펩.
	public GameObject dropFish; // 떨어질때의 물고기 프리펩.
	
	
	// Use this for initialization
	void Awake () {
		DontDestroyOnLoad(gameObject);
	}

	// 해당 코드와 위치타입에 맞는 수레 오브젝트를 반환.
	public HandcartCtrl getHandcartCtrl(int code, HandcartPosType posType) {
		if( posType == HandcartPosType.Front )
			return handcartPosSetDatas[code].front;
		else if( posType == HandcartPosType.Other )
			return handcartPosSetDatas[code].other;
		
		Debug.LogError("없는 위치 타입.");
		return null;
	}
	
}
