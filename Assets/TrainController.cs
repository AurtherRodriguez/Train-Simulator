using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class Wheel
{
    public WheelCollider LEFT;
    public WheelCollider RIGHT;
    public bool Accelerate;
    public bool Brake;
}


public class TrainController : MonoBehaviour
{
    public float maxSpeed = 50f;    // Maximum speed of the train
    public float acceleration = 20f;    // Acceleration of the train
    public float braking = 40f;     // Braking power of the train
    public float gravity = 20f;     // Gravity force applied to the train
    public float locomotiveMass = 100f;     // Mass of the locomotive
    public float carMass = 50f;     // Mass of each car

    private float throttleInput;    // Throttle input from the user
    private float brakeInput;   // Brake input from the user
    private float speed;    // Current speed of the train
    private Rigidbody[] trainParts;     // Array of rigidbodies for each train part
    public float wheelacce;
    public float wheelbrake;
    
    public List<Wheel> wheels;

    void Start()
    {
        trainParts = GetComponentsInChildren<Rigidbody>();

       foreach (Rigidbody rb in trainParts)
        {
            if (rb.CompareTag("Locomotive"))
            {
                rb.mass = locomotiveMass;
            }
            else
            {
                rb.mass = carMass;
            }
        }
    }

    void Update()
    {
        brakeInput = Input.GetAxis("Brake");
        
        if (brakeInput > 0)
        {
            brakeInput = Input.GetAxis("Brake") * braking;
        }
        else if (wheelacce <= 0 && brakeInput <= 0)
        {
            brakeInput = 20;
        }
        throttleInput = Input.GetAxis("Throttle") * acceleration;
        

        
        wheelacce = throttleInput;
        wheelbrake = brakeInput;
        foreach (Wheel wheel in wheels) 
        {
            if (wheel.Accelerate)
            {
                wheel.LEFT.motorTorque = wheelacce;
                wheel.RIGHT.motorTorque = wheelacce;
            }
            if (wheel.Brake)
            {
                wheel.LEFT.brakeTorque = wheelbrake;
                wheel.RIGHT.brakeTorque = wheelbrake;
            }
        }
    }
}