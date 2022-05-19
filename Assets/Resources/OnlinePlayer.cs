using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class OnlinePlayer : MonoBehaviourPunCallbacks
{
    public static GameObject LocalPlayerInstance;

    private void Awake()
    {
        if(photonView.IsMine)
        {
            LocalPlayerInstance = gameObject;
        }
    }
}
