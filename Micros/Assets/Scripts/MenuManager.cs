using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public Image bg, levels, cancel;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.None;
    }

    public void PlayBtn ()
    {
        bg.gameObject.SetActive(true);
        levels.gameObject.SetActive(true);
        cancel.gameObject.SetActive(true);
    }

    public void CancelBtn()
    {
        bg.gameObject.SetActive(false);
        levels.gameObject.SetActive(false);
        cancel.gameObject.SetActive(false);
    }

    public void RandomBtn()
    {
        int i = Random.Range(1, 5);
        if (!PlayerPrefs.HasKey("randomlevel"))
        {
            PlayerPrefs.SetInt("randomlevel", 0);
        }
        SceneManager.LoadScene(i);
    }

    public void EasyBtn()
    {
        DeleteRandomKey();
        SceneManager.LoadScene(1);
    }

    public void NormalBtn()
    {
        DeleteRandomKey();
       SceneManager.LoadScene(2);
    }

    public void HardBtn()
    {
        DeleteRandomKey();
        SceneManager.LoadScene(3);
    }

    public void SteamerBtn()
    {
        DeleteRandomKey();
        SceneManager.LoadScene(4);
    }

    public void ExitBtn ()
    {
        DeleteRandomKey();
        Application.Quit();
    }

    void DeleteRandomKey()
    {
        if (PlayerPrefs.HasKey("randomlevel"))
        {
            PlayerPrefs.DeleteKey("randomlevel");
        }
    }
}
