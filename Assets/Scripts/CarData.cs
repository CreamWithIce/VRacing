using UnityEngine;
using System.Collections.Generic;
public class CarData : MonoBehaviour {
    // Provides necessary data about each car to be used by other scripts
    // in terms of position on the leader board, deciding which car should disappear, etc.
    public string carName;
    public MeshRenderer chassis;
    public int priority = 1;
    [SerializeField] private LeaderboardManager leaderboard;
    [HideInInspector] public float finalTime;
    [HideInInspector] public int checkpointsPassed = 0;
    [HideInInspector] public int numLaps = 0;
    [HideInInspector] public bool isFinished = false;
    [HideInInspector] public int currentPosition = 12;
    [HideInInspector] public int index = 0;
    [HideInInspector] public Transform lastCp;
    [HideInInspector] public float startTime;
    private void Start() {
        startTime = Time.time;
        lastCp = transform;
        leaderboard.carCp.Add(carName,checkpointsPassed);
    }
}