using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class CarController : MonoBehaviour
{
    [System.Serializable]
    public class wheel{
        public WheelCollider rightTyre;
        public WheelCollider leftTyre;
        public bool motor;
        public bool turnable;
    }

    [SerializeField] private TMP_Text stateText;
    [SerializeField] private float maxAccelTorqueN = 600f;
    [SerializeField] private float maxBrakeTorqueN = 600f;
    [SerializeField] private float maxAngle = 25f;
    [SerializeField] private List<wheel> axles = new List<wheel>();


    [SerializeField] private TMP_Text positionText;
    [SerializeField] private TMP_Text lapInfo;
    [SerializeField] private LeaderboardManager leaderboard;
    [SerializeField] private RaceControl raceControl;
    [SerializeField] private CarData carData;
    [SerializeField] private GameObject endCam;
    [SerializeField] private GameObject carCam;
    [SerializeField] private Transform player;
    private bool isReversed = false;
    // Applies the rotation from the wheel into the cars wheels
    public void turnCar(float wheelAngle){
        float angleToTurn = wheelAngle*maxAngle;
        foreach(wheel axle in axles){
            if(axle.turnable){
                axle.leftTyre.steerAngle = angleToTurn;
                axle.rightTyre.steerAngle = angleToTurn;
            }
        }
    }

    // Takes the acceleration paddle is mapped from 0-1 so the acceleration is % of max torque
    public void accelerating(float pedalAmount){
        if(Input.GetButtonDown("XRI_Right_PrimaryButton")){
            isReversed = !isReversed;
        }
        float torqueProduced = maxAccelTorqueN*pedalAmount;
        if(isReversed){
            torqueProduced *= -1f;
            stateText.text = "R";
        }
        else{
            stateText.text = "F";
        }
        foreach(wheel axle in axles){
            if(axle.motor){
                axle.rightTyre.motorTorque = torqueProduced;
                axle.leftTyre.motorTorque = torqueProduced;

            }
        }
    }
    // Brake (decelarate) paddle is expressed between 0 and 1 so braking is % of max brake force
    public void braking(float brakePedal){
        float brakeAmount = maxBrakeTorqueN*brakePedal;
        foreach(wheel axle in axles){
            axle.rightTyre.brakeTorque = brakeAmount;
            axle.leftTyre.brakeTorque = brakeAmount;

        }
    }
    private void Start() {
        endCam.SetActive(false);
        carCam.SetActive(true);
    }

    private void Update() {
        // Updates the position and lap the player car is on
        positionText.text = leaderboard.leaderboardPosition(carData.carName).ToString() + "/" + leaderboard.carCp.Count.ToString();
        lapInfo.text = "Lap:\n" + carData.numLaps.ToString() + "/" + raceControl.maxLaps.ToString();
        if(carData.isFinished == true){
            endCam.SetActive(true);
            carCam.SetActive(false);
        }
        // Unrolls car if it is rolled over
        if(Input.GetButtonDown("XRI_Left_PrimaryButton")||Input.GetButtonDown("Jump")){
            player.position = carData.lastCp.position;
            player.rotation = Quaternion.Euler(carData.lastCp.eulerAngles.x,player.eulerAngles.y,carData.lastCp.eulerAngles.z);
        }

    }

}
