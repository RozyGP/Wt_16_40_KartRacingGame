using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddFuel : MonoBehaviour
{
    public DrivingScript drivingScript;

    public void Add()
    {
        if(drivingScript.enabled)
        {
            drivingScript.nitroFuel += 1;
        }
    }
}
