using UnityEngine;
using System.Collections;

[System.Serializable]
public class HandcartCtrlData {
	public Vector3 relativePosition;
	public Vector3 anchor;
}

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

	public Vector3 firstHandcartOffset;
	public Vector3 handcartInerval;

	public HandcartCtrl[] handcarts;

	public GameObject driverDummy;
	public GameObject driver;

	public HandcartCtrlData[] handcartCtrlDatas;
	

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
