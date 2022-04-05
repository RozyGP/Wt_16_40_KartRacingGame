using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    DrivingScript drivingScript;

    float lastTimeMoving = 0f;
    CheckPointController checkPointController;

    private void Start()
    {
        drivingScript = GetComponent<DrivingScript>();
        checkPointController = drivingScript.rb.GetComponent<CheckPointController>();
    }

    private void ResetLayer()
    {
        drivingScript.rb.gameObject.layer = 0; //set default layer
    }

    private void Update()
    {
        if (checkPointController.lap == RaceController.totalLaps + 1)
            return;

        float acceleration = Input.GetAxis("Vertical");
        float steer = Input.GetAxis("Horizontal");
        float brake = Input.GetAxis("Jump");

        if (drivingScript.rb.velocity.magnitude > 1 || !RaceController.racing)
            lastTimeMoving = Time.time;

        if ((Time.time > lastTimeMoving + 4 
            || drivingScript.rb.transform.position.y < -5))
        {
            drivingScript.rb.transform.position =
                checkPointController.lastPoint.transform.position + Vector3.up * 2;
            drivingScript.rb.transform.rotation =
                Quaternion.LookRotation(checkPointController.lastPoint.transform.right);
                

            drivingScript.rb.gameObject.layer = 6; //UWAGA!

            Invoke("ResetLayer", 3);
        }

        if (!RaceController.racing) 
            acceleration = 0;
        drivingScript.Drive(acceleration, brake, steer);
        drivingScript.EngineSound();
    }
}
