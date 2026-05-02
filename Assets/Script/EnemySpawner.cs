using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab; // ลาก Prefab ศัตรูมาใส่
    public float minSpawnTime = 3f;
    public float maxSpawnTime = 8f;

    private float nextSpawnTime;

    void Start()
    {
        // สุ่มเวลาสำหรับศัตรูตัวแรกทันที
        ScheduleNextSpawn();
    }

    void Update()
    {
        // เมื่อถึงเวลาที่กำหนด
        if (Time.time >= nextSpawnTime)
        {
            SpawnEnemy();
            ScheduleNextSpawn(); // สุ่มเวลารอบถัดไป
        }
    }

    void SpawnEnemy()
    {
        // เกิด ณ ตำแหน่งที่วาง Spawner นั้นๆ ไว้ใน Scene
        Instantiate(enemyPrefab, transform.position, Quaternion.identity);
    }

    void ScheduleNextSpawn()
    {
        // ใช้ Random.Range เพื่อให้เกิดความไม่แน่นอนในแต่ละเลน
        float randomDelay = Random.Range(minSpawnTime, maxSpawnTime);
        nextSpawnTime = Time.time + randomDelay;
    }
}