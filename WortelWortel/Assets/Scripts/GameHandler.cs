using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameHandler : MonoBehaviour
{
    int score = 0;
    int nextLeveLScore = 8;
    public GameObject enemy;
    TextMeshProUGUI txtScore;

    void Start()
    {
        txtScore = GameObject.Find("txtScore").GetComponent<TextMeshProUGUI>();
    }
    public void setScore(int score)
    {
        this.score = score;

    }

    public int getScore()
    {
        return score;
    }

    public void AddScore(int scoreToAdd)
    {
        score += scoreToAdd;
        Debug.Log("Total Score: " + score);
        txtScore.text = "Points: " + score;
        if (score > nextLeveLScore)
        {
            EnemySpawner();
            nextLeveLScore = (nextLeveLScore + 10) * 2;
        }
    }

    void EnemySpawner()
    {
        Debug.Log("INSTANTIATE");
        Instantiate(enemy, new Vector3(0, -20, 0), Quaternion.identity);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
