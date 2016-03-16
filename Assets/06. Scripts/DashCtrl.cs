using UnityEngine;
using System.Collections;

public class DashCetl : MonoBehaviour {
    GameObject parents;
    // Use this for initialization
    void Start () {
        parents = transform.parent.gameObject;
    }
	
	// Update is called once per frame
	void Update () {

        Vector3 vYRot = parents.GetComponent<Transform>().transform.rotation.eulerAngles;
     
        if ((vYRot.y < 90 && vYRot.y > 30) || (vYRot.y < 330 && vYRot.y > 270))
        {

         
                this.gameObject.SetActive(true);

        }
        else
        {

        
                this.gameObject.SetActive(false);


        }

    }
}
