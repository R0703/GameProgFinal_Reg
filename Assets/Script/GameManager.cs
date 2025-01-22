using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class GameManager : MonoBehaviour
{
    //public static GameManager instance;

    //private void Awake()
    //{
    //    if (instance == null)
    //    {
    //        instance = this;
    //        DontDestroyOnLoad(gameObject);
    //    }
    //    else
    //    {
    //        Destroy(gameObject);
    //    }
    //}
    //}
    public static GameManager instance;

    public Text mobHPText;
    public Text playerHPText;
    public Text enemiesCountText;
    //public GameObject winPanel;

    public int goldCount = 0;
    public int enemiesDefeated = 0;
    public int currentLevel = 1;
    private int winCondition = 3;

    [SerializeField]
    private GameObject winPanel;

    public GameObject homePanel;


    private void Awake()
    {
        if (instance == null)
        {
            instance = this;

            if (FindObjectsOfType(GetType()).Length > 1)
            {
                Destroy(gameObject);
            }
            else
            {
                DontDestroyOnLoad(gameObject);

                if (homePanel != null)
                {
                    DontDestroyOnLoad(homePanel);
                }
            }
        }
        else
        {
            Destroy(gameObject);
        }
    }


    private void Start()
    {
        if (homePanel == null)
        {
            Debug.LogError("HomePanel ada tp blm load");
        }
        else
        {
            Debug.Log("HomePanel ga di asg di inspector.");
        }
        LoadGameProgress();
        UpdateUI();
    }

    public void setPlayerHPText(string text)
    {
        if (playerHPText != null)
        {
            playerHPText.text = text;
        }
        else
        {
            Debug.LogWarning("playerHPText is not assigned in the GameManager.");
        }
    }

    public void setMobHPText(string text)
    {
        if (mobHPText != null)
        {
            mobHPText.text = text;
        }
        else
        {
            Debug.LogWarning("mobHPText is not assigned in the GameManager.");
        }
    }

    public void SetWinPanel(bool isActive)
    {
        if (winPanel != null)
        {
            winPanel.SetActive(isActive);
        }
        else
        {
            Debug.LogWarning("winPanel is not assigned in the GameManager.");
        }
    }


    public void IncrementEnemiesDefeated()
    {
        enemiesDefeated++;
        Debug.Log($"Enemies Defeated Incremented: {enemiesDefeated}");

        UpdateEnemiesUI();

        if (enemiesDefeated >= winCondition)
        {
            Debug.Log("Win condition met. Showing win panel.");
            //ShowWinPanel();
            StartCoroutine(ShowWinPanelWithDelay());

        }
    }

    private void ShowWinPanel()
    {

        if (winPanel == null)
        {
            Debug.LogWarning("WinPanel is null. Attempting to re-link.");
            winPanel = GameObject.Find("WinPanel");
        }

        if (winPanel != null)
        {
            Debug.Log("Activating WinPanel.");
            winPanel.SetActive(true);
            Time.timeScale = 0f; //cann possibly cause issue, tambahin delay
        }
        else
        {
            Debug.LogError("WinPanel is still null after re-link attempt. Crashing may occur.");
        }

    }

    private IEnumerator ShowWinPanelWithDelay()
    {
        if (winPanel == null)
        {
            Debug.Log("Reassigning WinPanel...");
            winPanel = FindWinPanel();
        }

        if (winPanel != null)
        {
            Debug.Log("Activating WinPanel.");
            winPanel.SetActive(true);
            yield return new WaitForEndOfFrame();
            Time.timeScale = 0f;
        }
        else
        {
            Debug.LogError("WinPanel is null. Cannot show win panel.");
        }
    }

    private GameObject FindWinPanel()
    {
        GameObject panel = GameObject.Find("WinPanel");

        if (panel == null)
        {
            foreach (GameObject go in Resources.FindObjectsOfTypeAll<GameObject>())
            {
                if (go.name == "WinPanel" && go.hideFlags == HideFlags.None)
                {
                    panel = go;
                    break;
                }
            }
        }

        return panel;
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        ReinitializeUI();
    }

    public void CollectGold()
    {
        goldCount++;
        UpdateGoldUI();

        if (goldCount >= winCondition && currentLevel == 1)
        {
            currentLevel = 2;
            SaveGame();
            SceneManager.LoadScene("Beach");
        }
    }

    public void ResetGame()
    {
        goldCount = 0;
        enemiesDefeated = 0;
        currentLevel = 1;

        SaveManager.ResetSave();
        SaveGame();
        SceneManager.LoadScene("HomeScene");
    }

    public void SaveGame()
    {
        SaveData saveData = new SaveData
        {
            goldCount = (currentLevel == 1) ? goldCount : 0,
            enemiesDefeated = (currentLevel == 2) ? enemiesDefeated : 0,
            currentLevel = currentLevel
        };
        SaveManager.SaveGame(saveData);
        Debug.Log("Game Saved: " + JsonUtility.ToJson(saveData, true));
    }

    private void LoadGameProgress()
    {
        SaveData saveData = SaveManager.LoadGame();

        goldCount = (saveData.currentLevel == 1) ? saveData.goldCount : 0;

        //reset enemies defeated pas ke lvl 2 jd 0
        if (saveData.currentLevel == 2)
        {
            enemiesDefeated = 0;
        }
        else
        {
            enemiesDefeated = saveData.enemiesDefeated;
        }

        currentLevel = saveData.currentLevel;

        Debug.Log($"Game Loaded: GoldCount={goldCount}, EnemiesDefeated={enemiesDefeated}, CurrentLevel={currentLevel}");
    }

    private void ReinitializeUI()
    {
        playerHPText = GameObject.Find("PlayerHPText")?.GetComponent<Text>();
        enemiesCountText = GameObject.Find("EnemiesCountText")?.GetComponent<Text>();
        mobHPText = GameObject.Find("MobHPText")?.GetComponent<Text>();
  
        winPanel = FindWinPanel();

        ////debug wp
        if (winPanel != null)
        {
            Debug.LogWarning("WinPanel reinstatedddd.");
        }


        UpdateUI();
    }

    private void UpdateUI()
    {
        if (currentLevel == 1)
        {
            UpdateGoldUI();
        }
        else if (currentLevel == 2)
        {
            UpdateEnemiesUI();
        }
    }

    private void UpdateGoldUI()
    {
        if (playerHPText != null && currentLevel == 1)
        {
            playerHPText.text = goldCount.ToString();
        }
    }

    private void UpdateEnemiesUI()
    {
        if (enemiesCountText != null)
        {
            enemiesCountText.text = enemiesDefeated.ToString();
        }
        else
        {
            Debug.LogWarning("EnemiesCountText gaada");
        }
    }
}
