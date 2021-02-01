using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOverScreen : MonoBehaviour
{
    public Text endOfGame;
    private bool displayed = false;
    
    // Update is called once per frame
    void Update()
    {
        if (!displayed)
        {
            if (PlayerPrefs.GetInt("Score") > PlayerPrefs.GetInt("Personal Best") )
            {
                endOfGame.text = "Your new Personal Best is: " + PlayerPrefs.GetInt("Score");
                PlayerPrefs.SetInt("Personal Best", PlayerPrefs.GetInt("Score"));
            }
            else
            {
                endOfGame.text = "Your final score is: " + PlayerPrefs.GetInt("Score");
            }
            displayed = true;
        }
    }
}
