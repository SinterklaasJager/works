// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;
// using Mirror;


// public class DemoSharedObject : NetworkBehaviour
// {
//     [SyncVar(hook = nameof(OnColorChanged))]
//     Color color;


//     [SyncVar(hook = nameof(OnOnlinePlayersChanged))]
//     public int numberOfPlayersOnline;
//     [SyncVar(hook = nameof(OnReadyPlayersChanged))]
//     public int numberOfPlayersReady;

//     MeshRenderer meshRenderer;

//     UIHandler uiHandler;

//     public override void OnStartClient()
//     {
//         base.OnStartLocalPlayer();
//         meshRenderer = gameObject.GetComponentInChildren<MeshRenderer>();
//         uiHandler = GameObject.Find("UI").GetComponent<UIHandler>();
//         uiHandler.demoSharedObject = this;
//     }

//     [Command(ignoreAuthority = true)]
//     public void CmdChangeColor(Color updateColor)
//     {
//         color = updateColor;
//     }

//     public void OnColorChanged(Color oldValue, Color newValue)
//     {

//         meshRenderer.material.color = newValue;
//     }

//     public void OnOnlinePlayersChanged(int oldValue, int newValue)
//     {
//         uiHandler.txtOnline.text = newValue.ToString();
//     }

//     public void OnReadyPlayersChanged(int oldValue, int newValue)
//     {
//         uiHandler.txtReady.text = newValue.ToString();
//     }

//     [ClientRpc]
//     public void RpcAllPlayersConnected()
//     {
//         uiHandler.EnableUI();
//     }

//     [ClientRpc]
//     public void RpcAllPlayersReady()
//     {
//         uiHandler.ActivateButton();
//     }

//     [Command(ignoreAuthority = true)]
//     public void SendReady()
//     {
//         numberOfPlayersReady++;
//         if (numberOfPlayersReady == ((NewNetworkManager)NewNetworkManager.singleton).players.Count)
//         {
//             CmdChangeColor(new Color(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f)));
//             RpcAllPlayersReady();
//             numberOfPlayersReady = 0;
//         }
//     }


// }
