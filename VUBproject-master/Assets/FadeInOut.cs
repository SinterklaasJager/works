using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeInOut : MonoBehaviour
{
    Color ObjectColor;
    // Start is called before the first frame update
    void Start()
    {
        ObjectColor = GetComponent<Renderer>().material.color;
        ObjectColor = Color.Lerp(Color.white, Color.black, Mathf.PingPong(Time.time, 1));
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void fadeIn(){

    }
    void fadeOut(){

    }
}
