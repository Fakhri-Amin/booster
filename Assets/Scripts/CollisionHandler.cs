using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    [SerializeField] float levelLoadDelay;
    [SerializeField] AudioClip dieSFX;
    [SerializeField] AudioClip successSFX;
    [SerializeField] ParticleSystem dieParticle;
    [SerializeField] ParticleSystem successParticle;
    [SerializeField] GameObject theBody;

    private AudioSource audioSource;

    private bool isTranstioning = false;
    private bool colllisionDisabled = false;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
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
            colllisionDisabled = !colllisionDisabled; // toggle collision
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        if (isTranstioning || colllisionDisabled) { return; }

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
        // Instantiate(dieParticle, transform.position, Quaternion.identity);
        dieParticle.Play();
        GetComponent<PlayerMovement>().enabled = false;
        theBody.SetActive(false);
        Invoke("ReloadLevel", levelLoadDelay);
    }

    void StartSuccessSequence()
    {
        isTranstioning = true;
        audioSource.Stop();
        audioSource.PlayOneShot(successSFX);
        successParticle.Play();
        GetComponent<PlayerMovement>().enabled = false;
        GetComponent<Rigidbody>().freezeRotation = true;
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
