using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class reverseMesh : MonoBehaviour
{
    Mesh mesh;
    // Start is called before the first frame update
    void Start()
    {
        mesh = GetComponent<MeshFilter>().mesh;
        mesh.triangles = mesh.triangles.Reverse().ToArray();
    }

    // Update is called once per frame
    void Update()
    {

    }




}
