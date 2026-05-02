using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{

    public void LoadGame()
    {
        SceneManager.LoadScene("Game");
    }

    // ฟังก์ชันกลับหน้าเมนูหลัก
    public void LoadMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

}

