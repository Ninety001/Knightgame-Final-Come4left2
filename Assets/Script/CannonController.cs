using UnityEngine;

public class CannonController : MonoBehaviour
{
    [Header("Cannon Setup")]
    public Transform barrel;      // ลำกล้อง
    public Transform shootPoint;  // จุดยิง (เหมือนปากกระบอก)
    public Transform controlPoint; // จุดที่ผู้เล่นจะไปยืนล็อคตำแหน่ง
    public GameObject ballPrefab; // Prefab ลูกปืนที่จะพุ่งออกไป
    public float rotateSpeed = 100f;
    public float fireForce = 20f;

    private float angle = 0f;
    private PlayerController currentPlayer;

    void Update()
    {
        // ทำงานเมื่อมีผู้เล่นคุมปืนอยู่เท่านั้น
        if (currentPlayer != null && currentPlayer.isControllingCannon)
        {
            // ล็อคตำแหน่งผู้เล่นให้ติดกับ Control Point
            currentPlayer.transform.position = controlPoint.position;

            // 1. ระบบหมุนปืน (W/S) รอบแกน Z
            if (Input.GetKey(KeyCode.W)) angle += rotateSpeed * Time.deltaTime;
            else if (Input.GetKey(KeyCode.S)) angle -= rotateSpeed * Time.deltaTime;

            angle = Mathf.Clamp(angle, -10f, 60f); // จำกัดมุมยิง
            barrel.rotation = Quaternion.Euler(0, 0, angle);

            // 2. ระบบยิง (Space)
            if (Input.GetKeyDown(KeyCode.Space))
            {
                Fire();
            }
        }
    }

    void Fire()
    {
        // สร้างลูกปืนและยิงออกไปตามแรง F = ma
        GameObject ball = Instantiate(ballPrefab, shootPoint.position, shootPoint.rotation);
        Rigidbody2D ballRb = ball.GetComponent<Rigidbody2D>();
        ballRb.AddForce(shootPoint.right * fireForce, ForceMode2D.Impulse);

        // ยิงเสร็จแล้วปล่อยการควบคุม
        currentPlayer.isControllingCannon = false;
        currentPlayer = null;
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Player") && Input.GetKeyDown(KeyCode.E))
        {
            PlayerController pc = other.GetComponent<PlayerController>();

            // กรณีชนกองกระสุน: หยิบกระสุน
            if (gameObject.CompareTag("AmmoStack"))
            {
                if (!pc.hasAmmo && !pc.isControllingCannon)
                {
                    pc.SetAmmo(true);
                }
            }
            // กรณีชนปืนใหญ่: โหลดกระสุน/เข้าคุม/ออกจากการคุม
            else if (gameObject.CompareTag("Cannon"))
            {
                if (pc.isControllingCannon)
                {
                    pc.isControllingCannon = false;
                    currentPlayer = null;
                }
                else if (pc.hasAmmo)
                {
                    pc.SetAmmo(false);
                    pc.isControllingCannon = true;
                    currentPlayer = pc;

                    // วาร์ปผู้เล่นไปที่จุดประจำตำแหน่งทันที
                    pc.transform.position = controlPoint.position;
                }
            }
        }
    }
}