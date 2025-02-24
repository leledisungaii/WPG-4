using UnityEngine;
using UnityEngine.SceneManagement;

public class GameSaver : MonoBehaviour
{
    public void SaveGame()
    {
        // Menyimpan nama scene aktif
        PlayerPrefs.SetString("SavedScene", SceneManager.GetActiveScene().name);
        
        // Menyimpan posisi pemain sebagai contoh
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            PlayerPrefs.SetFloat("PlayerX", player.transform.position.x);
            PlayerPrefs.SetFloat("PlayerY", player.transform.position.y);
            PlayerPrefs.SetFloat("PlayerZ", player.transform.position.z);
        }

        PlayerPrefs.Save();
    }

    public void LoadGame()
    {
        // Memuat nama scene yang tersimpan
        string sceneName = PlayerPrefs.GetString("SavedScene");
        if (!string.IsNullOrEmpty(sceneName))
        {
            SceneManager.LoadScene(sceneName);
        }

        // Memuat posisi pemain jika ada
        if (PlayerPrefs.HasKey("PlayerX"))
        {
            float x = PlayerPrefs.GetFloat("PlayerX");
            float y = PlayerPrefs.GetFloat("PlayerY");
            float z = PlayerPrefs.GetFloat("PlayerZ");

            GameObject player = GameObject.FindGameObjectWithTag("Player");
            if (player != null)
            {
                player.transform.position = new Vector3(x, y, z);
            }
        }
    }
}
