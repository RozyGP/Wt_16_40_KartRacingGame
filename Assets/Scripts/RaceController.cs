using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using Photon.Realtime;
using Photon.Pun;

public class RaceController : MonoBehaviourPunCallbacks
{
    public static bool racing = false;
    public static int totalLaps = 1;
    public int timer = 3;

    public CheckPointController[] carsController;

    public TextMeshProUGUI startText;
    AudioSource audioSource;
    public AudioClip count;
    public AudioClip start;

    public GameObject endPanel;

    public GameObject carPrefab;
    public Transform[] spawnPoints;
    public int playerCount;

    public GameObject startRaceButton;
    public GameObject waitingText;

    [PunRPC]
    public void StartGame()
    {
        InvokeRepeating("CountDown", 3, 1);
        startRaceButton.SetActive(false);
        waitingText.SetActive(false);

        GameObject[] cars = GameObject.FindGameObjectsWithTag("Car");
        carsController = new CheckPointController[cars.Length];
        for (int i = 0; i < cars.Length; i++)
        {
            carsController[i] = cars[i].GetComponent<CheckPointController>();
        }
    }

    public void BeginGame()
    {
        if(PhotonNetwork.IsMasterClient)
        {
            photonView.RPC("StartGame", RpcTarget.All, null);
        }
    }

    private void CountDown()
    {
        startText.gameObject.SetActive(true);
        if(timer != 0)
        {
            startText.text = timer.ToString();
            audioSource.PlayOneShot(count);
            timer--;
        }
        else
        {
            startText.text = "START!!!";
            audioSource.PlayOneShot(start);
            racing = true;
            CancelInvoke("CountDown");
            Invoke("HideStartText", 1);
        }
    }

    private void HideStartText()
    {
        startText.gameObject.SetActive(false);
    }

    private void Start()
    {
        playerCount = PhotonNetwork.CurrentRoom.PlayerCount;
        endPanel.SetActive(false);
        audioSource = GetComponent<AudioSource>();
        startText.gameObject.SetActive(false);

        startRaceButton.SetActive(false);
        waitingText.SetActive(false);

        Vector3 startPos;
        Quaternion startRot;
        GameObject playerCar = null;

        if(PhotonNetwork.IsConnected)
        {
            startPos = spawnPoints[playerCount - 1].position;
            startRot = Quaternion.LookRotation(spawnPoints[playerCount - 1].right);

            if(OnlinePlayer.LocalPlayerInstance == null)
            {
                playerCar = PhotonNetwork.Instantiate(carPrefab.name, startPos, startRot, 0);
            }

            if(PhotonNetwork.IsMasterClient)
            {
                startRaceButton.SetActive(true);
            }
            else
            {
                waitingText.SetActive(true);
            }
        }

        playerCar.GetComponent<PlayerController>().enabled = true;
        playerCar.GetComponent<DrivingScript>().enabled = true;
    }

    private void LateUpdate()
    {
        int finishedLap = 0;
        foreach(CheckPointController controller in carsController)
        {
            if (controller.lap == totalLaps + 1)
                finishedLap++;

            if(finishedLap == carsController.Length && racing)
            {
                endPanel.SetActive(true);
                racing = false;
            }
        }
    }

    public void LoadScene(int index)
    {
        SceneManager.LoadScene(index);
    }
}
