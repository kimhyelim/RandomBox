using UnityEngine;
using System.Collections;

public class WagonCtrl : MonoBehaviour
{

    public int[] range = new int[5];
    public GameObject good;

    public GameObject[] goods;

    private bool isCrash;

    private int num;

 //   public int goodsCount;


    private Vector3 goodsDropPos;

    // Use this for initialization
    void Start()
    {
        isCrash = false;
        num = 0;

        range[0] = 20;
        range[1] = 100;
        range[2] = 500;
        range[3] = 1000;
        range[4] = 2000;



    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnCollisionEnter(Collision coll)
    {
        
        Debug.Log(coll.relativeVelocity.sqrMagnitude);

        Debug.Log("coll 벡터 : " + coll.relativeVelocity);


        //num = 1;

        if (coll.relativeVelocity.sqrMagnitude > range[0] && coll.relativeVelocity.sqrMagnitude <= range[1])
        {
            num = 1;
        }
        else if (coll.relativeVelocity.sqrMagnitude > range[1] && coll.relativeVelocity.sqrMagnitude <= range[2])
        {
            num = 3;
        }
        else if (coll.relativeVelocity.sqrMagnitude > range[2] && coll.relativeVelocity.sqrMagnitude <= range[3])
        {
            num = 5;
        }
        else if (coll.relativeVelocity.sqrMagnitude > range[3] && coll.relativeVelocity.sqrMagnitude <= range[4])
        {
            num = 8;
        }
        else if (coll.relativeVelocity.sqrMagnitude > range[4])
        {
            num = 10;
        }

		Item item =  GameMng.Inst.getItem( 0 );
		
		if ( item != null && (num > item.count|| item.count == 0))
        {
            num = item.count;
        }
        if ( item != null && isCrash == false&&coll.gameObject.tag==("BUILDING")&&num!=0)
        {
            goodsDropPos = transform.position;
            goodsDropPos.y += 0.8f;// 0.8f;
            Debug.Log(num+"마리가 떨어집니다.");
            goods = new GameObject[num];
            for (int i = 0; i < num; i++)
            {
                goods[i] = (GameObject)Instantiate(good, goodsDropPos, Quaternion.identity);
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
