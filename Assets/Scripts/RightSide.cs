using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RightSide : MonoBehaviour
{
    [SerializeField] private WheelController wheel;
    // Checks that the right side of the wheel is being grabbed by the right hand
    private void OnTriggerStay(Collider hand) {
        if(hand.gameObject.TryGetComponent(out RHand rightHand)){
            if(Input.GetAxis("XRI_Right_Grip")>wheel.deadZone){
                wheel.rightHandGrabbed = true;
            }
            else{
                wheel.rightHandGrabbed = false;
            }
        }
    }
    private void OnTriggerExit(Collider hand) {
        if(hand.gameObject.TryGetComponent(out RHand rightHand)){
            wheel.rightHandGrabbed = false;
        }
        
    }
}
