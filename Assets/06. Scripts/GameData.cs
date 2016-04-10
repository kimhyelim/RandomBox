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

	public HandcartPosSetData[] handcartPosSetDatas;	
	public GameObject driver;
	public GameObject dropFish;
	
	

	// Use this for initialization
	void Awake () {
		DontDestroyOnLoad(gameObject);
	}

	public HandcartCtrl getHandcartCtrl(int code, HandcartPosType posType) {
		if( posType == HandcartPosType.Front )
			return handcartPosSetDatas[code].front;
		else if( posType == HandcartPosType.Other )
			return handcartPosSetDatas[code].other;
		
		Debug.LogError("없는 위치 타입.");
		return null;
	}
	
}
