using UnityEngine;
using System.Collections;

public class WagonCtrl : MonoBehaviour
{
    public int[] range = new int[5];                //떨어트리는 물품 사이 값

    public GameObject preObj;

    private GameObject[] goods;                     //떨어트리는 물품들

    private bool isCrash;                           //현재 충돌 중인가?

    private int dropNum;                            //떨어지는 물품 개수



    private Vector3 goodsDropPos;

    // Use this for initialization
    void Start()
    {
        isCrash = false;
        dropNum = 0;

        range[0] = 20;
        range[1] = 100;
        range[2] = 500;
        range[3] = 1000;
        range[4] = 2000;



    }


    void OnCollisionEnter(Collision coll)
    {

        //충돌 벡터의 힘에 따라 수송 물품이 떨어지는 개수 결정
        if (coll.relativeVelocity.sqrMagnitude > range[0] && coll.relativeVelocity.sqrMagnitude <= range[1])
        {
            dropNum = 1;
        }
        else if (coll.relativeVelocity.sqrMagnitude > range[1] && coll.relativeVelocity.sqrMagnitude <= range[2])
        {
            dropNum = 3;
        }
        else if (coll.relativeVelocity.sqrMagnitude > range[2] && coll.relativeVelocity.sqrMagnitude <= range[3])
        {
            dropNum = 5;
        }
        else if (coll.relativeVelocity.sqrMagnitude > range[3] && coll.relativeVelocity.sqrMagnitude <= range[4])
        {
            dropNum = 8;
        }
        else if (coll.relativeVelocity.sqrMagnitude > range[4])
        {
            dropNum = 10;
        }

		Item item =  GameMng.Inst.getItem( 0 );
		
		if ( item != null && (dropNum > item.count|| item.count == 0))
        {
            dropNum = item.count;
        }


        //수송 물품 생성
        if ( item != null && isCrash == false&&coll.gameObject.tag==("BUILDING")&& dropNum != 0)
        {
            goodsDropPos = transform.position;
            goodsDropPos.y += 0.8f;
            Debug.Log(dropNum + "마리가 떨어집니다.");
            goods = new GameObject[dropNum];
            for (int i = 0; i < dropNum; i++)
            {	
                goods[i] = (GameObject)Instantiate(preObj, goodsDropPos, Quaternion.identity);
				goods[i].SendMessage("fall", coll.relativeVelocity);
				item.count -= 1;
            }

            isCrash = true;
        }

    }

    void OnCollisionExit(Collision coll)
    {
        if(coll.gameObject.tag==("BUILDING"))
            isCrash = false;
    }
}
