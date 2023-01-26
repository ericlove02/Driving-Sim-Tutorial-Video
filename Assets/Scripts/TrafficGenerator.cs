using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrafficGenerator : MonoBehaviour
{
    public GameObject trafficManager; // prolly not used but good to keep track
    public bool trafficIsRunning; // turn on and off traffic between waypoint when car hits start and end waypoints
    public GameObject obstaclePrefab;

    public float trafficDensity = 10f;
    public GameObject startWaypoint;
    public GameObject carWaypoint;
    public GameObject endWaypoint;
    private bool carsIn;
    public bool isBackwards;

    // Update is called once per frame
    void Update()
    {
        if (trafficIsRunning && !carsIn)
        {
            float density = Random.Range(2, trafficDensity);
            for (int i = 0; i < density; i++)
            {
                Vector3 vecBetweenWaypoints = endWaypoint.transform.position - carWaypoint.transform.position;
                Vector3 randPos = carWaypoint.transform.position + Random.Range(0, 100) / 2 * .01f * vecBetweenWaypoints;
                Debug.Log(randPos);
                GameObject newcar = Instantiate(obstaclePrefab, randPos, Quaternion.identity);
                if (isBackwards)
                {
                    newcar.transform.Rotate(0, 180, 0);
                }
                newcar.transform.Translate(Random.Range(0, 5), 0, 0);
            }
            carsIn = true;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.gameObject.name);
        if (other.gameObject == startWaypoint)
        {
            trafficIsRunning = true;
        }
        else if (other.gameObject == endWaypoint)
        {
            trafficIsRunning = false;
        }
        else if (other.gameObject == carWaypoint)
        {
            trafficIsRunning = false;
            carsIn = false;
        }
    }
}
