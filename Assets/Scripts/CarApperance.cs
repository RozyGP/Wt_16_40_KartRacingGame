using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CarApperance : MonoBehaviour
{
    public string playerName;
    public Color carColor;
    public TextMeshProUGUI nameText;
    public Renderer carRenderer;

    public int playerNumber;

    public Camera rearCamera;

    int carRego;
    bool regoSet = false;
    public CheckPointController checkPointController;

    private void LateUpdate()
    {
        if(!regoSet)
        {
            carRego = Leaderboard.RegisterCar(playerName);
            regoSet = true;
            return;
        }

        Leaderboard.SetPosition(carRego,
            checkPointController.lap,
            checkPointController.checkPoint);
    }

    public void SetNameAndColor(string name, Color color)
    {
        playerName = name;
        nameText.text = name;
        carRenderer.material.color = color;
        nameText.color = color;
    }

    public void SetLocalPlayer()
    {
        FindObjectOfType<CameraController>().SetCameraProperties(this.gameObject);
        playerName = PlayerPrefs.GetString("PlayerName");
        carColor = ColorCar.IntToColor(PlayerPrefs.GetInt("Red"),
            PlayerPrefs.GetInt("Green"),
            PlayerPrefs.GetInt("Blue"));
        nameText.text = playerName;
        carRenderer.material.color = carColor;
        nameText.color = carColor;
        RenderTexture renderTexture = new RenderTexture(1024, 1024, 0);
        rearCamera.targetTexture = renderTexture;
        FindObjectOfType<RaceController>().SetMirror(rearCamera);
    }
}
