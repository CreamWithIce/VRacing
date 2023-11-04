using UnityEngine;
using System.Collections.Generic;
public class DisappearCar : MonoBehaviour {
    [SerializeField] private CarData carData;
    // Checks priority of the other car and determines which car should disappear 
    // Used to ensure cars do not clip into eachother
    private void OnTriggerStay(Collider other) {
        if(other.gameObject.TryGetComponent(out CarData otherCarData)){
            if(otherCarData.priority < carData.priority){
                otherCarData.chassis.enabled = false;
            }
            else{
                carData.chassis.enabled = false;
            }
        }
    }
    private void OnTriggerExit(Collider other) {
        if(other.gameObject.TryGetComponent(out CarData otherCarData)){
            if(otherCarData.chassis.enabled == false || carData.chassis.enabled == false){
                if(otherCarData.priority < carData.priority){
                    otherCarData.chassis.enabled = true;
                }
                else{
                    carData.chassis.enabled = true;
                }
            }
        }
    }
}