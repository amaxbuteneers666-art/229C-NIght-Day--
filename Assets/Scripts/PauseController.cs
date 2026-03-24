using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class PauseController : MonoBehaviour
{
    public GameObject pauseMenu;
    public TextMeshProUGUI countdownText;

    private bool isPaused = false;
    private bool isCountingDown = false;

    private Coroutine countdownCoroutine;

    void Start()
    {
    pauseMenu.SetActive(false);
    countdownText.gameObject.SetActive(false);
    }
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!isPaused)
            {
             PauseGame();
            }
            else
            {
                if (countdownCoroutine == null)
                {
                countdownCoroutine = StartCoroutine(ResumeWithCountdown());
                }
            }
        }
        
        
    }

    void PauseGame()
    {
        isPaused = true;
        pauseMenu.SetActive(true);
        Time.timeScale = 0f; // ⛔ หยุดเวลา
    }

    IEnumerator ResumeWithCountdown()
    {
       isCountingDown = true;
    pauseMenu.SetActive(false);
    countdownText.gameObject.SetActive(true);
    countdownText.text = ""; // 🔥 เคลียร์ก่อน

    yield return StartCoroutine(Countdown(3));

    countdownText.gameObject.SetActive(false);

    Time.timeScale = 1f;
    isPaused = false;
    isCountingDown = false;

    countdownCoroutine = null;
    }

    public void OnResumeButton()
    {
    if (!isCountingDown)
    {
        StartCoroutine(ResumeWithCountdown());
    }
    }

    public void OnRestartButton()
    {
    Time.timeScale = 1f; // 🔥 สำคัญ! กันเกมค้าง

    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    IEnumerator Countdown(int seconds)
    {
        for (int i = seconds; i > 0; i--)
        {
        countdownText.text = i.ToString();
        yield return new WaitForSecondsRealtime(1f);
        }

        countdownText.text ="GO!";
        yield return new WaitForSecondsRealtime(1f);
    }
}