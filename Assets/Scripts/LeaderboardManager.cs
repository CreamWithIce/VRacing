using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
public class LeaderboardManager : MonoBehaviour
{
    public Dictionary<string,int> carCp = new Dictionary<string,int>();

    // Input: the name of the car wanted to check on the position
    // Outputs the position of the car based on a linear search through a sorted dictionary
    public int leaderboardPosition(string carName){

        Dictionary<string,int> sortedCheckpoint = carCp.OrderBy(key => key.Value).ToDictionary(key => key.Key, key=>key.Value);
        int position = sortedCheckpoint.Count;
        foreach(KeyValuePair<string,int> carInfo in sortedCheckpoint){
            if(carInfo.Key == carName){
                return position;
            }
            position--;

        }
        return position;
    }
}
