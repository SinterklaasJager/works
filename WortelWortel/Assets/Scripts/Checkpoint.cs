using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    MeshRenderer mr;
    Material collectedMaterial;
    AudioClip bloopSound;
    // Start is called before the first frame update
    void Start()
    {
        mr = gameObject.GetComponent<MeshRenderer>();
        collectedMaterial = (Material)Resources.Load("materials/green", typeof(Material));
        bloopSound = (AudioClip)Resources.Load("Sound/GnomeGrunt1", typeof(AudioClip));
    }

    // Update is called once per frame
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag.Equals("Player"))
        {
            // collision.gameObject.PlayerHandler.playerHealth.Damage(1);
            mr.material = collectedMaterial;
            AudioSource.PlayClipAtPoint(bloopSound, Camera.main.transform.transform.position);
        }
    }
}
