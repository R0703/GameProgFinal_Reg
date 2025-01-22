using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.IO;

public class GoldStats : MonoBehaviour
{
    public Text goldCountText;
    private int goldCount = 0;
    private int requiredGold = 3;
    public AudioClip coinSFX; 
    private AudioSource audioSource;

    //public GameObject homePanelController;
    //public GameObject homePanelManager;

    void Start()
    {
        audioSource = gameObject.AddComponent<AudioSource>();

        // Sync gold count dg GameManager
        ReinitializeGoldStats();
    }

    public void ReinitializeGoldStats()
    {
        // Sync gold count with GameManager
        if (GameManager.instance != null)
        {
            goldCount = GameManager.instance.goldCount;
        }

        UpdateGoldUI();
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Gold"))
        {
            goldCount++;

            UpdateGoldUI();
            PlayCoinSFX();

            Destroy(collision.gameObject);

            if (GameManager.instance != null)
            {
                GameManager.instance.goldCount = goldCount;
                GameManager.instance.SaveGame(); // Save progress
            }

            if (goldCount >= requiredGold)
            {
                ChangeScene();
            }
        }
    }
    private void PlayCoinSFX()
    {
        if (coinSFX != null && audioSource != null)
        {
            audioSource.PlayOneShot(coinSFX);
        }
    }
    private void UpdateGoldUI()
    {
        goldCountText.text = goldCount.ToString();
    }

    private void ChangeScene()
    {
        if (GameManager.instance != null)
        {
            GameManager.instance.currentLevel = 2;

            SaveManager.ResetSave();
            SaveData newSaveData = new SaveData
            {
                goldCount = 0,
                enemiesDefeated = 0,
                currentLevel = 2
            };
            SaveManager.SaveGame(newSaveData);

            // Ensure homePanel persists
            if (GameManager.instance.homePanel != null)
            {
                DontDestroyOnLoad(GameManager.instance.homePanel);
            }
        }

        // Load Level 2 yaitu beach
        SceneManager.LoadScene("Beach");
    }
}
