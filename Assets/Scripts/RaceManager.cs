using System.Collections;
using UnityEngine;

public class RaceManager : MonoBehaviour
{
    public static RaceManager Instance;

    [Header("Race Settings")]
    [SerializeField] private Checkpoint[] checkpoints;
    [SerializeField] private int lastCheckpointIndex = -1;
    [SerializeField] private bool iscircuit = false;
    [SerializeField] private int totalLaps = 1 ;

    private int currentLap = 1;

    private bool raceStarted = false;
    private bool raceFinished = false;
    
    #region  Unity Functions

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }       
    }
    
    #endregion

    #region Checkpoint Managament

    public void CheckpointReached(int checkpointTndex)
    {
        if((!raceStarted && checkpointTndex != 0) || raceFinished) return;
        
        if(checkpointTndex == lastCheckpointIndex + 1)
        {
            //
        }
    }

    private void UpdateCheckpoint(int checkpointTndex)
    {
        if(checkpointTndex == 0)
        {
            if(!raceStarted)
            {
                StartRace();
            }
            else if (iscircuit && lastCheckpointIndex == checkpoints.Length - 1)
            {
                OnLapFinish();
            }
        }
        else if (!iscircuit&& checkpointTndex == checkpoints.Length - 1)
        {
            OnLapFinish();
        }
    }
    #endregion

    #region  Race Management

    private void OnLapFinish()
    {
        currentLap++;

        if (currentLap > totalLaps)
        {
            EndRace();
        }
    }
    private void StartRace()
    {
        raceStarted = true;
        raceFinished = false;
    }

    private void EndRace()
    {
        raceStarted = false;
        raceFinished = true;
    }

    #endregion
}