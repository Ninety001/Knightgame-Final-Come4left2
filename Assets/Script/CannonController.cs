using UnityEngine;

public class CannonController : MonoBehaviour
{
    [Header("Cannon Setup")]
    public Transform barrel;
    public Transform shootPoint;
    public Transform controlPoint;
    public GameObject ballPrefab;
    public float rotateSpeed = 100f;
    public float fireForce = 20f;

    private float angle = 0f;
    private PlayerController currentPlayer;

    // ระบบตรวจจับแบบใหม่ (ช่วยให้กด E ติดง่ายขึ้น)
    private bool isPlayerNearby = false;
    private PlayerController nearbyPlayer;

    void Update()
    {
        // 1. เช็คการกด E ใน Update (แม่นยำกว่าใน TriggerStay)
        if (isPlayerNearby && nearbyPlayer != null && Input.GetKeyDown(KeyCode.E))
        {
            HandleInteraction();
        }

        // 2. ระบบควบคุมปืนใหญ่ (ทำงานเมื่อมีคนคุม)
        if (currentPlayer != null && currentPlayer.isControllingCannon)
        {
            currentPlayer.transform.position = controlPoint.position;

            if (Input.GetKey(KeyCode.W)) angle += rotateSpeed * Time.deltaTime;
            else if (Input.GetKey(KeyCode.S)) angle -= rotateSpeed * Time.deltaTime;

            angle = Mathf.Clamp(angle, -10f, 60f);
            barrel.rotation = Quaternion.Euler(0, 0, angle);

            if (Input.GetKeyDown(KeyCode.Space)) Fire();
        }
    }

    void HandleInteraction()
    {
        // กรณีเป็นกองกระสุน
        if (gameObject.CompareTag("AmmoStack"))
        {
            if (!nearbyPlayer.hasAmmo && !nearbyPlayer.isControllingCannon)
            {
                nearbyPlayer.SetAmmo(true);
            }
        }
        // กรณีเป็นปืนใหญ่
        else if (gameObject.CompareTag("Cannon"))
        {
            if (nearbyPlayer.isControllingCannon)
            {
                nearbyPlayer.isControllingCannon = false;
                currentPlayer = null;
            }
            else if (nearbyPlayer.hasAmmo)
            {
                nearbyPlayer.SetAmmo(false);
                nearbyPlayer.isControllingCannon = true;
                currentPlayer = nearbyPlayer;

                // ล็อคตำแหน่ง (เฉพาะปืนใหญ่ที่ต้องมี ControlPoint)
                if (controlPoint != null)
                    nearbyPlayer.transform.position = controlPoint.position;
            }
        }
    }

    void Fire()
    {
        GameObject ball = Instantiate(ballPrefab, shootPoint.position, shootPoint.rotation);
        ball.GetComponent<Rigidbody2D>().AddForce(shootPoint.right * fireForce, ForceMode2D.Impulse);

        currentPlayer.isControllingCannon = false;
        currentPlayer = null;
    }

    // เก็บค่าผู้เล่นเมื่อเดินเข้าเขต
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNearby = true;
            nearbyPlayer = other.GetComponent<PlayerController>();
        }
    }

    // ล้างค่าเมื่อเดินออกจากเขต
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNearby = false;
            nearbyPlayer = null;
        }
    }
}