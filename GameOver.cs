﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    public Text gameOverText;
    public Text dayText;

    void Start()
    {
        dayText.text = "You survived for " + Day.day + " days.";
    }
    /*
    public void showDay() {

    }
    */
}