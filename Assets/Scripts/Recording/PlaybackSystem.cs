using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class PlaybackSystem : MonoBehaviour
{
    [SerializeField] private string fileName = "car1.dat";
    private string[] transformData;
    [SerializeField] private Transform car;
    [SerializeField] private StartingManager startManager;
    public bool playFileBack = true;

    private int index = 0;
    // Inefficient memory usage reading the file into memory at the beginning of the script
    private void Start() {
        TextAsset fileRead = (TextAsset)Resources.Load(fileName);
        transformData = fileRead.text.Split("\n");
    }
    // Applies the cars transforms from the next line of a file
    private void FixedUpdate() {
        if(startManager.raceStarted == true && index < transformData.Length-1){
            // First 3 indexes of the line are the X Y Z of the next position car should move to
            string[] vectors = transformData[index].Split(",");
            float posX = convertToFloat(vectors[0]);
            float posY = convertToFloat(vectors[1]);
            float posZ = convertToFloat(vectors[2]);
            Vector3 positionData = new Vector3(posX,posY,posZ);
            // Last 3 indexes are the X Y Z of the next rotation
            float rotX = convertToFloat(vectors[3]);
            float rotY = convertToFloat(vectors[4]);
            float rotZ = convertToFloat(vectors[5]);
            Vector3 rotationData = new Vector3(rotX,rotY,rotZ);
            car.position = positionData;
            car.rotation = Quaternion.Euler(rotationData);
            
            index++;
        }
        
    }
    float convertToFloat(string toConvert){
        return (float)Convert.ToDouble(toConvert);
    }
    
}
