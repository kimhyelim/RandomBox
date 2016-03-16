using UnityEngine;
using System.Collections;

public class DropFishCtrl : MonoBehaviour {


    private float gravity;//중력가속도
    private int startAngle;//시작 각도

    private float Speed;//
    private float v_x;//x축으로 이동
    private float v_y;//y축으로 이동

    private bool ISEnd = false;


    private Transform tr;

    // Use this for initialization
    void Start()
    {
        // Tacitus = this.gameObject;
        tr = GetComponent<Transform>();

        gravity = Random.Range(0.1f, 1.0f);//중력가속도 랜덤
        Speed = Random.Range(1.0f, 2.5f);//스피드 랜덤 
        startAngle = Random.Range(0, 180);

        v_x = Speed * Mathf.Cos(startAngle * Mathf.Deg2Rad);//x축속도
        v_y = Speed * Mathf.Sin(startAngle * Mathf.Deg2Rad);//y축속도


    }

    // Update is called once per frame
    void Update()
    {
       Vector3 moveDir = (Vector3.up * v_y) + (Vector3.right * v_x);
            tr.Translate(moveDir * Time.deltaTime, Space.World);


            v_y -= gravity;

       
        if (tr.position.y < -0.5f)
        {
            Destroy(this.gameObject);
        }


    }

}
