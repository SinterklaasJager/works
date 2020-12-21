using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class RocketHandler : NetworkBehaviour
{
    GameObject rocket, launchPad;

    [SyncVar(hook = nameof(OnOnlinePlayersChanged))]
    public int numberOfPlayersOnline;
    [SyncVar(hook = nameof(OnReadyPlayersChanged))]
    public int numberOfPlayersReady;

    [SyncVar(hook = nameof(OnStageChanged))]
    public int stage;

    UIHandler uiHandler;

    public override void OnStartClient()
    {
        base.OnStartLocalPlayer();

        uiHandler = GameObject.Find("UI").GetComponent<UIHandler>();
        uiHandler.rocketHandler = this;
        rocket = GameObject.Find("Rocket");
    }

    public void OnStageChanged(int oldValue, int newValue)
    {
        uiHandler.txtStage.text = newValue.ToString();
        cmdSpawnRocketRequest();


    }

    public void OnOnlinePlayersChanged(int oldValue, int newValue)
    {
        uiHandler.txtOnline.text = newValue.ToString();
    }

    public void OnReadyPlayersChanged(int oldValue, int newValue)
    {
        uiHandler.txtReady.text = newValue.ToString();
    }

    [ClientRpc]
    public void RpcAllPlayersConnected()
    {
        uiHandler.EnableUI();
    }

    [ClientRpc]
    public void RpcAllPlayersReady()
    {
        uiHandler.ActivateButton();
    }

    [Command(ignoreAuthority = true)]
    public void SendReady()
    {
        numberOfPlayersReady++;
        if (numberOfPlayersReady == ((NewNetworkManager)NewNetworkManager.singleton).players.Count)
        {

            RpcAllPlayersReady();
            numberOfPlayersReady = 0;
            if (stage < 4)
            {
                stage++;
            }
            else
            {

                stage = 0;
            }
        }
    }

    [Command(ignoreAuthority = true)]
    void cmdSpawnRocketRequest()
    {
        if (stage == 1)
        {
            Debug.Log("Stage 0: " + rocket.transform.GetChild(0));
            rocket.transform.GetChild(0).gameObject.SetActive(true);
        }
        else if (stage == 2)
        {
            rocket.transform.GetChild(1).gameObject.SetActive(true);

        }
        else if (stage == 3)
        {
            Debug.Log("stage 2");
            rocket.transform.GetChild(2).gameObject.SetActive(true);
        }
        else if (stage == 4)
        {
            Debug.Log("Fly Away");
            rocket.transform.Translate(0, 3, 0);

            if (rocket.transform.position.y >= 2 || stage == 4)
            {
                Debug.Log("Reset");
                for (int i = 0; i < rocket.transform.childCount; ++i)
                {
                    rocket.transform.GetChild(i).gameObject.SetActive(false);
                }
                rocket.transform.position = new Vector3(0f, 0f, 0f);
            }

        }
        else
        {
            Debug.Log("Something went wrong building the rocket");
        }
    }

}
