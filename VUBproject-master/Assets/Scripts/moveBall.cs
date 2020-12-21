using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class moveBall : MonoBehaviour
{
    GameObject ball;
    GameObject receptor;
    AudioSource audioSource;

    public bool hit;

    void Start()
    {
        receptor = GameObject.Find("Receptor");
    }

    void BallGotShot()
    {
        transform.DOMove(receptor.transform.position, 0.3f);
        transform.localScale = new Vector3(-0.5f, -0.5f, -0.5f);
        // transform.Translate(Vector3.MoveTowards(transform.position, receptor.transform.position, 2f));

    }
    void Update()
    {
        if (hit)
        {
            BallGotShot();

        }

    }
}
