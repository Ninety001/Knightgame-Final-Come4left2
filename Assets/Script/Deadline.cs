using UnityEngine;

public class Deadline : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            if (GameManager.instance != null)
            {
                GameManager.instance.TakeDamage(1);
            }

            Destroy(other.gameObject);
        }
    }
}