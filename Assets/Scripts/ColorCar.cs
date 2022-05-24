using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ColorCar : MonoBehaviour
{
    public Renderer rend;

    public Slider redSlider;
    public Slider greenSlider;
    public Slider blueSlider;

    public TMP_Text redSliderText;
    public TMP_Text greenSliderText;
    public TMP_Text blueSliderText;

    public Color col;

    public static Color IntToColor(int red, int green, int blue)
    {
        float r = (float)red / 256;
        float g = (float)green / 256;
        float b = (float)blue / 256;

        return new Color(r, g, b);
    }

    void SetCarColor(int red, int green, int blue)
    {
        Color color = IntToColor(red, green, blue);
        rend.material.color = color;

        PlayerPrefs.SetInt("Red", red);
        PlayerPrefs.SetInt("Green", green);
        PlayerPrefs.SetInt("Blue", blue);
    }

    private void Start()
    {
        //Gdy bedzie klucz "Red", to beda tez rownoczesnie "Green" i "Blue".
        if (!PlayerPrefs.HasKey("Red"))
            return;

        int red = PlayerPrefs.GetInt("Red");
        int green = PlayerPrefs.GetInt("Green");
        int blue = PlayerPrefs.GetInt("Blue");

        col = IntToColor(red, green, blue);
        rend.material.color = col;

        redSlider.value = red;
        greenSlider.value = green;
        blueSlider.value = blue;
    }

    private void Update()
    {
        int red = (int)redSlider.value;
        int green = (int)greenSlider.value;
        int blue = (int)blueSlider.value;

        SetCarColor(red, green, blue);

        redSliderText.text = red.ToString();
        greenSliderText.text = green.ToString();
        blueSliderText.text = blue.ToString();
    }
}
