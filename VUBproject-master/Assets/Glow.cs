using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Glow : MonoBehaviour
{
    Renderer rend;
    int index;
    // Start is called before the first frame update
    void Start()
    {
        rend = GetComponent<Renderer>();

        rend.material.shader = Shader.Find("ColumnGlow");
        Debug.Log(rend.material.shader);
    }

    // Update is called once per frame
    void Update()
    {

    }

    
}
