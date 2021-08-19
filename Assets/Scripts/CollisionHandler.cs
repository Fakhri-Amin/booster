using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    [SerializeField] float levelLoadDelay;
    [SerializeField] AudioClip dieSFX;
    [SerializeField] AudioClip successSFX;

    private AudioSource audioSource;

    private bool isTranstioning = false;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void OnCollisionEnter(Collision other)
    {
        if (isTranstioning) { return; }

        switch (other.gameObject.tag)
        {
            case "Friendly":
                break;
            case "Finish":
                StartSuccessSequence();
                break;
            default:
                StartCrashSequence();
                break;
        }
    }

    void StartCrashSequence()
    {
        isTranstioning = true;
        audioSource.Stop();
        audioSource.PlayOneShot(dieSFX);
        // TODO Add Partcile Effect
        GetComponent<PlayerMovement>().enabled = false;
        Invoke("ReloadLevel", levelLoadDelay);
    }

    void StartSuccessSequence()
    {
        isTranstioning = true;
        audioSource.Stop();
        audioSource.PlayOneShot(successSFX);
        // TODO Add Partcile Effect
        GetComponent<PlayerMovement>().enabled = false;
        Invoke("LoadNextLevel", levelLoadDelay);
    }

    private void ReloadLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
    }

    private void LoadNextLevel()
    {
        int nextSceneIndex = SceneManager.GetActiveScene().buildIndex + 1;
        if (nextSceneIndex == SceneManager.sceneCountInBuildSettings)
        {
            nextSceneIndex = 0;
        }
        SceneManager.LoadScene(nextSceneIndex);
    }
}
