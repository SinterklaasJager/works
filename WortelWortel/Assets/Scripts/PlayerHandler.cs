using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHandler : MonoBehaviour
{
    public HealthSystem playerHealth;
    public Animator animator;

    public GameObject GameOverScreen;

    bool death;

    Rigidbody rb;

    public GameObject[] levens;
    void Start()
    {
        playerHealth = new HealthSystem(5);
        rb = gameObject.GetComponent<Rigidbody>();
    }

    void dying()
    {
        animator.SetTrigger("Death");
        rb.constraints = RigidbodyConstraints.FreezePosition;
        death = true;
    }

    void HealtIconUpdater()
    {
        int currentHealth = playerHealth.GetHealth();

        for (int i = 0; i < levens.Length; i++)
        {
            if (i > currentHealth - 1)
            {
                levens[i].SetActive(false);
            }
            else
            {
                levens[i].SetActive(true);
            }
        }


    }

    void Update()
    {
        HealtIconUpdater();
        if (Input.GetKey(KeyCode.P))
        {
            playerHealth.Damage(1);
        }

        if (playerHealth.GetHealth() == 5)
        {

        }

        if (playerHealth.GetHealth() <= 0)
        {
            Debug.Log("Death");
            if (death == false)
            {
                dying();
            }
        }

        if (death)
        {
            GameOverScreen.SetActive(true);
        }

    }
}
