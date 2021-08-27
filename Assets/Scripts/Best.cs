using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;
using UnityEngine.Tilemaps;

public class Best : MonoBehaviour
{
    //Collider2D col;
    public Transform shadowCastersContainer;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    public void Bleb()
    {
        Transform camPos = GameObject.FindGameObjectWithTag("MainCamera").transform;

        //col = GetComponent<TilemapCollider2D>();
        //var myMesh = col.CreateMesh(true, true);

        CompositeCollider2D tilemapCollider = GetComponent<CompositeCollider2D>();
        Debug.Log(tilemapCollider.pathCount);

        for (int i = 0; i < tilemapCollider.pathCount; i++)
        {
            Vector2[] path = new Vector2[tilemapCollider.GetPathPointCount(i)];
            tilemapCollider.GetPath(i, path);

            GameObject newObj = new GameObject();
            newObj.transform.parent = shadowCastersContainer;
            ShadowCaster2D caster = newObj.AddComponent<ShadowCaster2D>();
            //caster.customShapePath = new Vector3[] { myMesh.vertices[myMesh.triangles[i]], myMesh.vertices[myMesh.triangles[i+1]], myMesh.vertices[myMesh.triangles[i+2]] };
            caster.customShapePath = (from point in path select (Vector3) point).ToArray();

            caster.selfShadows = true;
            caster.goGoMesh = true;
            caster.camPosition = camPos;

            newObj.name = "shadow: " + i;
        }
        
    }
}

/*public class Poly
{
    public List<Vector3> Verts { get; private set; }
    private List<(int, int)> edges;
    public bool[] triCheck;

    private Vector3? shared1;
    private Vector3? shared2;

    private int newPolyPos1;
    private int newPolyPos2;

    public Poly()
    {
        Verts = new List<Vector3>();
        edges = new List<(int, int)>();
    }

    public bool MatchCheck(Vector3[] poly)
    {
        shared1 = null;
        shared2 = null;

        for (int i = 0; i < poly.Length; i++)
        {
            if (Verts.Contains(poly[i]))
            {
                if (shared1 == null)
                {
                    shared1 = poly[i];
                    newPolyPos1 = i;
                }
                else
                {
                    shared2 = poly[i];
                    newPolyPos2 = i;
                    return true;
                }
            }
        }

        return false;
    }

    public void Add(Vector3[] poly)
    {
        int oldPolyPos1 = Verts.IndexOf((Vector3)shared1);
        int oldPolyPos2 = Verts.IndexOf((Vector3)shared2);

        // 

        for (int i = 0; i < Verts.Count; i++)
        {
            
        }


        


    }
}
*/

#if (UNITY_EDITOR) 

[CustomEditor(typeof(Best))]
public class BestEditor : Editor
{

    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        if (GUILayout.Button("Generate"))
        {
            var generator = (Best)target;

            //Undo.RecordObject(generator.shadowCastersContainer, "GridShadowCastersGenerator.generate"); // this does not work :(

            generator.Bleb();

            // as a hack to make the editor save the shadowcaster instances, we rename them now instead of when theyre generated.

            
        }
    }
}

#endif