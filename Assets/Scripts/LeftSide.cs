using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeftSide : MonoBehaviour
{
    [SerializeField] private WheelController wheel;
    // Checks that the left hand side of the wheel is being grabbed by the left hand
    private void OnTriggerStay(Collider hand) {
        if(hand.gameObject.TryGetComponent(out LHand leftHand)){
            if(Input.GetAxis("XRI_Left_Grip")>wheel.deadZone){
                wheel.leftHandGrabbed = true;
            }
            else{
                wheel.leftHandGrabbed = false;
            }
        }
    }
    private void OnTriggerExit(Collider hand) {
        if(hand.gameObject.TryGetComponent(out LHand leftHand)){
            wheel.leftHandGrabbed = false;
        }
    }
}
