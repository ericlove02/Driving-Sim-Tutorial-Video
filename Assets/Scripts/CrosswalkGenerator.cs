using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrosswalkGenerator : MonoBehaviour
{
    public GameObject CrosswalkManager; // prolly not used but good to keep track
    public int numFamilies = 2;
    private bool crossWalkOn = false;
    public GameObject startWaypoint;
    public GameObject familyPrefab;

    public GameObject crosswalkTileBeg;
    public GameObject crosswalkTileEnd;

    // Update is called once per frame
    void Update()
    {
        if (crossWalkOn)
        {
            for (int i = 0; i < numFamilies; i++)
            {
                Vector3 vecBetweenTiles = crosswalkTileEnd.transform.position - crosswalkTileBeg.transform.position;
                Vector3 randPos = crosswalkTileBeg.transform.position + Random.Range(0, 100) * .01f * vecBetweenTiles;
                randPos.y += 2;
                Instantiate(familyPrefab, randPos, Quaternion.identity);
            }
            crossWalkOn = false;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.gameObject.name);
        if (other.gameObject == startWaypoint)
        {
            crossWalkOn = true;
        }

    }
}
