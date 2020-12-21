using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEiwit : MonoBehaviour
{
    public GameObject[] Eiwits;
    float radius;
    int selectionInt;


    void Start()
    {

        instantiateEiwit(30);


    }

    // Update is called once per frame
    void Update()
    {

    }

    public void instantiateEiwit(int amount)
    {
        for (int i = 0; i < amount; i++)
        {
            selectionInt = Random.Range(0, 4);
            Debug.Log(selectionInt);
            radius = Random.Range(10, 20);
            Quaternion rotation = Quaternion.AngleAxis(i * 360 / amount, Vector3.up);
            Vector3 direction = rotation * Vector3.forward;
            Vector3 position = transform.position + (direction * radius) + new Vector3(0, Random.Range(1, 5), 0);
            GameObject prefabEiwit = Instantiate(Eiwits[selectionInt], position, rotation);
            // prefabEiwit.name = prefabEiwit.tag;
            prefabEiwit.name = "Eiwit";



        }
    }
}
