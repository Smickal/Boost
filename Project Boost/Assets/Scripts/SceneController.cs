using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    public void ResetLevel()
    {
        SceneManager.LoadScene(1);
    }

    public void GoBackToMainMenu()
    {
        SceneManager.LoadScene(0);
    }

    
}
