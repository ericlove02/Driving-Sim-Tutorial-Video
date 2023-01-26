using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrafficCar : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        transform.position += transform.forward * Time.deltaTime * 40f;
    }

    void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.gameObject.name);
        if (other.gameObject.name == "EndWaypoint")
        {
            Destroy(gameObject);
        }
    }
}
