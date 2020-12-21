using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Point : MonoBehaviour
{
    public GameHandler GameHandler;
    public int score = 10;

    int timeOut = 800;

    float timer;
    bool startTimer;
    Vector3 SpawnLocation;
    Vector3 playerPosition;

    Vector3 innerCircle;
    Vector3 outerCircle;

    public GameObject player;
    void Start()
    {
        playerPosition = player.transform.position;

    }
    void FixedUpdate()
    {
        if (playerPosition != player.transform.position)
        {
            playerPosition = player.transform.position;
        }

        if (transform.position.y < -100)
        {
            StartCoroutine(Reset());
        }
        if (startTimer)
        {
            // Debug.Log(timer);
            timer += 10 * Time.deltaTime;
            if (timer > timeOut)
            {
                startTimer = false;
                StartCoroutine(Reset());
                timer = 0;
            }
        }

    }

    void OnCollisionEnter(Collision collision)
    {
        Debug.Log("point hit");
        if (collision.collider.tag == "Player")
        {
            AddScore();
            StartCoroutine(Reset());
        }
    }

    IEnumerator Reset()
    {
        gameObject.GetComponent<Rigidbody>().useGravity = false;
        transform.position = new Vector3(0, -50, 0);
        yield return new WaitForSeconds(3f);

        Spawn();

    }

    void Spawn()
    {
        float distance = 0;

        while (distance < 80f)
        {
            SpawnLocation = new Vector3(playerPosition.x, 20, playerPosition.z) + Random.insideUnitSphere * 100;
            distance = Vector3.Distance(SpawnLocation, playerPosition);
            // Debug.Log("Distance: " + distance);
            if (distance > 80f)
            {
                gameObject.transform.position = SpawnLocation;
                gameObject.GetComponent<Rigidbody>().useGravity = true;

                startTimer = true;
            }


        }

    }
    void AddScore()
    {
        GameHandler.AddScore(score);
    }
}
