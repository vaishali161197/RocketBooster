using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RocketCollision : MonoBehaviour
{
    [SerializeField] float LevelLoadDelay = 1f;
    [SerializeField] AudioClip Crash;
    [SerializeField] AudioClip Success;
    [SerializeField] ParticleSystem crashParticle;
    [SerializeField] ParticleSystem successParticle;
    AudioSource audiosoure;
    bool isCollided = false;
    bool collisionDisabled = false;
    private void Start()
    {
        audiosoure = GetComponent<AudioSource>();
    }
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.L))
        {
            LoadNextLevel();
        }
        else if(Input.GetKeyDown(KeyCode.C))
        {
            collisionDisabled = !collisionDisabled;
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if(isCollided || collisionDisabled)
        {
            return;
        }
        switch (collision.gameObject.tag)
        {
            case "Friendly":
                Debug.Log(" You Bumped into launching pad ");
                break;

            case "Finish":
                StartSuccessSequence();
                break;

            default:
                StartCrashSequence();
                break;
        }


    }

    private void LoadNextLevel()
    {
        int NextSceneIndex = SceneManager.GetActiveScene().buildIndex + 1;
        if(NextSceneIndex == SceneManager.sceneCountInBuildSettings)
        {
            NextSceneIndex = 0;
        }
        SceneManager.LoadScene(NextSceneIndex);
    }

    private void ReloadLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
    }
    private void StartCrashSequence()

    {
        isCollided = true;
        audiosoure.Stop();
        audiosoure.PlayOneShot(Crash);
        crashParticle.Play();
        GetComponent<Movement>().enabled = false;
        Invoke("ReloadLevel", LevelLoadDelay);
    }

    private void StartSuccessSequence()
    {
        isCollided = true;
        audiosoure.Stop();
        audiosoure.PlayOneShot(Success);
        successParticle.Play();
        GetComponent<Movement>().enabled = false;
        Invoke("LoadNextLevel", LevelLoadDelay);
    }
}
