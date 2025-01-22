using UnityEngine;
using UnityEngine.SceneManagement;

public class HomeScreenManager : MonoBehaviour
{
    public GameObject creditsPanel; 

 
    public void LoadLevel1()
    {
        SceneManager.LoadScene("Garden");
    }

    public void ToggleCreditsPanel()
    {
        if (creditsPanel != null)
        {
            creditsPanel.SetActive(!creditsPanel.activeSelf);
        }
    }

    public void ToggleCloseCreditsPanel()
    {
        if (creditsPanel != null && creditsPanel.activeSelf)
        {
            creditsPanel.SetActive(false);
        }
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
