using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class HUDManager : MonoBehaviour {

    public Image pausemenu, pauseindicator, death, victory;

    void Update ()
    {
        if (Input.GetButtonDown("Cancel") && death.gameObject.activeInHierarchy == false && victory.gameObject.activeInHierarchy == false && pausemenu.gameObject.activeInHierarchy == false)
        {
            Pause();
        }
    }

    public void Pause()
    {
        if (pausemenu.gameObject.activeInHierarchy == false)
        {
            pausemenu.gameObject.SetActive(true);
            pauseindicator.gameObject.SetActive(true);
            Time.timeScale = 0;
            AudioListener.volume = 0;
            Cursor.lockState = CursorLockMode.None;
        }
        else if (pausemenu.gameObject.activeInHierarchy == true)
        {
            pausemenu.gameObject.SetActive(false);
            pauseindicator.gameObject.SetActive(false);
            Time.timeScale = 1;
            AudioListener.volume = 1;
            Cursor.lockState = CursorLockMode.Locked;
        }
    }

    public void MenuBtn()
    {
        pausemenu.gameObject.SetActive(false);
        pauseindicator.gameObject.SetActive(false);
        Time.timeScale = 1;
        AudioListener.volume = 1;
        Cursor.lockState = CursorLockMode.None;
        SceneManager.LoadScene(0);
    }
    public void RetryBtn()
    {
        if (PlayerPrefs.HasKey("randomlevel"))
        {
            int i = Random.Range(1, 5);
            print(i);
            SceneManager.LoadScene(i);
        }
        else
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }

    public void ExitBtn()
    {
        AudioListener.volume = 1;
        Application.Quit();
    }
}
