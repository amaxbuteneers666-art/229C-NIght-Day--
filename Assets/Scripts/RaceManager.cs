
using System.Collections;
using UnityEngine;
using TMPro;

public class RaceManager : MonoBehaviour
{
    public static RaceManager Instance;
    [Header("Ui References")]
    [SerializeField] private TextMeshProUGUI currentLapTimeText;
    
    [SerializeField] private TextMeshProUGUI bestLaptimeText;
    [SerializeField] private TextMeshProUGUI overallRaceTimeText;
    [SerializeField] private TextMeshProUGUI LapText;

    [Header("Race Settings")]
    [SerializeField] private Checkpoint[] checkpoints;
    [SerializeField] private int lastCheckpointIndex = -1;
    [SerializeField] private bool isCircuit = false;
    [SerializeField] private int totalLaps = 1 ;
 
    private int currentLap = 1;

    private bool raceStarted = false;
    private bool raceFinished = false;

    [Header("Lap Timer")]
    private float currentLapTime = 0f;
    private float overallRaceTime = 0f;
    private float bestLapTime = Mathf.Infinity;
    
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
    private void Update()
    {
        if(raceStarted)
        {
            UpdateTimers();
        }
        UpdaerUI();
    }

    #endregion

    #region Checkpoint Managament

    public void CheckpointReached(int checkpointTndex)
    {
        if((!raceStarted && checkpointTndex != 0) || raceFinished) return;
        
        if(checkpointTndex == lastCheckpointIndex + 1)
        {
            //UpdateCheckpoint();
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
            else if (isCircuit && lastCheckpointIndex == checkpoints.Length - 1)
            {
                OnLapFinish();
            }
        }
        else if (!isCircuit&& checkpointTndex == checkpoints.Length - 1)
        {
            OnLapFinish();
        }
        lastCheckpointIndex = checkpointTndex;
    }
    #endregion

    #region  Race Management

    private void OnLapFinish()
    {
        currentLap++;
        
        if(currentLapTime < bestLapTime)
        {
            bestLapTime = currentLapTime;
        }

        if (currentLap > totalLaps)
        {
            EndRace();
        }
        else
        {
            currentLapTime = 0f;
            lastCheckpointIndex = isCircuit ? 0 : -1;

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

    private void UpdateTimers()
    {
        currentLapTime += Time.deltaTime;
        overallRaceTime += Time.deltaTime;
    }
    private void UpdaerUI()
    {
        currentLapTimeText.text = FormatTime(currentLapTime);
        overallRaceTimeText.text = FormatTime(overallRaceTime);
        LapText.text="Lap:"+currentLap+"/"+totalLaps;
        bestLaptimeText.text = FormatTime(bestLapTime);
    }

    #endregion

    #region Utility Functions
    
    
    private string FormatTime(float time) 
    {
        if(float.IsInfinity(time) || time < 0 ) return "--:--";

        int minutes = (int)time / 60;
        float seconds = time % 60;
        return string.Format("{0:00}:{1:00}",minutes,seconds);
    }
    #endregion
}