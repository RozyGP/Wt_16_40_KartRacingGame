using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RaceController : MonoBehaviour
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
        endPanel.SetActive(false);

        audioSource = GetComponent<AudioSource>();
        startText.gameObject.SetActive(false);

        InvokeRepeating("CountDown", 3, 1);

        for(int i=0;i<playerCount;i++)
        {
            GameObject car = Instantiate(carPrefab);
            car.transform.position = spawnPoints[i].position;
            car.transform.rotation = Quaternion.LookRotation(spawnPoints[i].right);
            car.GetComponent<CarApperance>().playerNumber = i;

            if (i == 0)
            {
                car.GetComponent<PlayerController>().enabled = true;
                GameObject.FindObjectOfType<CameraController>().SetCameraProperties(car);
            }
        }

        GameObject[] cars = GameObject.FindGameObjectsWithTag("Car");
        carsController = new CheckPointController[cars.Length];
        for(int i=0;i<cars.Length;i++)
        {
            carsController[i] = cars[i].GetComponent<CheckPointController>();
        }
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
