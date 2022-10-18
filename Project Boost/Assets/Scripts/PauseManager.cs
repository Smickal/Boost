using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
public class PauseManager : MonoBehaviour
{
    [SerializeField] GameObject pausePanelObj;
    [SerializeField] GameObject pauseButton;

    [SerializeField] GameObject deathCounterText;
    [SerializeField] TextMeshProUGUI counterText;


    int deathCounter = 0;

    private void Start()
    {
        counterText.text = deathCounter.ToString();    
    }

    public void ResetLevel()
    {
        SceneManager.LoadScene(1);
        pauseButton.SetActive(true);
        Destroy(gameObject);
    }

    public void GoBackToMainMenu()
    {
        SceneManager.LoadScene(0);
        Destroy(gameObject);
    }

    public void PauseButtonPressed()
    {
        Time.timeScale = 0f;
        pausePanelObj.SetActive(true);
    }

    public void ResumeButtonPressed()
    {
        Time.timeScale = 1f;
        pausePanelObj.SetActive(false);
    }

    public void QuitApplication()
    {
        Application.Quit();
    }
    
    public void IncreaseDeathCounter()
    {
        deathCounter += 1;
        counterText.text = deathCounter.ToString();
    }

    public int GetCurrentDeathCounter()
    {
        return deathCounter;
    }

    public void DisableUI()
    {
        pauseButton.SetActive(false);
        deathCounterText.SetActive(false);
    }
}
