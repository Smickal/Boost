using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{

    [SerializeField] float delayTime = 1f;

    [Header("SFX")]
    [SerializeField] AudioClip crashSFX;
    [SerializeField] AudioClip successSFX;

    [Header("VFX")]
    [SerializeField] ParticleSystem crashVFX;
    [SerializeField] ParticleSystem successVFX;

    AudioSource audioSource;
    PauseManager pauseManager;

    bool isTransitioning = false;
    bool isCollisionDisable = false;
    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        pauseManager = FindObjectOfType<PauseManager>();
    }

    private void Update()
    {
        RespondToDebugKeys();
    }

    private void RespondToDebugKeys()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            LoadNextLevel();
        }
        else if (Input.GetKeyDown(KeyCode.C))
        {
            isCollisionDisable = !isCollisionDisable;
        }
        else if(Input.GetKeyDown(KeyCode.Escape))
        {
            Debug.Log("Application Quit!");
            Application.Quit();
        }
    }



    void OnCollisionEnter(Collision collision)
    {
        if(isTransitioning || isCollisionDisable) { return; }

        switch (collision.gameObject.tag)
        {
            case "Friendly":
                Debug.Log("you're okay!");
                break;

            case "Finish":
                StartSuccessSequence();
                break;

            default:
                StartCrashSequence();
                break;
        }
    }

    private void StartSuccessSequence()
    {
        //Todo Add Particle Effects
        PrepareSequence();
        audioSource.PlayOneShot(successSFX);
        successVFX.Play();
        Invoke("LoadNextLevel", delayTime);
        FindObjectOfType<Destroy>().DisableTutorialText();
    }

    void StartCrashSequence()
    {
        //todo add Particle Effects
        PrepareSequence();
        audioSource.PlayOneShot(crashSFX);
        crashVFX.Play();
        FindObjectOfType<PauseManager>().IncreaseDeathCounter();
        Invoke("ReloadLevel", delayTime);
        FindObjectOfType<Destroy>().DisableTutorialText();
    }

    void PrepareSequence()
    {
        isTransitioning = true;
        audioSource.Stop();
        GetComponent<Movement>().enabled = false;
    }

    void ReloadLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
    }

    void LoadNextLevel()
    {
        int nextSceneIndex = SceneManager.GetActiveScene().buildIndex + 1;
        if (nextSceneIndex == SceneManager.sceneCountInBuildSettings)
        {
            nextSceneIndex = 0;
        }

        SceneManager.LoadScene(nextSceneIndex);
    }
}
