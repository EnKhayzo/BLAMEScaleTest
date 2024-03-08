using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtText : MonoBehaviour
{   
    public Transform target;

    public GameObject innerText;
    public bool hideOnSmallDistance = false;
    public float smallDistanceThreshold;

    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.Find("Character").transform;
    }

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(target.transform, target.transform.up);

        if(hideOnSmallDistance){
            float dist = Vector3.Distance(transform.position, target.position);
            // Debug.Log(dist);

            if(dist > smallDistanceThreshold && !innerText.activeSelf)      innerText.SetActive(true);
            else if(dist <= smallDistanceThreshold && innerText.activeSelf)  innerText.SetActive(false);
        }
    }
}
