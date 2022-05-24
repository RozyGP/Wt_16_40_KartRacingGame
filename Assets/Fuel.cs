using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fuel : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        AddFuel car = other.GetComponent<AddFuel>();

        if(car != null)
        {
            car.Add();
        }
    }
}
