using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    //Score of the player
    private int score;

    //Number of Objects in Game
    public int numberOfObjects; 
    
    //Container of the score text
    public GameObject scoreContainer;

    //Text to display the score
    public Text scoreText;

    //Difficulty stage the player has reached (1-5)
    private int difficulty;

    public float[] weights; 

    //list of the possible objects 
    public GameObject[] objects;

    public ArrayList spawnedObjects;

    public int[] objectsIndex;

    public GameObject[] ui;

    public GameObject ScoreBoard;

    public GameObject leftSpawnPoint;

    public GameObject rightSpawnPoint;

    //The Player 
    public GameObject player;

    //Script of the player
    private PlayerController playerScript;

    //Button to activate Plus 
    public GameObject plus;

    //Script of plus button 
    private ButtonController plusScript;

    //Button to activate Minus 
    public GameObject minus;

    //Script of minus button 
    private ButtonController minusScript;

    //Button to spin
    public GameObject spin;

    //Script of spin button 
    private ButtonController spinScript;

    public GameObject settings;
  
    public GameObject quit;

    public GameObject scoreboard_bottom;

    public GameObject scoreboard_score;

    public float speed;

    public GameObject restart;

    public GameObject[] buttons;

    public float time =-1f;

    private void Awake()
    {
        Application.targetFrameRate = 60;
    }
    // Start is called before the first frame update
    void Start()
    {
        //Initializing all important variables 
        score = 0;
        difficulty = 1;

        speed = 5f; 
        
        playerScript = player.GetComponent<PlayerController>();

        plusScript = plus.GetComponent<ButtonController>();

        minusScript = minus.GetComponent<ButtonController>();

        spinScript = spin.GetComponent<ButtonController>();

        scoreText = scoreContainer.GetComponent<Text>();

        spawnedObjects = new ArrayList();

        foreach (GameObject element in ui)
        {
            element.SetActive(false);
        }

        if (!PlayerPrefs.HasKey("Volume"))
        {
            PlayerPrefs.SetFloat("Volume", 0.5f);
        }

        if (!PlayerPrefs.HasKey("Music"))
        {
            PlayerPrefs.SetInt("Music", 1);
        }

        ScoreBoard.SetActive(false);

        restart.SetActive(false);

        numberOfObjects = 0; 

        objectsIndex = GenerateList();

        UnityEngine.Random.InitState((int)System.DateTime.Now.Ticks);

        Debug.Log((int)System.DateTime.Now.Ticks);
    }

    void Update()
    {
        playerChangeState();
        scoreText.text = PlayerPrefs.GetInt("Score").ToString();

        if (time >= 0f) time += Time.deltaTime;
    }


    public void StartGame()
    {
        plusScript.isPressed = false;
        minusScript.isPressed = false;
        score = 0;
        speed = 5f;
        Debug.Log(PlayerPrefs.GetInt("Score").ToString());
        numberOfObjects = 0;
        SpawnRandomObject();
        player.SetActive(true);
        ScoreBoard.SetActive(false);
        restart.SetActive(false);
        quit.SetActive(false);
        playerScript.setPlayerPol(PlayerController.pol.Neutral);

        foreach (GameObject element in ui)
        {
            element.SetActive(true);
        }

        foreach (GameObject button in buttons)
        {
            Button button_component = button.GetComponent<Button>();
            button_component.interactable = true;
        }
    }

    public void EndGame()
    {
        player.SetActive(false);

        foreach (GameObject element in ui)
        {
            element.SetActive(false);
        }

        foreach(GameObject element in spawnedObjects)
        {
            Destroy(element);
        }

        plusScript.isPressed = false;
        minusScript.isPressed = false;

        spawnedObjects.Clear();
        ScoreBoard.SetActive(true);
        restart.SetActive(true);
        quit.SetActive(true);
        settings.SetActive(true);
        Text scoreboard_bottom_text = scoreboard_bottom.GetComponent<Text>();

        if (PlayerPrefs.GetInt("Score") > PlayerPrefs.GetInt("Personal Best"))
        {
            scoreboard_bottom_text.text = "Congratulations!\n that's a new highscore";
            scoreboard_bottom_text.color = Color.green;
            PlayerPrefs.SetInt("Personal Best", PlayerPrefs.GetInt("Score"));
        }
        else if (PlayerPrefs.GetInt("Score") >= PlayerPrefs.GetInt("Personal Best") - 5)
        {
            scoreboard_bottom_text.text = "Close! \n Only " + (PlayerPrefs.GetInt("Personal Best") - PlayerPrefs.GetInt("Score") + 1 ).ToString() + " more points \n for a new high score";
            scoreboard_bottom_text.color = Color.yellow;
        }
        else if (PlayerPrefs.GetInt("Score") < PlayerPrefs.GetInt("Personal Best"))
        {
            scoreboard_bottom_text.text = "Shame! \n You need " + (PlayerPrefs.GetInt("Personal Best") - PlayerPrefs.GetInt("Score") + 1).ToString() + " more points \n for a new high score";
            scoreboard_bottom_text.color = Color.red;
        }

        scoreboard_score.GetComponent<Text>().text = PlayerPrefs.GetInt("Score").ToString();
    }
    /**
     * Spawns an object, every object has a number associated with it 
     * 
     * @param obj The Number of the object to be spawned
     * @param direction Determines the direction the object should move in
     * @param side Determines the side of the player the object should be spawned of
     * 
     */
    public void SpawnObject(int obj,int direction,GameObject side)
    {
        

        if (obj > 0) {
            GameObject temp = Instantiate(objects[obj], side.transform.position,side.transform.rotation);
            temp.GetComponent<ObjectController>().setDirection(direction);
            temp.GetComponent<ObjectController>().SetSpeed(speed);
            AddObjectTolist(temp);
        }
        
    }

    public void SpawnRandomObject()
    {
        if(numberOfObjects == 0)
        {
            spawnedObjects.Clear();
            int object1 = WeightedRandom(objectsIndex, weights);
            int object2 = WeightedRandom(objectsIndex, weights);

            if (object1 != 0) numberOfObjects++;
            if (object2 != 0) numberOfObjects++;

            if (numberOfObjects == 0)
            {
                SpawnRandomObject();
            }
            else
            {
                SpawnObject(object1, 1, leftSpawnPoint.gameObject);
                SpawnObject(object2, -1, rightSpawnPoint.gameObject);
                speed = Mathf.Clamp(speed + 0.05f, 3f, 5f);
            }

            time = 0f;
        }

    }

    public void AddObjectTolist(GameObject newObject)
    {
        spawnedObjects.Add(newObject);
    }

    public void DeleteFromList()
    {
        spawnedObjects.Clear();
    }

    /**
     * Changes the state of the player character, if the neither of the buttons is pressed the character will return to a neutral state 
     * where the state of the poles corresponds with their color
     */
    void playerChangeState()
    {
        if (plusScript.getIsPressed())
        {
            playerScript.setPlayerPol(PlayerController.pol.Plus);
        }
        else if (minusScript.getIsPressed())
        {
            playerScript.setPlayerPol(PlayerController.pol.Minus);
        }
        else
        {
            playerScript.setPlayerPol(PlayerController.pol.Neutral);
        }

    }

    /**
     *  Increases the score of the game
     */
    public void IncreaseScore()
    {
        score++;
        Debug.Log("time:"+time.ToString());
    }


    public void DecreaseNumberOfObjects()
    {
        numberOfObjects--;
    }
    /**
     * Decides a random number from from a collection of numbers based on a given weight collection
     *
     * @param numbers The Collection of numbers 
     * @param weights The Collection of weights
     * 
     * @retuns A random number from the collection
     *
     */
    public int WeightedRandom(int[] numbers, float[] weights)
    {
        ArrayList numbers_with_weights = new ArrayList();



        if (numbers.Length != weights.Length && weights.Sum() != 1.0f)
        {
            return -1;
        }

        for (int i = 0; i < numbers.Length; i++)
        {
            int j = (int)Math.Floor(weights[i] * 100);

            for (int k = 0; k < j; k++)
            {
                numbers_with_weights.Add(numbers[i]);
            }

        }

        int temp = (int)numbers_with_weights[UnityEngine.Random.Range(0, numbers_with_weights.Count)];
        Debug.Log(temp);
        return temp;
    }

    private int[] GenerateList()
    {
        int[] list = new int[objects.Length];

        for(int i = 0; i < list.Length; i++)
        {
            list[i] = i;
        }

        return list; 
    }

    public int GetScore()
    {
        return score;
    }

   


}

   