using UnityEngine;
using UnityEngine.UI;

public class LevelSelectionManager : MonoBehaviour
{
    public Button level1Button;
    public Button level2Button;
    public Button level3Button;

    private void Start()
    {
        // Mengatur status tombol berdasarkan penyelesaian level dan pengumpulan clue
        SetupLevelButtons();
    }

    private void SetupLevelButtons()
    {
        // Cek apakah level 1 dan level 2 sudah selesai atau clue terkumpul
        bool level1Completed = PlayerPrefs.GetInt("Level_1_Completed", 0) == 1;
        bool level1ClueCollected = PlayerPrefs.GetInt("Level_1_ClueCollected", 0) == 1;
        bool level2Completed = PlayerPrefs.GetInt("Level_2_Completed", 0) == 1;

        // Level 1 selalu dapat diakses
        level1Button.interactable = true;

        // Level 2 hanya dapat diakses jika level 1 sudah selesai atau clue terkumpul
        level2Button.interactable = level1Completed || level1ClueCollected;
        
        // Level 3 hanya dapat diakses jika level 2 sudah selesai
        level3Button.interactable = level2Completed;

        // Menambahkan event listener untuk tombol
        level1Button.onClick.AddListener(() => LoadLevel("Stage1-Level1"));
        level2Button.onClick.AddListener(() => LoadLevel("Stage1-Level2"));
        level3Button.onClick.AddListener(() => LoadLevel("Stage1-Level3"));
    }

    private void LoadLevel(string levelName)
    {
        // Memuat scene berdasarkan nama level
        UnityEngine.SceneManagement.SceneManager.LoadScene(levelName);
    }

    // Method untuk menandai level sebagai selesai, dipanggil saat level selesai
    public void CompleteLevel(int levelIndex)
    {
        PlayerPrefs.SetInt($"Level_{levelIndex}_Completed", 1);
        PlayerPrefs.Save();
    }

    // Method untuk menandai clue sebagai terkumpul, dipanggil saat clue dikumpulkan di level 1
    public void CollectClue(int levelIndex)
    {
        if (levelIndex == 1)
        {
            PlayerPrefs.SetInt("Level_1_ClueCollected", 1);
            PlayerPrefs.Save();
            SetupLevelButtons();  // Update tombol agar Level 2 bisa diakses
        }
    }
}
