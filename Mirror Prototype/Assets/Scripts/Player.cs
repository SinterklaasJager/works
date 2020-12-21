using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class Player : NetworkBehaviour
{
    public void Start()
    {
        gameObject.name = "Player_" + netId;
        if (isLocalPlayer)
        {
            gameObject.name += "_LOCAL";
        }

    }
}
