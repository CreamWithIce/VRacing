using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartingManager : MonoBehaviour
{
    public bool raceStarted = false;
    [SerializeField] private float countDownSeconds = 4f;

    [SerializeField] private Material countingMat;
    [SerializeField] private Material raceBeginMat;
    [SerializeField] private MeshRenderer[] lights = new MeshRenderer[3];

   
    void Start()
    {
        raceStarted = false;
    }

    // Updates the lights colour when the race is starting
    // Afterwards, hands responsibility off to race control
    void Update()
    {
        if(countDownSeconds >= 0f){
            countDownSeconds -= Time.deltaTime;
            if(countDownSeconds < 1f){
                lights[2].material = countingMat;
            }
            else if(countDownSeconds < 2f){
                lights[1].material = countingMat;
            }
            else if(countDownSeconds < 3f){
                lights[0].material = countingMat;
            }
        }
        else{
            raceStarted = true;
            foreach(MeshRenderer light in lights){
                light.material = raceBeginMat;
            }
        }
    }
}
