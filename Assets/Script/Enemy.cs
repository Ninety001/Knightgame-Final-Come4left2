using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float moveSpeed = 2f;

    void Update()
    {
        // เดินไปทางซ้าย (-X) อย่างเดียว
        transform.Translate(Vector2.left * moveSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // ถ้าชนกับวัตถุที่มี Tag ว่า "Projectile" (ลูกปืนใหญ่)
        if (other.CompareTag("Projectile"))
        {

            Destroy(other.gameObject);
            Destroy(this.gameObject);

            Debug.Log("ศัตรูถูกกำจัดแล้ว!");
        }
    }
}