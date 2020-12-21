using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Enemy : MonoBehaviour
{
    public int maxHealth = 100;
    public float minHeight;
    public float maxHeight;
    public float sideDistance = 3;

    public int damageAmount = 1;
    public MeshRenderer mr;

    // public BoxCollider sideCollider1;
    // public BoxCollider sideCollider2;
    // public BoxCollider sideCollider3;
    // public BoxCollider sideCollider4;


    // public BoxCollider sideCollider1;
    // public BoxCollider sideCollider2;
    // public BoxCollider sideCollider3;
    // public BoxCollider sideCollider4;

    public HealthSystem enemyHealth;
    public GameHandler gameHandler;

    Vector3[] spawnArea;
    GameObject player;
    Vector3 playerPosition;

    float moveCounter = 0;
    bool death;
    bool move;
    bool startMoveCounter;
    bool lookAtPlayer = true;

    public Transform startMarker;
    public Transform endMarker;

    // Movement speed in units per second.
    public float speed = 1.0F;

    // Time when the movement started.
    private float startTime;

    // Total distance between the markers.
    private float journeyLength;
    float distTraveled;

    void Start()
    {

        player = GameObject.Find("ConradJoseph");
        mr = gameObject.GetComponentInChildren<MeshRenderer>();
        gameHandler = GameObject.Find("GameHandler").GetComponent<GameHandler>();

        enemyHealth = new HealthSystem(maxHealth);

        playerPosition = player.transform.position;
        Spawn();

        startMoveCounter = true;
    }

    void Update()
    {
        // if (playerPosition != player.transform.position)
        // {
        //     playerPosition = player.transform.position;
        // }
        if (!death)
        {

            if (enemyHealth.GetHealth() <= 0)
            {
                Die();
            }
            Debug.Log(startMoveCounter);
            // if (startMoveCounter)
            // {
            //     moveCounter += 10 * Time.deltaTime;
            //     if (moveCounter > 30)
            //     {

            //         // MoveToPlayer();
            //         moveCounter = 0;
            //         move = true;

            //         playerPosition = player.transform.position;
            //         startTime = Time.time;
            //         journeyLength = Vector3.Distance(gameObject.transform.position, playerPosition);

            //         startMoveCounter = false;
            //     }
            //     else
            //     {
            //         move = false;
            //     }
            // }

            if (startMoveCounter)
            {
                Invoke("MoveToPlayer", 3f);

                startMoveCounter = false;
                playerPosition = player.transform.position;
                move = true;
                startTime = Time.time;
                journeyLength = Vector3.Distance(gameObject.transform.position, playerPosition);
            }

            if (lookAtPlayer)
            {
                transform.LookAt(player.transform);
            }
            checkHeight(minHeight, maxHeight);
        }
        // checkSides();
    }

    void FixedUpdate()
    {
        if (!death)
        {
            if (move)
            {
                MoveToPlayer();
            }
            checkHeight(minHeight, maxHeight);
            // checkSides();

        }
    }
    void OutOfBounds()
    {

    }

    void Spawn()
    {
        // transform.position = player.transform.position + Random.insideUnitSphere * 5;

        transform.position = new Vector3(player.transform.position.x, player.transform.position.y, player.transform.position.z) + Random.insideUnitSphere * 25;
    }

    void MoveToPlayer()
    {
        // transform.DOMove(playerPosition, 2f);

        //look at player
        lookAtPlayer = false;

        // Distance moved equals elapsed time times speed..
        float distCovered = (Time.time - startTime) * 0.5f;
        distTraveled = journeyLength;
        // Fraction of journey completed equals current distance divided by total distance.
        float fractionOfJourney = distCovered / journeyLength;

        // Set our position as a fraction of the distance between the markers.
        transform.position = Vector3.Lerp(gameObject.transform.position, playerPosition, fractionOfJourney);
        // Debug.Log("enemey pos" + transform.position);
        // Debug.Log("playerpos" + playerPosition);
        if (transform.position.x == playerPosition.x && transform.position.z == playerPosition.z)
        {
            Debug.Log("Recharge Move");
            startMoveCounter = true;
            move = false;
            lookAtPlayer = true;
        }

    }
    IEnumerator Reset()
    {
        yield return new WaitForSeconds(0.5f);
        transform.position = new Vector3(0, -100, 0);
        enemyHealth.SetHealth(maxHealth);
        yield return new WaitForSeconds(4f);
        mr.material = Resources.Load<Material>("Materials/Enemy_Cube");
        Spawn();
        yield return new WaitForSeconds(2f);
        MoveToPlayer();
        death = false;
    }
    void OnCollisionEnter(Collision collision)
    {
        if (!death)
        {
            if (collision.collider.tag == "Player")
            {
                Debug.Log("Enemy Hit Player");
                player.GetComponent<PlayerHandler>().playerHealth.Damage(damageAmount);
                player.GetComponentInChildren<Animator>().SetTrigger("Hit");

            }
        }

    }
    void checkHeight(float minHeight, float maxHeight)
    {
        RaycastHit hitInfoDown;
        Debug.DrawLine(transform.position + (Vector3.up * 0.1f), transform.position + (Vector3.up * 0.1f) + (Vector3.down * minHeight), Color.red);

        Physics.Raycast(transform.position + (Vector3.up * 0.1f), Vector3.down, out hitInfoDown, float.MaxValue);
        // Debug.Log(hitInfoDown.distance);
        if (hitInfoDown.distance < minHeight)
        {
            // Debug.Log("Less than Distance, cuurent Distance: " + hitInfoDown.distance);
            float height = minHeight - hitInfoDown.distance;
            transform.position = new Vector3(transform.position.x, transform.position.y + height, transform.position.z);
            // transform.DOMove(new Vector3(transform.position.x, transform.position.y + height + 0.1f, transform.position.z), 0.2f);
        }
        if (transform.position.y > maxHeight)
        {
            transform.position = new Vector3(transform.position.x, maxHeight, transform.position.z);
        }
        // if (transform.position.y < 1)
        // {
        //     transform.position = new Vector3(transform.position.x, 1, transform.position.z);
        // }
    }
    void Die()
    {
        death = true;
        Debug.Log("Enemy Died");

        //AddScore
        Reset();

        gameHandler.AddScore(2);
        mr.material = Resources.Load<Material>("Materials/Death");
        //play dead animation
        StartCoroutine(Reset());

    }
}
