using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Oscillator : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] Vector3 movementVector;
    [SerializeField] float period = 2f;

    Vector3 startPos;
    float movementFactor;

    void Start()
    {
        startPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (period <= Mathf.Epsilon) { return; }

        float cycles = Time.time / period;   //continually growing over time
        const float tau = Mathf.PI * 2;      //contant value of 6.283
        float rawSinWave = Mathf.Sin(tau * cycles); // going from -1 to 1

        movementFactor = (rawSinWave + 1f) / 2f; // recalculate it to 0 to 1

        Vector3 offset = movementVector * movementFactor;
        transform.position = startPos + offset;
    }
}
