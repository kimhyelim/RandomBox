using UnityEngine;
using System.Collections;

public class HandCartCtrl : MonoBehaviour {

    public GameObject fish;
    public Transform[] childsTr;
    // Use this for initialization
    void Start()
    {
        if (this.gameObject.name != ("fristCargoBay"))
        {
            GetComponent<ConfigurableJoint>().connectedBody = transform.parent.GetComponent<Rigidbody>();

        }

        childsTr = GetComponentsInChildren<Transform>();
    }


    void Update()
    {
        Vector3 vZRot = GetComponent<Transform>().transform.rotation.eulerAngles;
        if ((vZRot.z < 90 && vZRot.z > 30) || (vZRot.z < 330 && vZRot.z > 270))
        {

            Debug.Log("생성떨구기.");
            StartCoroutine(dropFish());
        }


        Vector3 vYRot = GetComponent<Transform>().transform.rotation.eulerAngles;
        if ((vYRot.y < 90 && vYRot.y > 40) || (vYRot.y < 320 && vYRot.y > 270))
        {

            for (int i = 1; i < 3; i++)
            {
                childsTr[i].gameObject.SetActive(true);

            }
        }
        else
        {

            for (int i = 1; i < 3; i++)
            {
                childsTr[i].gameObject.SetActive(false);

            }

        }

    }

    void OnCollisionEnter(Collision coll)
    {

        if (coll.gameObject.tag == ("BUILDING"))
        {
            Debug.Log("벽충돌.");
            for (int i = 0; i < 20; i++)
            {
                StartCoroutine(dropFish());
            }

        }
    }
    IEnumerator dropFish()
    {
        Vector3 fishPos = GetComponent<Transform>().position;
        fishPos.y = 0.4f;
        GameObject Fishs1 = (GameObject)Instantiate(fish, fishPos, Quaternion.identity);
        yield return null;

    }

}
