using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class MoveReceptor : MonoBehaviour
{
    int index;
    public bool move;

    public int receptorNumber;
    public GameObject[] meshes;

    public GameObject player;
    MeshFilter meshFilter;
    MeshRenderer meshRenderer;

    GameObject newReceptor;
    GameObject receptor;
    GameObject oldReceptor;

    Vector3 currentPos;

    // Start is called before the first frame update
    void Start()
    {
        meshRenderer = GetComponent<MeshRenderer>();
        meshFilter = gameObject.GetComponent<MeshFilter>();
        newReceptor = GameObject.Find("NewReceptor");
        receptor = GameObject.Find("Receptor");
        oldReceptor = GameObject.Find("OldReceptor");
        getnewAppearence();
    }
    void moveReceptor()
    {

        if (index == 1)
        {
            transform.DOMove(receptor.transform.position, 0.8f);

        }
        else if (index == 2)
        {
            transform.DOMove(oldReceptor.transform.position, 0.8f);

        }
        else if (index == 3)
        {
            Invoke("warp", 0.2f);

        }
        Debug.Log(index);
        index = 0;
    }
    void getnewAppearence()
    {
        int randomint = Random.Range(0, meshes.Length);
        meshFilter.mesh = meshes[randomint].GetComponent<MeshFilter>().sharedMesh;
        meshRenderer.materials = meshes[randomint].GetComponent<MeshRenderer>().sharedMaterials;
        gameObject.tag = meshes[randomint].tag;
    }
    // Update is called once per frame
    void Update()
    {
        transform.LookAt(new Vector3(0, 0, 0));
        // moveReceptor();

    }

    public void setindex()
    {
        if (receptorNumber == 1)
        {
            transform.DOMove(receptor.transform.position, 0.4f).OnComplete(() => Callback(receptor));

            receptorNumber++;
        }
        else if (receptorNumber == 2)
        {
            transform.DOMove(oldReceptor.transform.position, 0.4f).OnComplete(() => Callback(oldReceptor));

            receptorNumber++;
        }
        else if (receptorNumber == 3)
        {
            Invoke("warp", 0.1f);
            receptorNumber = 1;
        }
    }
    void Callback(GameObject marker)
    {
        gameObject.transform.position = marker.transform.position;
    }

    void warp()
    {
        transform.position = newReceptor.transform.position;
        getnewAppearence();
    }

    public void Reset()
    {

    }
}
