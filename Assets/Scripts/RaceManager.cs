using UnityEngine;
using TMPro;

public class RaceManager : MonoBehaviour
{

    public static RaceManager Instance;

    [Header("Result UI")]
    public GameObject resultPanel;      // ลาก ResultPanel มาใส่ที่นี่
    public TextMeshProUGUI finalTimeText; // ลาก Text ที่จะโชว์เวลาสรุปมาใส่ที่นี่
    
    
    
    [Header("Lap Settings")]
    public int totalLaps = 3;
    private int currentLap = 0;

    [Header("Checkpoint")]
    public int totalCheckpoints = 0;
    private int passedCheckpoints = 0;

    [Header("Time")]
    private float currentLapTime = 0f;

    [Header("UI")]
    public TextMeshProUGUI lapText;
    public TextMeshProUGUI lapTimeText;


    
    private void Awake()
    {
        Instance = this;
    }

    private void Update()
    {
        currentLapTime += Time.deltaTime;
        //lapTimeText.text = "Time: " + currentLapTime.ToString("F2");
        lapTimeText.text = ": " + currentLapTime.ToString("F2");
    }

    public void PassCheckpoint()
    {
        passedCheckpoints++;
    }

private void Start()
    {
        Time.timeScale = 1f;
        UpdateLapUI(); 
    }



    private void UpdateLapUI()
    {
        if (lapText != null)
        {
            lapText.text = ": " + currentLap + "/" + totalLaps;
        }
    }

    public void CrossFinishLine()
    {
        Debug.Log("Hit Finish Line! Checkpoints passed: " + passedCheckpoints);

        if (passedCheckpoints >= totalCheckpoints)
        {
            currentLap++;
            passedCheckpoints = 0;

            UpdateLapUI();

            if (currentLap >= totalLaps)
            {
                Debug.Log("Finish Race!");
                // อาจจะเพิ่ม Time.timeScale = 0; เพื่อหยุดเกม\

                Invoke("ShowResults", 0.5f); // เรียกฟังก์ชันสรุปผล
                 return; // หยุดการทำงานอื่นในฟังก์ชันนี้
            }

            passedCheckpoints = 0;
            currentLapTime = 0f;
            


        }
        else 
        {
            Debug.Log("ยังผ่าน Checkpoint ไม่ครบ! ขาดอีก: " + (totalCheckpoints - passedCheckpoints));
        }
    }

    void ShowResults()
    {
   // 1. เปิดหน้าจอสรุปผลขึ้นมาก่อน
    resultPanel.SetActive(true);

    // 2. คำนวณและสั่งเปลี่ยนตัวหนังสือใน "เฟรมปัจจุบัน" นี้เลย (สำคัญมาก)
    if (finalTimeText != null)
    {
        finalTimeText.text = "Your Time: " + currentLapTime.ToString("F2") + "s";
    }

    // 3. ค่อยหยุดเวลาเกมเป็นศูนย์
    Time.timeScale = 0f;

    
    }



    
}