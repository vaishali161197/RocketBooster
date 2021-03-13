using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField] float RocketSpeed = 100f;
    [SerializeField] float RocketRotation = 1f;
    [SerializeField] AudioClip EngineSound;
    [SerializeField] ParticleSystem mainBoosterParticle;
    [SerializeField] ParticleSystem leftThrusterParticle;
    [SerializeField] ParticleSystem rightThrusterParticle;
    Rigidbody Rb;
    AudioSource audioSource;
    // Start is called before the first frame update
    void Start()
    {
        Rb = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        ProcessThrust();
        ProcessRotation();
    }

    private void ProcessThrust()
    {
        if(Input.GetKey(KeyCode.Space))
        {
            Rb.AddRelativeForce(Vector3.up * Time.deltaTime * RocketSpeed);
            if (!audioSource.isPlaying)

            {
                audioSource.PlayOneShot(EngineSound);


            }
            if(!mainBoosterParticle.isPlaying)
            {
                mainBoosterParticle.Play();
            }
        }
        else
        {
            audioSource.Stop();
            mainBoosterParticle.Stop();
        }

    }

    private void ProcessRotation()
    {
        if(Input.GetKey(KeyCode.A))
        {
            ApplyRotation(RocketRotation);
            if (!rightThrusterParticle.isPlaying)
            {
                rightThrusterParticle.Play();
            }
        }
        else if(Input.GetKey(KeyCode.D))
        {
            ApplyRotation(-RocketRotation);
            if (!leftThrusterParticle.isPlaying)
            {
                leftThrusterParticle.Play();
            }

        }
        else
        {
            rightThrusterParticle.Stop();
            leftThrusterParticle.Stop();
        }
    }

    private void ApplyRotation(float rocketRotation)
    {
        Rb.freezeRotation = true; //freezing rotation so we can manually rotate
        transform.Rotate(Vector3.forward * Time.deltaTime * rocketRotation);
        Rb.freezeRotation = false; //unfreezing rotation so the physics system takeover
    }




}
