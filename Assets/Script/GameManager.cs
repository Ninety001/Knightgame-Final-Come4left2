using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public int maxHealth = 4;
    public int currentHealth;

    public Image[] heartIcons;

    void Awake()
    {
        if (instance == null) instance = this;
        else Destroy(gameObject);
    }

    void Start()
    {
        currentHealth = maxHealth;
        Debug.Log("Game Start! Health: " + currentHealth);
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        UpdateHealthUI();

        if (currentHealth <= 0)
        {
            GameOver();
        }
    }

    void UpdateHealthUI()
    {
        // วนลูปเช็คว่าควรเปิดหรือปิดหัวใจดวงไหนบ้าง
        for (int i = 0; i < heartIcons.Length; i++)
        {
            if (i < currentHealth)
            {
                heartIcons[i].enabled = true; // แสดงหัวใจ
            }
            else
            {
                heartIcons[i].enabled = false; // ซ่อนหัวใจ
            }
        }
    }

    public void GameOver()
    {
        SceneManager.LoadScene("GameOver");
    }
}