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

    private void Start()
    {
        if(playerNumber == 0)
        {
            playerName = PlayerPrefs.GetString("PlayerName");
            carColor = ColorCar.IntToColor(
                PlayerPrefs.GetInt("Red"),
                PlayerPrefs.GetInt("Green"),
                PlayerPrefs.GetInt("Blue")
                );
        }
        else
        {
            playerName = "Random " + playerNumber;
            carColor = new Color(
                Random.Range(0f, 1f), 
                Random.Range(0f, 1f), 
                Random.Range(0f, 1f)
                );
        }

        nameText.text = playerName;
        carRenderer.material.color = carColor;
        nameText.color = carColor;
    }
}
