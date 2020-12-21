using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EiwitRotation : MonoBehaviour
{
    GameObject player;
    int rotationspeed;
    public bool isstatic;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("OVRPlayerController");
        if (gameObject.name.Equals("Eiwit"))
        {

            rotationspeed = Random.Range(-25, 25);
        }
        else
        {
            rotationspeed = Random.Range(-5, 5);
        }

    }

    // Update is called once per frame
    void Update()
    {
        if (!isstatic)
        {
            transform.RotateAround(player.transform.position, Vector3.up, rotationspeed * Time.deltaTime);
            transform.LookAt(player.transform);
        }


    }
}
