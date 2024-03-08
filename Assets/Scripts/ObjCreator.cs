using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjCreator : MonoBehaviour
{


    // Start is called before the first frame update
    void Start() {

        Vector3[] tvs = {
            new Vector3( -1f, -1f, -1f ),
            new Vector3( -1f, 1f, -1f ),
            new Vector3( 1f, 1f, -1f ),
            new Vector3( 1f, -1f, -1f ),

            new Vector3( -1f, -1f, 1f ),
            new Vector3( -1f, 1f, 1f ),
            new Vector3( 1f, 1f, 1f ),
            new Vector3( 1f, -1f, 1f ),
        };

        Vector3[] vs = {
            tvs[0], tvs[1], tvs[2], tvs[3],
            tvs[0], tvs[4], tvs[5], tvs[1],
            tvs[4], tvs[7], tvs[6], tvs[5],
            tvs[3], tvs[2], tvs[6], tvs[7],

            tvs[0], tvs[3], tvs[7], tvs[4],
            tvs[1], tvs[5], tvs[6], tvs[2],
        };

        Vector3[] norm = {
            Vector3.back,
            Vector3.back,
            Vector3.back,
            Vector3.back,

            Vector3.left,
            Vector3.left,
            Vector3.left,
            Vector3.left,
            
            Vector3.forward,
            Vector3.forward,
            Vector3.forward,
            Vector3.forward,
            
            Vector3.right,
            Vector3.right,
            Vector3.right,
            Vector3.right,
            
            Vector3.down,
            Vector3.down,
            Vector3.down,
            Vector3.down,
            
            Vector3.up,
            Vector3.up,
            Vector3.up,
            Vector3.up,
        };

        Vector2[] uv = {
            new Vector2(0f,0f), new Vector2(0f,1f), new Vector2(1f,1f), new Vector2(1f,0f),
            new Vector2(0f,0f), new Vector2(0f,1f), new Vector2(1f,1f), new Vector2(1f,0f),
            new Vector2(0f,0f), new Vector2(0f,1f), new Vector2(1f,1f), new Vector2(1f,0f),
            new Vector2(0f,0f), new Vector2(0f,1f), new Vector2(1f,1f), new Vector2(1f,0f),

            new Vector2(0f,0f), new Vector2(0f,1f), new Vector2(1f,1f), new Vector2(1f,0f),
            new Vector2(0f,0f), new Vector2(0f,1f), new Vector2(1f,1f), new Vector2(1f,0f),
        };

        int[] idx = { 
            0,1,2, 0,2,3, 
            4,5,6, 4,6,7, 
            8,9,10, 8,10,11, 
            12,13,14, 12,14,15, 

            16,17,18, 16,18,19, 
            20,21,22, 20,22,23, 
        };

        Mesh m = new Mesh();
        m.vertices = vs;
        m.normals = norm;
        m.uv = uv;
        m.triangles = idx;
        GameObject obj = new GameObject();
        obj.name = "TEST";
        MeshRenderer mr = obj.AddComponent<MeshRenderer>();
        mr.material = new Material(Shader.Find("Diffuse"));
        MeshFilter mf = obj.AddComponent<MeshFilter>();
        mf.mesh = m;
        obj.transform.position += new Vector3(5f, 0f, 0f);
        obj.transform.SetParent(GameObject.Find("PolarThingy").transform);
        //Instantiate(obj, new Vector3(0, 0, 0), Quaternion.Euler(0, 0, 0));
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
