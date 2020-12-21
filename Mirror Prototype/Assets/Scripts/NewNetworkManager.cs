using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using Vuforia;

public class NewNetworkManager : NetworkManager
{
    public UIHandler uIHandler;


    // DemoSharedObject demoSharedObj;
    RocketHandler rocketHandler;


    public List<Player> players = new List<Player>();

    public override void OnStartServer()
    {
        Debug.Log("Server Started");

        base.OnStartServer();
        GameObject sharedObj = Instantiate(spawnPrefabs.Find(prefab => prefab.name == "LaunchPad"), new Vector3(-0.12f, 0, 0.12f), Quaternion.identity, GameObject.Find("ImageTarget").transform);

        sharedObj.name = "LaunchPad";
        rocketHandler = sharedObj.GetComponent<RocketHandler>();

        NetworkServer.Spawn(sharedObj);

    }
    void OnStatusChanged(TrackableBehaviour.Status status, TrackableBehaviour.StatusInfo statusInfo)
    {
        Debug.LogFormat("Status is: {0}, statusInfo is: {1}", status, statusInfo);

        if (status.ToString() == "TRACKED")
        {
            Debug.Log("if status == tracked UwU");
        }
    }

    public override void OnStopServer()
    {
        Debug.Log("Server Stopped");

    }
    public override void OnClientConnect(NetworkConnection conn)
    {
        Debug.Log("Connected to server");

    }

    public override void OnClientDisconnect(NetworkConnection conn)
    {

        Debug.Log("Disconnected from server");
    }

    public override void OnServerAddPlayer(NetworkConnection conn)
    {

        GameObject player = Instantiate(playerPrefab, Vector3.zero, Quaternion.identity);
        Player newPlayer = player.GetComponent<Player>();
        players.Add(newPlayer);
        NetworkServer.AddPlayerForConnection(conn, player);

        // demoSharedObj.numberOfPlayersOnline++;
        rocketHandler.numberOfPlayersOnline++;

        if (numPlayers == 2)
        {
            // demoSharedObj.RpcAllPlayersConnected();
            rocketHandler.RpcAllPlayersConnected();

        }

    }

    // public ServerVars GetServerVars()
    // {
    //     return syncVars;
    // }
}
