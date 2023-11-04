using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
public class RecordSystem : MonoBehaviour
{
    // Used to record the AI cars
    public carInputs inputToCar;
    [SerializeField] private string savePath = @".\Assets\AI_File";
    [SerializeField] private string saveName = "car1.dat";
    private List<string> data = new List<string>();
    [SerializeField] private Transform car;
    public bool currentlyRecording = false;
    [SerializeField] private StartingManager startManager;

    // Makes car move and then saves the transforms at each 0.02 seconds
    private void FixedUpdate() {
        if(currentlyRecording && startManager.raceStarted){
            float forward = Input.GetAxis("Vertical");
            float steer = Input.GetAxis("Horizontal");
            float brakes = Input.GetAxis("Jump");

            inputToCar.turnCar(steer);
            inputToCar.accelerateCar(forward*-1);
            inputToCar.brakeCar(brakes);
            addTransforms();
        }
        else{
            inputToCar.brakeCar(1f);
            car.rotation = Quaternion.Euler(0f,-90f,0f);
        }
    }
    public void Save(){
        currentlyRecording = false;
        using (StreamWriter sw = File.CreateText((savePath+saveName))){
            foreach(string dataline in data){
                sw.WriteLine(dataline);
            }
        }
    }
    // Position is stored first then rotation is stored
    void addTransforms(){
        string carPos = car.position.ToString().Trim(new char[]{'(',')'});
        string carRot = car.eulerAngles.ToString().Trim(new char[]{'(',')'});
        data.Add(carPos+","+carRot);
    }
}

    
