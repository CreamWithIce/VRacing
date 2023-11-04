using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using TMPro;
public class WheelController : MonoBehaviour
{
    [Range(0,180)]
    [SerializeField] private float angleClamp = 45f;
    [SerializeField] private Transform primaryHand;
    [SerializeField] private Transform secondaryHand;
    [SerializeField] private Transform wheel;
    [SerializeField] private Vector3 defaultLocalRotation;

    [SerializeField] private CarController controller;
    [SerializeField] private StartingManager startManager;

    [SerializeField] private TMP_Text positionText;
    [SerializeField] private LeaderboardManager leaderboard;
    [SerializeField] private CarData carData;
    public Vector3 handTransform;
    public Vector3 hand;
    public bool leftHandGrabbed = true;
    public bool rightHandGrabbed = true;
    public float deadZone = 0.2f;
    public float tanAngle;

    // Update is called once per frame
    void Update()
    {
        if(startManager.raceStarted == false){
            controller.braking(1f);
        }
        else{
            controller.accelerating(Input.GetAxis("XRI_Right_Trigger")*-1f);
            controller.braking(Input.GetAxis("XRI_Left_Trigger"));
        }

        // Gets position between the hands and wheel, and
        // normalizes that to calculate the angle between the hand and wheel using trig
        Vector3 relativePrim = primaryHand.InverseTransformPoint(wheel.position);
        Vector3 relativeSec = secondaryHand.InverseTransformPoint(wheel.position);
        handTransform = relativePrim;
        hand = primaryHand.position;
        
        float tanAnglePrim = (Mathf.Atan2(relativePrim.y, relativePrim.z)*Mathf.Rad2Deg);
        float tanAngleSec = (Mathf.Atan2(relativeSec.y, relativeSec.z)*Mathf.Rad2Deg)*-1f;
        
        // Averages the angle between the primary (right) hand and the wheel,
        // and secondary (left) hand and the wheel
        tanAngle = ((float)Math.Round(tanAnglePrim,2)+(float)Math.Round(tanAnglePrim,2))/2;

        // Makes sure the wheel cannot turn past a certain point
        tanAngle = Mathf.Clamp(tanAngle,angleClamp*-1,angleClamp);
        
        if(true == leftHandGrabbed && true == rightHandGrabbed){
            wheel.localRotation = Quaternion.Euler(defaultLocalRotation.x+tanAngle,defaultLocalRotation.y,defaultLocalRotation.z);
            controller.turnCar(tanAngle/angleClamp);
        }
    }
}
