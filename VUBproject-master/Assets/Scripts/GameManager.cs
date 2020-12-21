using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class GameManager : MonoBehaviour
{
    bool mutated;
    public bool GameOver = false;
    public Text text;
    public Text endScore;
    int totalmutations = 3;
    public MutationBar mutationBar;
    public GameManager instance;
    int mutations;
    int currentPuntMutation;
    int points;

    public GameObject leftController;
    public GameObject rightController;

    public GameObject spots;
    public GameObject scoreboard;
    public GameObject endscoreUI;

    float timeRemaining = 90;
    public bool timerIsRunning = false;
    float minutes;
    float seconds;
    public Text TimerText;
    public GameObject timercanvas;
    public int AminoZuurAmount;
    public Text aminozuuramount;
    public GameObject pointer;



    void Start()
    {

        instance = this;
        endScore.text = "0";
        timerIsRunning = true;
    }
    void Update()
    {
        if (timerIsRunning)
        {
            if (timeRemaining > 0)
            {
                timeRemaining -= Time.deltaTime;
                DisplayTime(timeRemaining);
            }
            else
            {
                Debug.Log("Time has run out!");
                timeRemaining = 0;
                timerIsRunning = false;
                gameover();
            }
        }

    }

    void DisplayTime(float timeToDisplay)
    {
        minutes = Mathf.FloorToInt(timeToDisplay / 60);
        seconds = Mathf.FloorToInt(timeToDisplay % 60);
        TimerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }


    public void setpoints(int points)
    {
        aminozuuramount.text = AminoZuurAmount.ToString() + " / 10";
        this.points += points;
        text.text = "Score : " + this.points;
        endScore.text = this.points.ToString();


    }

    public void setPointMutationPoints()
    {
        if (!mutated)
        {
            this.currentPuntMutation++;
            mutationBar.updateslider(5);
            if (this.currentPuntMutation >= 3)
            {

                this.currentPuntMutation = 0;
                mutate();
            }
        }

    }

    public void mutate()
    {
        mutated = true;
        mutations++;
        pickRandomMutation();
        if (mutations >= 5)
        {
            gameover();
        }

    }

    void gameover()
    {
        GameOver = true;
        //score canvas weghalen
        scoreboard.SetActive(false);
        timercanvas.SetActive(false);
        endscoreUI.SetActive(true);
        pointer.SetActive(true);
        //game over canvas tonen
        
        //startscreen en score showen.
        
    }

    public void stopmutation()
    {
        Time.timeScale = 1;
        spots.SetActive(false);
        leftController.GetComponent<Weapon>().StopVibrate();
        rightController.GetComponent<Weapon>().StopVibrate();
        mutated = false;
    }

    void pickRandomMutation()
    {
        int randomNumber = Random.Range(0, totalmutations);
        if (randomNumber == 0)
        {
            //eerste mutatie
            Debug.Log("Mutatie 1");
            spots.SetActive(true);
        }
        else if (randomNumber == 1)
        {
            Debug.Log("Mutatie 2");
            //tweede mutatie

            Time.timeScale = Time.timeScale * 3;
        }
        else if (randomNumber == 2)
        {
            Debug.Log("Mutatie 3");
            //derde mutatie
            leftController.GetComponent<Weapon>().StartVibrate();
            rightController.GetComponent<Weapon>().StartVibrate();
        }

    }

    
}

