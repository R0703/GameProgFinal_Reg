using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HomePanelController : MonoBehaviour
{
    public GameObject homePanel;

    private void Awake()
    {
        if (homePanel == null)
        {
            homePanel = GameObject.Find("HomePanel");
            if (homePanel == null)
            {
                Debug.LogWarning("HomePanel not found");
            }
            else
            {
                Debug.Log("HomePanel linked.");
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
        Debug.Log("HomePanel assigned!");
    }

    public void ReinitializeHomePanel()
    {
        if (homePanel == null)
        {
            homePanel = GameObject.Find("HomePanel");
        }

        if (homePanel == null)
        {
            Debug.LogWarning("HomePanel not found");
        }
        else
        {
            Debug.Log("HomePanel linked");
        }
    }
    public void ToggleHomePanel()
    {
        if (homePanel != null)
        {
            homePanel.SetActive(!homePanel.activeSelf);
            Debug.Log($"HomePanel toggled to active: {homePanel.activeSelf}");
        }
        else
        {
            Debug.LogError("HomePanel isnt assigned");
        }
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
    //    if (homePanel == null) // Reassign homePanel if it's null
    //    {
    //        homePanel = GameObject.Find("HomePanel");

    //        if (homePanel == null && GameManager.instance != null)
    //        {
    //            homePanel = GameManager.instance.homePanel;
    //        }

    //        if (homePanel != null)
    //        {
    //            Debug.Log("HomePanel successfully reassigned in HomePanelController.");
    //        }
    //        else
    //        {
    //            Debug.LogError("HomePanel is still null after reassignment attempt.");
    //        }
    //    }
    //}
}

