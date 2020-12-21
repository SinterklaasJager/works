using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReceptorManager : MonoBehaviour
{
    BoxCollider receptorCollider;
    SpawnEiwit spawnEiwit;

    public Material green;
    public Material red;

    public Material blue;
    MoveReceptor receptor1;
    MoveReceptor receptor2;
    MoveReceptor receptor3;

    public Material[] materials;

    public AudioClip rightAnswer;
    public AudioClip wrongAnswer;

    public GameManager gameManager;
    public GameObject[] aminozuurPrefab;
    public Material[] AZmats;
    public GameObject AZpositionprefab;
    public GameObject[] particlePrefab;

    string[] eiwitten = { "G", "A", "T", "C" };

    void Start()
    {
        gameManager = GameObject.Find("Gamemanager").GetComponent<GameManager>();
        receptorCollider = gameObject.GetComponent<BoxCollider>();
        receptor1 = GameObject.Find("Receptor1").GetComponent<MoveReceptor>();
        receptor2 = GameObject.Find("Receptor2").GetComponent<MoveReceptor>();
        receptor3 = GameObject.Find("Receptor3").GetComponent<MoveReceptor>();

        spawnEiwit = GameObject.Find("eiwitSpawner").GetComponent<SpawnEiwit>();


    }


    
    void OnTriggerEnter(Collider collider)
    {
        if (collider.name.Equals("Eiwit"))
        {
            //eiwit merging
            collider.gameObject.transform.parent = gameObject.transform;
            //check of correct is.
            if (gameObject.tag == "G" && collider.tag == "C" || gameObject.tag == "C" && collider.tag == "G" || gameObject.tag == "T" && collider.tag == "A" || gameObject.tag == "A" && collider.tag == "T")
            {
                collider.gameObject.GetComponent<Renderer>().material = green;
                gameObject.GetComponent<Renderer>().material = green;
                gameManager.AminoZuurAmount++;
                getpoints(10 + gameManager.AminoZuurAmount);
                gameObject.gameObject.GetComponent<AudioSource>().PlayOneShot(rightAnswer);
                //aminozuur spawnen

                GameObject AZ = Instantiate(aminozuurPrefab[Random.Range(0, aminozuurPrefab.Length)], AZpositionprefab.transform.position, Quaternion.Euler(Random.Range(0, 360), Random.Range(0, 360), Random.Range(0, 360)));
                AZ.tag = "AminoZuur";
                AZ.transform.parent = AZpositionprefab.transform;

                if (gameManager.AminoZuurAmount >= 10)
                {
                    gameManager.AminoZuurAmount = 0;
                    Instantiate(particlePrefab[0], AZpositionprefab.transform.position, Quaternion.identity);
                    deleteAminzuren();
                    gameManager.setpoints(30);
                }
            }
            else
            {
                pointMutate();
                gameObject.GetComponent<Renderer>().material = red;
                gameObject.gameObject.GetComponent<AudioSource>().PlayOneShot(wrongAnswer);
                if (gameManager.AminoZuurAmount > 0) {
                    gameManager.AminoZuurAmount = 0;
                    gameManager.setpoints(0);
                    deleteAminzuren();
                    Instantiate(particlePrefab[1], AZpositionprefab.transform.position, Quaternion.identity);
                }
                
                
            }
            Destroy(collider.gameObject);
            spawnEiwit.instantiateEiwit(1);
            MoveReceptor();
        }
    }

    

    void deleteAminzuren()
    {
        GameObject[] azToDestroy = GameObject.FindGameObjectsWithTag("AminoZuur");
        for (int i = 0; i < azToDestroy.Length; i++)
        {
            Destroy(azToDestroy[i]);
        }
    }

    void MoveReceptor()
    {
        receptor1.setindex();
        receptor2.setindex();
        receptor3.setindex();
    }
    void getpoints(int amount)
    {
        gameManager.setpoints(amount);
    }
    void pointMutate()
    {
        gameManager.setPointMutationPoints();

    }


}
