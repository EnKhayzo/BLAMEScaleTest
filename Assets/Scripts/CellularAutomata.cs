using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;
using UnityEngine.Experimental.AI;

public class CellularAutomata : MonoBehaviour
{
    public static Vector3 defSize = new Vector3(10,10,10); //default size

    public class CA{ //cellular automata
        // Vector3 cSize = new Vector3(5, 5, 5); //coordinate space size
        public Vector3 size = defSize;

        //grid in which the CA lives (3-dimensional)
        byte[] grid1;
        byte[] grid2;

        public byte[] curGrid; 
        public byte[] oldGrid;

        public bool boundCheck(Vector3 pos) {
            return pos.z < 0 || pos.z > size.z - 1 || pos.y < 0 || pos.y > size.y - 1 || pos.x < 0 || pos.x > size.x - 1;
        }

        public int toi(Vector3 v) { return (int)(v.z*size.y*size.x + v.y*size.y + v.x); }

        public bool alive(Vector3 pos) { if (boundCheck(pos)) return false; return oldGrid[toi(pos)] == 1; }
        public bool dead(Vector3 pos) { if (boundCheck(pos)) return false; return oldGrid[toi(pos)] == 0; }

        public void kill(Vector3 pos) { if (boundCheck(pos)) return; curGrid[toi(pos)] = 0; }
        public void revive(Vector3 pos) { if (boundCheck(pos)) return; curGrid[toi(pos)] = 1; }

        public int getNeighbors(Vector3 pos) {
            int num = 0;

            for (int z = -1; z < 2; z++) {
                for (int y = -1; y < 2; y++) {
                    for (int x = -1; x < 2; x++) {
                        Vector3 npos = pos + new Vector3(x, y, z);
                        if (boundCheck(npos)) continue; //if its outside the grid's bounds

                        num += alive(npos) ? 1 : 0;
                    }
                }
            }

            return num;
        }

        public void automata(Vector3 pos) {
            curGrid[toi(pos)] = oldGrid[toi(pos)];

            if (!alive(pos)) {
                int num = getNeighbors(pos);

                if (num == 1) revive(pos);
            }
        }

        public void update() {
            for(int z=0; z<size.z; z++) {
                for (int y = 0; y < size.y; y++) {
                    for (int x = 0; x < size.x; x++) {
                        automata(new Vector3(x, y, z));
                    }
                }
            }

            //swap for double buffering
            byte[] t = curGrid;
            curGrid = oldGrid;
            oldGrid = t;
        }

        public void init() {
            grid1 = new byte[(int)(size.x * size.y * size.z)];
            grid2 = new byte[(int)(size.x * size.y * size.z)];

            curGrid = grid1;
            oldGrid = grid2;
        }

        public CA() { init(); }
        public CA(Vector3 size) { this.size = size; init(); }
    };
    public List<CA> cas = new List<CA>();

