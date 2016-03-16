using UnityEngine;
using System.Collections;

public class TransportManagerCtrl : MonoBehaviour {
    public GameObject lastHandCart;

    public GameObject pCart;

    public
	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
        if (Vector3.Distance(GameObject.FindGameObjectWithTag("NPC").GetComponent<Transform>().transform.position, GameObject.FindGameObjectWithTag("PLAYER").GetComponent<Transform>().transform.position)<1)
        {
            Debug.Log("거리내에 들어옴");
            if (Input.GetMouseButtonDown(0))
            {
                Debug.Log("생성가능 ");
                GameObject newCart = (GameObject)Instantiate(pCart, pCart.transform.position, Quaternion.identity);
                newCart.transform.parent = lastHandCart.transform;
                newCart.transform.localPosition = -Vector3.forward * 1.5f;
                newCart.transform.LookAt(lastHandCart.transform.position);
                GameObject.Find("Player").GetComponent<Test>().speed += 60;
                GameObject.Find("Player").GetComponent<Test>().rotSpeed += 4;
                lastHandCart = newCart;
            }
        }


	}
}
