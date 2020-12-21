using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dmgDone : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

        void OnCollisionEnter(Collision collision) {
        //Output the Collider's GameObject's name
        Debug.Log(collision.collider.name);
        if (collision.collider.name == "Obstacle0")
        {
            // GameHandler.healthSystemP2.Damage(10);
        }
    }

}
