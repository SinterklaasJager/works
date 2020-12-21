using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponHandler : MonoBehaviour
{
    GameObject weapon;
    bool dealingDamage;
    bool done;

    GameObject PlayerMovement;
    void Start()
    {
        done = false;
        dealingDamage = false;
        weapon = gameObject.transform.GetChild(0).gameObject;
        PlayerMovement = gameObject.transform.Find("Player").gameObject;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Swing()
    {
        // gameObject.transform.Rotate(0, 0, -80);
        // gameObject.transform.Rotate(0, 0, 80);

        StartCoroutine(swing());

    }

    public void Hack()
    {
        // gameObject.transform.Rotate(0, 0, -80);
        // gameObject.transform.Rotate(0, 0, 80);

        StartCoroutine(hack());

    }


    IEnumerator swing()
    {
        dealingDamage = true;
        //Rotate 80 deg
        transform.Rotate(-90, 0, 0, Space.Self);
        transform.Rotate(0, 0, -80, Space.Self);

        //Wait for 1 seconds
        yield return new WaitForSeconds(0.8f);
        dealingDamage = false;
        yield return new WaitForSeconds(0.2f);
        //Rotate back
        transform.Rotate(0, 0, 80, Space.Self);
        done = true;
        dealingDamage = false;
    }

    IEnumerator hack()
    {
        dealingDamage = true;
        //Rotate 80 deg
        transform.Rotate(0, 0, -80, Space.Self);

        //Wait for 1 seconds
        yield return new WaitForSeconds(0.8f);
        dealingDamage = false;
        yield return new WaitForSeconds(0.2f);
        //Rotate back
        transform.Rotate(0, 0, 80, Space.Self);
        done = true;
        dealingDamage = false;
    }
}
