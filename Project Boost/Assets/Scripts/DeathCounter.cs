using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
public class DeathCounter : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI counterText;
    void SetText(string text)
    {
        counterText.text = text;
    }

    private void Start()
    {
        int currentDeathcounter = FindObjectOfType<PauseManager>().GetCurrentDeathCounter();
        SetText(currentDeathcounter.ToString());
        FindObjectOfType<PauseManager>().DisableUI();
    }

    public void GoBackToMainMenu()
    {
        Destroy(FindObjectOfType<PauseManager>().gameObject);
        SceneManager.LoadScene(0);
    }

    public void ResetLevel()
    {
        Destroy(FindObjectOfType<PauseManager>().gameObject);
        SceneManager.LoadScene(1);
    }
}
