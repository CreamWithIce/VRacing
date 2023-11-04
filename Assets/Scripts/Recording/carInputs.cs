using UnityEngine;
using System.Collections.Generic;
using System.Collections;
// Used when recording to move the base car
public class carInputs : MonoBehaviour {
    [System.Serializable]
    public class wheel{
        public WheelCollider rightTyre;
        public WheelCollider leftTyre;
        public bool motor;
        public bool turnable;
    }

    [SerializeField] private List<wheel> axles = new List<wheel>();
    [SerializeField] private float maxAccelTorqueN = 600f;
    [SerializeField] private float maxBrakeTorqueN = 600f;
    [SerializeField] private float maxAngle = 25f;
    // Turns car based on right/left inputs given
    public void turnCar(float wheelAngle){
        float angleToTurn = wheelAngle*maxAngle;
        foreach(wheel axle in axles){
            if(axle.turnable){
                axle.leftTyre.steerAngle = angleToTurn;
                axle.rightTyre.steerAngle = angleToTurn;
            }
        }
        
    }

    // The pedal amount is between 0-1 from forward and back inputs, so it is % of max torque
    public void accelerateCar(float pedalAmount){
        float torqueProduced = maxAccelTorqueN*pedalAmount;
        foreach(wheel axle in axles){
            if(axle.motor){
                axle.rightTyre.motorTorque = torqueProduced;
                axle.leftTyre.motorTorque = torqueProduced;

            }
        }

    }
    // The brake pedal amount is between 0-1 from braking inputs, so it is % of max brake force
    public void brakeCar(float brakePedal){
        float brakeAmount = maxBrakeTorqueN*brakePedal;
        foreach(wheel axle in axles){
            axle.rightTyre.brakeTorque = brakeAmount;
            axle.leftTyre.brakeTorque = brakeAmount;
        }
    }
}