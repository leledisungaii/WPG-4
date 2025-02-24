using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneLoader : MonoBehaviour
{
    // Nama scene yang ingin dimuat
    [SerializeField] private string sceneName;

    private void Start()
    {
        // Menemukan komponen Button dan menambahkan listener untuk memanggil fungsi LoadScene saat tombol ditekan
        Button button = GetComponent<Button>();
        if (button != null)
        {
            button.onClick.AddListener(LoadScene);
        }
        else
        {
            Debug.LogWarning("Komponen Button tidak ditemukan pada GameObject ini.");
        }
    }

    // Fungsi untuk memuat scene yang dituju
    private void LoadScene()
    {
        // Periksa apakah nama scene sudah diisi
        if (!string.IsNullOrEmpty(sceneName))
        {
            SceneManager.LoadScene(sceneName);
        }
        else
        {
            Debug.LogWarning("Nama scene belum diatur. Masukkan nama scene di Inspector.");
        }
    }
}
