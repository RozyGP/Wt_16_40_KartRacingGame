using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using Photon.Realtime;
using Photon.Pun;

public class RaceLauncher : MonoBehaviourPunCallbacks
{
    //public InputField playerName;
    public TMP_InputField playerName;

    byte maxPlayersPerRoom = 3;
    bool isConnecting;
    public TMP_Text networkText;
    string gameVersion = "2";

    private void Awake()
    {
        PhotonNetwork.AutomaticallySyncScene = true;
        if (!PlayerPrefs.HasKey("PlayerName"))
            return;
        playerName.text = PlayerPrefs.GetString("PlayerName");
    }

    public override void OnConnectedToMaster()
    {
        if(isConnecting)
        {
            networkText.text += "OnConnectToMaster...\n";
            PhotonNetwork.JoinRandomRoom();
        }
    }

    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        networkText.text += "Failed to join random room.\n";
        PhotonNetwork.CreateRoom(null, new RoomOptions
        {
            MaxPlayers = this.maxPlayersPerRoom
        });
    }

    public override void OnJoinedRoom()
    {
        networkText.text = "Joined Room with " + PhotonNetwork.CurrentRoom.PlayerCount + " players.\n";
        PhotonNetwork.LoadLevel(1);
    }

    public override void OnDisconnected(DisconnectCause cause)
    {
        networkText.text += "Disconnected because " + cause + "\n";
        isConnecting = false;
    }

    public void ConnectNetwork()
    {
        networkText.text = "";
        isConnecting = true;
        PhotonNetwork.NickName = playerName.text;
        if(PhotonNetwork.IsConnected)
        {
            networkText.text += "Joining Room ...\n";
            PhotonNetwork.JoinRandomRoom();
        }
        else
        {
            networkText.text += "Connecting ...\n";
            PhotonNetwork.GameVersion = gameVersion;
            PhotonNetwork.ConnectUsingSettings();
        }
    }

    public void SetPlayerName(string name)
    {
        PlayerPrefs.SetString("PlayerName", name);
    }

    public void StartTrial()
    {
        SceneManager.LoadScene(1);
    }
}
