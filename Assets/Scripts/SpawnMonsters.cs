using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterSpawner : MonoBehaviour
{
    // Prefab của quái vật
    public GameObject monsterPrefab;

    // Khu vực spawn (hình chữ nhật trong 2D)
    public Vector2 spawnAreaCenter;
    public Vector2 spawnAreaSize;

    // Số lượng quái muốn spawn
    public int numberOfMonsters = 5;

    void Start()
    {
        SpawnMonsters();
    }

    void SpawnMonsters()
    {
        for (int i = 0; i < numberOfMonsters; i++)
        {
            // Tạo vị trí ngẫu nhiên trong khu vực spawn
            Vector2 randomPosition = GetRandomPositionInArea();

            // Spawn quái vật tại vị trí ngẫu nhiên
            Instantiate(monsterPrefab, randomPosition, Quaternion.identity);
        }
    }

    Vector2 GetRandomPositionInArea()
    {
        // Tính toán một vị trí ngẫu nhiên bên trong khu vực 2D
        float x = Random.Range(spawnAreaCenter.x - spawnAreaSize.x / 2, spawnAreaCenter.x + spawnAreaSize.x / 2);
        float y = Random.Range(spawnAreaCenter.y - spawnAreaSize.y / 2, spawnAreaCenter.y + spawnAreaSize.y / 2);

        return new Vector2(x, y);
    }

    // Vẽ khu vực spawn trong cửa sổ Scene để dễ dàng điều chỉnh
    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(spawnAreaCenter, spawnAreaSize);
    }
}
