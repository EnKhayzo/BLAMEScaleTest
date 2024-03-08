using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FloatingOrigin : MonoBehaviour{
    Transform pl;
    public float treshold = 100f;

    // Start is called before the first frame update
    void Start(){
        pl = GameObject.FindGameObjectWithTag("Player").transform;
    }

    float clamp(float f, float a, float b) {
        return System.Math.Min(System.Math.Min(f, a), b);
    }

    Vector3 clamp(Vector3 v, Vector3 a, Vector3 b) {
        return new Vector3(clamp(v.x, a.x, b.x), clamp(v.y, a.y, b.y), clamp(v.z, a.z, b.z));
    }

    // Update is called once per frame
    void Update(){
        if(pl.position.magnitude > treshold) {
            //Debug.Log(pl.position.magnitude);
            Vector3 pos = pl.position;
            foreach(GameObject g in SceneManager.GetActiveScene().GetRootGameObjects()) {
                /*if(g.tag.CompareTo("Player") != 0) */g.transform.position -= pos;
            }
            //pl.position = new Vector3(0f, 0f, 0f);
        }

        if (Input.GetKeyDown(KeyCode.R)){
            foreach (GameObject g in SceneManager.GetActiveScene().GetRootGameObjects()){
                g.transform.position = Vector3.zero;
            }
        }
    }
}
