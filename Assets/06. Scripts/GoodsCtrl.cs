using UnityEngine;
using System.Collections;

public class GoodsCtrl : MonoBehaviour
{
    public Rigidbody body;
    private float speed;

    private float v_x;//x축으로 이동
    private float v_y;//y축으로 이동
    private float v_z;//y축으로 이동

    public void fall(Vector3 pos)
    {

        body.detectCollisions = false;          //물체 끼리 충돌 off
        speed = Random.Range(10.0f, 20f);       //스피드 값 랜덤 


        //물품이 떨어지는 방향
		v_x = pos.x + Random.Range( -pos.x, pos.x );
		v_y = ( pos.sqrMagnitude+ 0.1f ) * 10.0f;
		v_z = pos.z + Random.Range( -pos.z, pos.z );


		body.AddForce(new Vector3(v_x, v_y, v_z) * speed);
        StartCoroutine(dropTime());

    }
    IEnumerator dropTime()
    {

		yield return new WaitForSeconds( 0.3f );
		body.detectCollisions = true;           //충돌 on
		yield return new WaitForSeconds(2.0f);
        Destroy(this.gameObject);

    }
    void OnCollisionEnter(Collision coll)
    {
        //떨어진 물품에 충돌할 경우 다시 추가
        if(coll.gameObject.tag==("CART"))
        {
            Destroy(this.gameObject);
			Item item = GameMng.Inst.getItem( 0 );
			if ( item != null ) {
				item.count += 1;
			}

		}
    }

}
