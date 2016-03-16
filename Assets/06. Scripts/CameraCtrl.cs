using UnityEngine;
using System.Collections;
using UnityEngine.UI;

//카메라에 사용되는 스크립트
//기능 : 상황에 따라 카메라 시점 변환
public class CameraCtrl : MonoBehaviour {


    private Transform tr;                                          

    //MOVE시점
    public Transform targetTr;                                      //타겟오브젝트                            
    public float dist = 10.0f;                                      //타겟와의 거리
    public float height = 4.0f;                                     //타겟과의 높이
    public float dampTrace = 10.0f;

          
    
	void Start () {
        tr = GetComponent<Transform>();

       
	}
	
	void FixedUpdate() {
        tr.position = Vector3.Lerp(tr.position, targetTr.position - (targetTr.forward * dist) + (Vector3.up * height), Time.deltaTime * dampTrace);    //카메라 위치 설정
        tr.LookAt(targetTr.position);                                 //타겟을 바라보게
       
    }
}
