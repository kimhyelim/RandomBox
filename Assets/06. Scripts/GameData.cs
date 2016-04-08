using UnityEngine;
using System.Collections;

[System.Serializable]
public class HandcartCtrlData {
	public Vector3 relativePosition;
	public Vector3 anchor;
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
	
}
