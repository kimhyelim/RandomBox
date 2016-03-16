using UnityEngine;
using System.Collections;
using UnityEngine.UI;

//플레이어에 사용되는스크립트
//기능 : 플레이어의 움직임


public class PlayerMove : MonoBehaviour {
 
    private float h = 0.0f;     
    private float v = 0.0f;

    private Transform tr;
    public float speed=10.0f;       //움직이는 속도

    private GameObject headOfficeButton;

    void Start()
    {
        tr = GetComponent<Transform>();
    }

    void Update()
    {

        h = Input.GetAxis("Horizontal");
        v = Input.GetAxis("Vertical");

        Vector3 moveDir = (Vector3.forward * v) + (Vector3.right * h);

        tr.Translate(moveDir * Time.deltaTime * speed, Space.Self);
    }
}
