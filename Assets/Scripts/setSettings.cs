using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Settings : MonoBehaviour
{

    [SerializeField] Slider slider;
    public float AISpeed;
    public float sliderValue = 0;
    public String difficulty;
    private float easySpeed = 3;
    private float mediumSpeed = 5;
    private float hardSpeed = 6.5f;
    private float expertSpeed = 8;
    


    public void setDifficulty()
    {
        sliderValue = slider.value;
        if (sliderValue == 0)
        {
            AISpeed = easySpeed;
            difficulty = "E";
        }
        else if (sliderValue == 1)
        {
            AISpeed = mediumSpeed;
            difficulty = "M";
        }
        else if (sliderValue == 2)
        {
            AISpeed = hardSpeed;
            difficulty = "H";
        }
        else
        {
            AISpeed = expertSpeed;
            difficulty = "Ex";
        }
        PlayerPrefs.SetFloat("AISpeed", AISpeed);
        PlayerPrefs.SetString("difficulty", difficulty);
        PlayerPrefs.Save();
        
    }
}
