using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Oscillator : MonoBehaviour
{
    [SerializeField] Vector3 movementVector;
    [SerializeField] [Range(0,1)] float MovementFactor;
    [SerializeField] float period = 2f;
    Vector3 startingPosition;
    // Start is called before the first frame update
    void Start()
    {
        startingPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        float cycles = Time.time / period;
        const float tau = Mathf.PI * 2;
        float rawSinWave = Mathf.Sin(cycles * tau);
        MovementFactor = (rawSinWave + 1f) / 2;
        Vector3 offset = movementVector * MovementFactor;
        transform.position = startingPosition - offset;
    }
}
