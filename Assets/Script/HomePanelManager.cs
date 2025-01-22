using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;

public class HomePanelManager : MonoBehaviour
{
    public GameObject homePanel;

    private string saveDataPath;
    private void Awake()
    {
        if (homePanel == null)
        {
            homePanel = GameObject.Find("HomePanel");
            if (homePanel == null)
            {
                Debug.LogWarning("HomePanel not found in the current scene!");
            }
            else
            {
                Debug.Log("HomePanel successfully linked to HomePanelManager.");
            }
        }
    }
    private void Start()
    {
        StartCoroutine(FindHomePanel());

    }

    private IEnumerator FindHomePanel()
    {
        while (homePanel == null)
        {
            homePanel = GameObject.Find("HomePanel");
            if (homePanel == null)
            {
                yield return null;
            }
        }
        Debug.Log("HomePanel assigned successfully!");
    }

    public void OpenHomePanel()
    {
        homePanel.SetActive(true);
    }

    public void CloseHomePanel()
    {
        homePanel.SetActive(false);
    }

    public void LoadLevel1()
    {
        SceneManager.LoadScene("Garden");
    }

    public void LoadLevel2()
    {
        SceneManager.LoadScene("Beach");
    }

    public void LoadMainMenu()
    {
        SceneManager.LoadScene("HomeScene");
    }

    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("Game Quit");
    }

    //private void OnEnable()
    //{
    //    SceneManager.sceneLoaded += OnSceneLoaded;
    //}

    //private void OnDisable()
    //{
    //    SceneManager.sceneLoaded -= OnSceneLoaded;
    //}

    //private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    //{
    //    if (homePanel == null)
    //    {
    //        homePanel = GameObject.Find("HomePanel");

    //        if (homePanel == null && GameManager.instance != null)
    //        {
    //            homePanel = GameManager.instance.homePanel;
    //        }

    //        if (homePanel != null)
    //        {
    //            Debug.Log("HomePanel successfully reassigned in HomePanelManager.");
    //        }
    //        else
    //        {
    //            Debug.LogError("HomePanel is still null after reassignment attempt.");
    //        }
    //    }
    //}
}

