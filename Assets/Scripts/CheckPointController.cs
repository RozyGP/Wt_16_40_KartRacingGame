using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPointController : MonoBehaviour
{
    public int lap = 0;
    public int checkPoint = -1;
    int pointCount;
    public int nextPoint;

    private void Start()
    {
        GameObject[] checkpoints = GameObject.FindGameObjectsWithTag("CheckPoint");
        pointCount = checkpoints.Length;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "CheckPoint")
        {
            int thisPoint = int.Parse(other.gameObject.name);
            if(thisPoint == nextPoint)
            {
                checkPoint = thisPoint;
                if(checkPoint == 0)
                {
                    lap++;
                    Debug.Log("Lap: " + lap);
                }

                nextPoint++;
                nextPoint = nextPoint % pointCount;
            }
        }
    }
}