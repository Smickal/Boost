using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [Header("Force of Ship")]
    [SerializeField] float thrusterForce = 100f;
    [SerializeField] float rotationForce = 100f;

    [Header("SFX")]
    [SerializeField] AudioClip mainEngineSFX;
    [SerializeField] AudioClip sideEngineSFX;
    AudioSource sideAudioSource;

    [Header("VFX")]
    [SerializeField] ParticleSystem mainBoosterVFX;
    [SerializeField] ParticleSystem leftSideBoosterVFX;
    [SerializeField] ParticleSystem RightSideBoosterVFX;


    Rigidbody rb;
    AudioSource audioSource;

    public bool isMenu;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
        sideAudioSource = gameObject.AddComponent<AudioSource>();
    }


    void Update()
    {
        if(!isMenu)
        {
            ProcessThrust();
            ProcessRotation();
        }
        else
        {
            ProcessMenuShip();
        }
    }

    private void ProcessMenuShip()
    {
        if (!mainBoosterVFX.isPlaying)
        {
            mainBoosterVFX.Play();
        }
        if (!leftSideBoosterVFX.isPlaying)
        {
            leftSideBoosterVFX.Play();
        }
        if (!RightSideBoosterVFX.isPlaying)
        {
            RightSideBoosterVFX.Play();
        }
    }

    void ProcessThrust()
    {
        if(Input.GetKey(KeyCode.Space))
        {
            StartThrusting();
        }
        else
        {
            StopThrusting();
        }


    }

    void ProcessRotation()
    {
        bool getLeftInput = Input.GetKey(KeyCode.D);
        bool getRightInput = Input.GetKey(KeyCode.A);

        if (getLeftInput && !getRightInput)
        {
            RotateRight();
        }
        else if (getRightInput && !getLeftInput)
        {
            RotateLeft();
        }
        else
        {
            StopRotate();
        }

    }

    void StartThrusting()
    {
        rb.AddRelativeForce(Vector3.up * thrusterForce * Time.deltaTime);
        if (!audioSource.isPlaying)
        {
            audioSource.PlayOneShot(mainEngineSFX);
        }
        if (!mainBoosterVFX.isPlaying)
        {
            mainBoosterVFX.Play();
        }

    }

    private void StopThrusting()
    {
        audioSource.Stop();
        mainBoosterVFX.Stop();
    }

    private void RotateRight()
    {
        ApplyRotation(-rotationForce);
        if (!leftSideBoosterVFX.isPlaying)
        {
            leftSideBoosterVFX.Play();
        }
        if(!sideAudioSource.isPlaying)
        {
            sideAudioSource.PlayOneShot(sideEngineSFX);
        }
    }

    private void RotateLeft()
    {
        ApplyRotation(rotationForce);
        if (!RightSideBoosterVFX.isPlaying)
        {
            RightSideBoosterVFX.Play();
        }
        if (!sideAudioSource.isPlaying)
        {
            sideAudioSource.PlayOneShot(sideEngineSFX);
        }
    }

    private void StopRotate()
    {
        leftSideBoosterVFX.Stop();
        RightSideBoosterVFX.Stop();
        sideAudioSource.Stop();
    }

    public void ApplyRotation(float rotationThisFrame)
    {
        rb.freezeRotation = true; //freezing rotation so we can manually rotate   
        transform.Rotate(Vector3.forward * rotationThisFrame * Time.deltaTime);
        rb.freezeRotation = false; // un-freezing rotation so physics system can takeover
    }
}

