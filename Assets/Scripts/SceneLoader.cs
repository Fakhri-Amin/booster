using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    [SerializeField] GameObject optionMenu;
    private bool optionToggle = false;

    private void Start()
    {
        Time.timeScale = 1;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            optionToggle = !optionToggle;
        }
        ShowOptionMenu();
    }

    private void ShowOptionMenu()
    {
        if (optionMenu == null) { return; }

        if (optionToggle)
        {
            optionMenu.SetActive(true);
            Time.timeScale = 0;
        }
        else
        {
            Time.timeScale = 1;
            optionMenu.SetActive(false);
        }
    }

    // Start is called before the first frame update
    public void LoadNextScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
    public void LoadMainMenu()
    {
        SceneManager.LoadScene(0);
        Time.timeScale = 1;
    }
    public void Quit()
    {
        Application.Quit();
    }
}
