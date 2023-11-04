using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System;
using TMPro;
public class RaceControl : MonoBehaviour {
    [SerializeField] private StartingManager startManager;
    [SerializeField] private LeaderboardManager leaderboard;
    [SerializeField] private List<CarData> cars = new List<CarData>();
    [SerializeField] private List<TMP_Text> positionText = new List<TMP_Text>();
    [SerializeField] private List<TMP_Text> nameText = new List<TMP_Text>();
    [SerializeField] private List<TMP_Text> timeText = new List<TMP_Text>();

    [SerializeField] private GameObject boardUi;
    public List<Transform> checkpoints = new List<Transform>();
    public int maxLaps = 3;
    public struct displayInfo{
        public string carName;
        public string position;
        public string time;
    }
    private List<displayInfo> carDisplay = new List<displayInfo>();
    // Ensures that the leaderboard cannot be updated more than once
    private bool ableToCalculate = true;

    // Sorts cars and then displays them on a leaderboard based on time
    public void CheckAllCarsFinished() {
        if(allCarsFinished()&&ableToCalculate == true){
            ableToCalculate = false;
            List<CarData> sortedCars = cars.OrderBy(x => x.finalTime).ToList();
            for(int i = 0; i < sortedCars.Count; i++){
                TimeSpan timer = TimeSpan.FromSeconds(sortedCars[i].finalTime);
                
                displayInfo carDisplayInfo;
                carDisplayInfo.carName = sortedCars[i].carName;
                carDisplayInfo.position = (i+1).ToString();
                carDisplayInfo.time = timer.Minutes.ToString()+":"+timer.Seconds.ToString()+"."+timer.Milliseconds.ToString();
                carDisplay.Add(carDisplayInfo);
                assignToBoard();
                boardUi.SetActive(true);
    
            }
        }
    }
    // Checks all cars have finished the race (called in the lap checkpoint)
    private bool allCarsFinished(){
        foreach(CarData car in cars){
            if(!car.isFinished){
                return false;
            }
        }
        return true;
    }
    // After finishing each car is assigned a position on the board based on the final time (more accurate than checkpoints)
    private void assignToBoard(){
        for(int i = 0; i < carDisplay.Count; i++){
            positionText[i].text = carDisplay[i].position;
            nameText[i].text = carDisplay[i].carName;
            timeText[i].text = carDisplay[i].time;
        }
    }
}