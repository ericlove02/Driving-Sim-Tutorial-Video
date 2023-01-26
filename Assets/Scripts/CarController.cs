using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CarController : MonoBehaviour
{

    public float maxSpeed = 90.0f;
    public float minSpeed = -10.0f;
    public float currSpeed = 0f;
    public float rotationSpeed = 80.0f;

    public AudioSource horn;

    public TextMeshProUGUI speedUI;

    public GameObject exclaimationPoint;

    public AudioClip[] passengerSounds;
    public AudioSource passengerAudio;

    public AudioClip[] carSounds; // 0 - accel; 1 - brake; 2 - idle
    public AudioSource carAudio;

    public GameObject endComponents;
    public GameObject endGameWaypoint;
    private bool inEndGame = false;
    private bool startedCoroutine = false;

    // Update is called once per frame
    void Update()
    {
        if (inEndGame)
        {
            endComponents.SetActive(true);
            maxSpeed = 1;
            minSpeed = -1;
            if (!startedCoroutine)
            {
                StartCoroutine(EndGameCoroutine());
                currSpeed = 0;
                startedCoroutine = true;
            }
        }
        else
        {
            endComponents.SetActive(false);
        }

        float vertInput = Input.GetAxis("Vertical");
        float horInput = Input.GetAxis("Horizontal");

        if (Input.GetKeyDown(KeyCode.E))
        {
            horn.Play();
        }

        // accelerating
        if (vertInput > 0 && currSpeed < maxSpeed)
        {
            currSpeed += vertInput * .1f;
            carAudio.clip = carSounds[0];
        }
        // braking
        if (vertInput < 0 && currSpeed >= minSpeed)
        {
            currSpeed += vertInput * .3f;
            carAudio.clip = carSounds[1];
        }
        // no vertical input
        if (vertInput == 0)
        {
            carAudio.clip = carSounds[2];
        }
        if (!carAudio.isPlaying)
        {
            carAudio.Play();
        }

        // friction when going forward
        if (currSpeed > 0)
        {
            currSpeed -= .01f;
        }
        //friction when going backward
        if (currSpeed < 0)
        {
            currSpeed += .01f;
        }

        // passenger excalimation point when at max speed
        if (currSpeed >= maxSpeed || inEndGame)
        {
            exclaimationPoint.SetActive(true);
        }
        else
        {
            exclaimationPoint.SetActive(false);
        }
        // passenger random audio when at max speed
        if (currSpeed >= maxSpeed && !passengerAudio.isPlaying && !inEndGame)
        {
            int rand = Random.Range(0, passengerSounds.Length);
            passengerAudio.clip = passengerSounds[rand];
            passengerAudio.Play();
        }

        // setting speed ui to current speed
        speedUI.text = currSpeed.ToString("0.00") + " mph";

        transform.position += transform.forward * currSpeed * Time.deltaTime;
        transform.Rotate(Vector3.up, rotationSpeed * horInput * Time.deltaTime);
    }

    void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.gameObject.name);
        if (other.gameObject == endGameWaypoint)
        {
            inEndGame = true;
        }
    }

    IEnumerator EndGameCoroutine()
    {
        //Print the time of when the function is first called.
        Debug.Log("Started Coroutine at timestamp : " + Time.time);

        //yield on a new YieldInstruction that waits for 5 seconds.
        yield return new WaitForSeconds(12);

        //After we have waited 5 seconds print the time again.
        Debug.Log("Finished Coroutine at timestamp : " + Time.time);
        SceneManager.LoadScene(0);

    }
}
