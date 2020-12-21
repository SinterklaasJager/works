using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AminoChain : MonoBehaviour
{

    public List<GameObject> aminoZuren = new List<GameObject>();
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //0.150 naar links opschuiven
    }

    void addAminoAcid(GameObject aminozuur)
    {
        aminoZuren.Add(aminozuur);


    }
}
