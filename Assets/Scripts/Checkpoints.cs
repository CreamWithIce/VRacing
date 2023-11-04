using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoints : MonoBehaviour
{
    [SerializeField] private bool isLapCheckpoint = false;
    [SerializeField] private RaceControl raceController;
    [SerializeField] private LeaderboardManager leaderboard;
    private void OnTriggerEnter(Collider car) {
        if(car.gameObject.TryGetComponent<CarData>(out CarData carInfo)){

            // Checks if car still has laps left and checks it is going through the correct checkpoint then updates the leaderboard
            if(carInfo.numLaps<=raceController.maxLaps){

                if(transform==raceController.checkpoints[carInfo.index]){
                    if(isLapCheckpoint == true){
                        carInfo.numLaps++;
                        // Needs to check if you have completed the laps after updating number of laps
                        if(carInfo.numLaps>raceController.maxLaps&&carInfo.isFinished == false){
                            carInfo.finalTime = Time.time-carInfo.startTime;
                            carInfo.isFinished = true;
                            raceController.CheckAllCarsFinished();
                        }
                    }
                    // Adds to the checkpoints which is how live scoring is done on track. 
                    // Reduces complexity of trying to work out distance
                    carInfo.checkpointsPassed++;
                    leaderboard.carCp[carInfo.carName] = carInfo.checkpointsPassed;
                    carInfo.currentPosition = leaderboard.leaderboardPosition(carInfo.carName);
                    carInfo.lastCp = raceController.checkpoints[carInfo.index];
                    // Ensures that out of range error cannot occur from incrementing laps
                    carInfo.index = (carInfo.index+1)%raceController.checkpoints.Count; 
                }
                else{
                    carInfo.checkpointsPassed--;
                    leaderboard.carCp[carInfo.carName] = carInfo.checkpointsPassed;
                }

            }
            else if(carInfo.isFinished == false){
                carInfo.finalTime = Time.time;
                carInfo.isFinished = true;
            }
        }
    }
}
