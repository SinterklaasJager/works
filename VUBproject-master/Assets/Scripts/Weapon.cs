using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Weapon : MonoBehaviour
{
    // Start is called before the first frame update
    public OVRInput.Controller controller;
    public OVRInput.Button pointyShooty;
    public GameObject shootPoint;

    public AudioClip shootSound;
    public AudioClip hitSound;

    EiwitRotation eiwitRotation;
    public GameManager gameManager;
    GameObject go;

    AudioSource audioSource;

    public ParticleSystem[] effects;



    // OVRInput.Controller controllerMask = OVRInput.Controller.Active;

    //Axis2D.PrimaryIndexTrigger
    void Start()
    {
        pointyShooty = OVRInput.Button.PrimaryIndexTrigger;
        audioSource = gameObject.GetComponent<AudioSource>();



    }

    IEnumerator VibrationAfterShot()
    {

        OVRInput.SetControllerVibration(0.2f, 0.8f, controller);
        yield return 0.08f;
        audioSource.PlayOneShot(hitSound);
        OVRInput.SetControllerVibration(0f, 0f, controller);
    }

    public void StartVibrate()
    {
        OVRInput.SetControllerVibration(1f, 1f, controller);
    }

    public void StopVibrate()
    {
        OVRInput.SetControllerVibration(0f, 0f, controller);
    }

    void Update()
    {
        if (OVRInput.GetDown(pointyShooty, controller) && !gameManager.GameOver)
        {
            Debug.Log("Shoot");
            audioSource.PlayOneShot(shootSound);
            // ParticleSystem.Instantiate(muzzleFlash, new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, gameObject.transform.position.z));
            effects[0].Play();

            Instantiate(effects[1], shootPoint.transform.position, shootPoint.transform.rotation);
            Ray ray = new Ray(transform.position, transform.forward);
            //Physics.SphereCast(landingRay, radius, out hit, distance
            // Debug.DrawRay(transform.position,transform.forward,Color.red,1f);
            RaycastHit hit;
            if (Physics.SphereCast(ray,0.1f, out hit, 1000))
            {
                if (hit.transform.gameObject.name.Equals("Eiwit")
                )
                {
                    go = hit.transform.gameObject;
                    StartCoroutine(VibrationAfterShot());
                    eiwitRotation = go.GetComponent<EiwitRotation>();
                    eiwitRotation.isstatic = true;


                    // hit.transform.gameObject.GetComponent<Renderer>().material = Resources.Load<Material>("Resources/Materials/Green");
                    hit.transform.gameObject.GetComponent<moveBall>().hit = true;

                    Instantiate(effects[2], hit.transform.position, Quaternion.identity);
                }
            }


        }
        
    }

}
