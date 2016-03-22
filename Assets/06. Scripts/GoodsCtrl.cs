using UnityEngine;
using System.Collections;

public class GoodsCtrl : MonoBehaviour
{

    private float gravity;//중력가속도
    private int startAngle;//시작 각도
    public Rigidbody body;
    private float speed;//
    private float v_x;//x축으로 이동
    private float v_y;//y축으로 이동
    private float v_z;//y축으로 이동

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
    }
    public void fall(Vector3 pos)
    {

        body.detectCollisions = false;
        speed = Random.Range(10.0f, 20f);//스피드 랜덤 


		v_x = pos.x + Random.Range( -pos.x, pos.x );
		v_y = ( pos.y + 0.1f ) * 30.0f;
		v_z = pos.z + Random.Range( -pos.z, pos.z );


		//v_x = pos.x + Random.Range( -pos.x * 0.2f, pos.x * 0.2f );
		//v_y = pos.magnitude * 0.3f;
		//v_z = pos.z + Random.Range( -pos.z * 0.2f, pos.z * 0.2f );

		//Vector3 moveDir = (Vector3.forward * v_z*2) + (Vector3.right * v_x*2) + (Vector3.up * 100);
		//body.AddForce(moveDir * speed);
		body.AddForce(new Vector3(v_x, v_y, v_z) * speed);
        StartCoroutine(dropTime());

    }
    IEnumerator dropTime()
    {

		yield return new WaitForSeconds( 0.3f );
		body.detectCollisions = true;
		yield return new WaitForSeconds(2.0f);
        Destroy(this.gameObject);

    }
    void OnCollisionEnter(Collision coll)
    {
        if(coll.gameObject.tag==("CART"))
        {
            Destroy(this.gameObject);
			//coll.gameObject.GetComponent<WagonCtrl>().goodsCount += 1;
			Item item = GameMng.Inst.getItem( 0 );
			if ( item != null ) {
				item.count += 1;
			}

		}
    }

}
