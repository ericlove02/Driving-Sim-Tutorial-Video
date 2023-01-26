using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Scoring : MonoBehaviour
{
    public int score = 0;
    public TextMeshProUGUI scoreUI;
    public TextMeshProUGUI endScore;
    public GameObject endGameWaypoint;

    void OnCollisionEnter(Collision collision)
    {
        Debug.Log(collision.gameObject.name);

        if (collision.gameObject.name == "Family(Clone)" ||
        collision.gameObject.name == "Car_1(Clone)")
        {
            score++;
            scoreUI.text = score.ToString() + " objects hit!";
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == endGameWaypoint)
        {
            scoreUI.gameObject.SetActive(false);
            endScore.text = "You hit " + score.ToString() + " obstacles during your drive today! Did you know that 40% of drunk driving accident end in fatalities? Please do not drink and drive. From your friends at DrunkSim, thank you for playing!";
            endScore.gameObject.SetActive(true);
        }
    }
}
