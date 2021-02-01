using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class StartGame : MonoBehaviour, IPointerClickHandler
{
    public GameObject gameField;

    public GameController game;

    public GameObject[] menu;

    // Start is called before the first frame update
    void Start()
    {
        game = gameField.GetComponent<GameController>(); 
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        game.StartGame();

        foreach (GameObject element in menu)
        {
            element.SetActive(false);
        }
        PlayerPrefs.SetInt("Score", 0);
    }
}