    // Start is called before the first frame update
    void Start()
    {
        CA ca = new CA();
        cas.Add(ca);

        ca.oldGrid[ca.toi(new Vector3(5,5,5))] = 1;
        //ca.oldGrid[ca.toi(new Vector3(4, 4, 4))] = 1;
        float last = Time.realtimeSinceStartup;
        for (int i = 0; i < 30; i++) {
            ca.update();
        }
        float cur = Time.realtimeSinceStartup;
        Debug.Log("func took " + (cur - last) + " sec");

        last = Time.realtimeSinceStartup;

        //then generate the mesh based on the resulting state of the grid
        List<Vector3> vert = new List<Vector3>();
        List<Vector3> norm = new List<Vector3>();
        List<Vector2> uv = new List<Vector2>();
        List<int> idx = new List<int>();

        //pre-defined default quad (temporary)
        Vector3[] vs = {
            new Vector3( -1f, -1f, -1f ),
            new Vector3( -1f, 1f, -1f ),
            new Vector3( 1f, 1f, -1f ),
            new Vector3( 1f, -1f, -1f ),

            new Vector3( -1f, -1f, 1f ),
            new Vector3( -1f, 1f, 1f ),
            new Vector3( 1f, 1f, 1f ),
            new Vector3( 1f, -1f, 1f ),
        };

        Vector3[] tnorm = {
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

        Vector2[] tuv = {
            new Vector2(0f,0f), new Vector2(0f,1f), new Vector2(1f,1f), new Vector2(1f,0f),
            new Vector2(0f,0f), new Vector2(0f,1f), new Vector2(1f,1f), new Vector2(1f,0f),
            new Vector2(0f,0f), new Vector2(0f,1f), new Vector2(1f,1f), new Vector2(1f,0f),
            new Vector2(0f,0f), new Vector2(0f,1f), new Vector2(1f,1f), new Vector2(1f,0f),

            new Vector2(0f,0f), new Vector2(0f,1f), new Vector2(1f,1f), new Vector2(1f,0f),
            new Vector2(0f,0f), new Vector2(0f,1f), new Vector2(1f,1f), new Vector2(1f,0f),
        };

        for (int z = 0; z < ca.size.z; z++) {
            for (int y = 0; y < ca.size.y; y++) {
                for (int x = 0; x < ca.size.x; x++) {
                    Vector3 pos = new Vector3(x,y,z);

                    if (ca.alive(pos)) {
                        Vector3 apos = pos * 2f;
                        if (!ca.alive(pos + Vector3.back)) {
                            int oSize = (int)vert.Count;
                            vert.Add(apos + vs[0]); vert.Add(apos + vs[1]); vert.Add(apos + vs[2]); vert.Add(apos + vs[3]);
                            norm.Add(tnorm[0]); norm.Add(tnorm[1]); norm.Add(tnorm[2]); norm.Add(tnorm[3]);
                            uv.Add(tuv[0]); uv.Add(tuv[1]); uv.Add(tuv[2]); uv.Add(tuv[3]);
                            idx.Add(oSize + 0); idx.Add(oSize + 1); idx.Add(oSize + 2);  idx.Add(oSize + 0); idx.Add(oSize + 2); idx.Add(oSize + 3);
                        }
                        if (!ca.alive(pos + Vector3.left)) {
                            int oSize = (int)vert.Count;
                            vert.Add(apos + vs[0]); vert.Add(apos + vs[4]); vert.Add(apos + vs[5]); vert.Add(apos + vs[1]);
                            norm.Add(tnorm[4]); norm.Add(tnorm[5]); norm.Add(tnorm[6]); norm.Add(tnorm[7]);
                            uv.Add(tuv[4]); uv.Add(tuv[5]); uv.Add(tuv[6]); uv.Add(tuv[7]);
                            idx.Add(oSize + 0); idx.Add(oSize + 1); idx.Add(oSize + 2); idx.Add(oSize + 0); idx.Add(oSize + 2); idx.Add(oSize + 3);
                        }
                        if (!ca.alive(pos + Vector3.forward)) {
                            int oSize = (int)vert.Count;
                            vert.Add(apos + vs[4]); vert.Add(apos + vs[7]); vert.Add(apos + vs[6]); vert.Add(apos + vs[5]);
                            norm.Add(tnorm[8]); norm.Add(tnorm[9]); norm.Add(tnorm[10]); norm.Add(tnorm[11]);
                            uv.Add(tuv[8]); uv.Add(tuv[9]); uv.Add(tuv[10]); uv.Add(tuv[11]);
                            idx.Add(oSize + 0); idx.Add(oSize + 1); idx.Add(oSize + 2); idx.Add(oSize + 0); idx.Add(oSize + 2); idx.Add(oSize + 3);
                        }
                        if (!ca.alive(pos + Vector3.right)) {
                            int oSize = (int)vert.Count;
                            vert.Add(apos + vs[3]); vert.Add(apos + vs[2]); vert.Add(apos + vs[6]); vert.Add(apos + vs[7]);
                            norm.Add(tnorm[12]); norm.Add(tnorm[13]); norm.Add(tnorm[14]); norm.Add(tnorm[15]);
                            uv.Add(tuv[12]); uv.Add(tuv[13]); uv.Add(tuv[14]); uv.Add(tuv[15]);
                            idx.Add(oSize + 0); idx.Add(oSize + 1); idx.Add(oSize + 2); idx.Add(oSize + 0); idx.Add(oSize + 2); idx.Add(oSize + 3);
                        }

                        if (!ca.alive(pos + Vector3.down)) {
                            int oSize = (int)vert.Count;
                            vert.Add(apos + vs[0]); vert.Add(apos + vs[3]); vert.Add(apos + vs[7]); vert.Add(apos + vs[4]);
                            norm.Add(tnorm[16]); norm.Add(tnorm[17]); norm.Add(tnorm[18]); norm.Add(tnorm[19]);
                            uv.Add(tuv[16]); uv.Add(tuv[17]); uv.Add(tuv[18]); uv.Add(tuv[19]);
                            idx.Add(oSize + 0); idx.Add(oSize + 1); idx.Add(oSize + 2); idx.Add(oSize + 0); idx.Add(oSize + 2); idx.Add(oSize + 3);
                        }
                        if (!ca.alive(pos + Vector3.up)) {
                            int oSize = (int)vert.Count;
                            vert.Add(apos + vs[1]); vert.Add(apos + vs[5]); vert.Add(apos + vs[6]); vert.Add(apos + vs[2]);
                            norm.Add(tnorm[20]); norm.Add(tnorm[21]); norm.Add(tnorm[22]); norm.Add(tnorm[23]);
                            uv.Add(tuv[20]); uv.Add(tuv[21]); uv.Add(tuv[22]); uv.Add(tuv[23]);
                            idx.Add(oSize + 0); idx.Add(oSize + 1); idx.Add(oSize + 2); idx.Add(oSize + 0); idx.Add(oSize + 2); idx.Add(oSize + 3);
                        }
                    }
                }
            }
        }
        cur = Time.realtimeSinceStartup;
        Debug.Log("sec func took " + (cur - last) + " sec");
        
        Mesh m = new Mesh();
        m.vertices = vert.GetRange(0, vert.Count).ToArray();
        m.normals = norm.GetRange(0, norm.Count).ToArray();
        m.uv = uv.GetRange(0, uv.Count).ToArray();
        m.triangles = idx.GetRange(0, idx.Count).ToArray();
        GameObject obj = new GameObject();
        obj.name = "TEST";
        MeshRenderer mr = obj.AddComponent<MeshRenderer>();
        mr.material = new Material(Shader.Find("Diffuse"));
        MeshFilter mf = obj.AddComponent<MeshFilter>();
        mf.mesh = m;
        obj.transform.position += new Vector3(105f, 5f, -10f);
        obj.transform.SetParent(GameObject.Find("PolarThingy").transform);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
