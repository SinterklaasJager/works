using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using UnityEngine.UI;

public class UIHandler : MonoBehaviour
{
    public Button btnReady;

    public Text txtOnline;
    public Text txtReady;
    public Text txtStage;

    //TODO: What is the best way to get a reference to the sharedobj from here?
    // public DemoSharedObject demoSharedObject;
    public RocketHandler rocketHandler;

    public void EnableUI()
    {
        btnReady.gameObject.SetActive(true);

    }

    public void ActivateButton()
    {
        btnReady.interactable = true;
    }

    public void SendPlayerReady()
    {
        btnReady.interactable = false;
        // demoSharedObject.SendReady();
        rocketHandler.SendReady();
    }

}
