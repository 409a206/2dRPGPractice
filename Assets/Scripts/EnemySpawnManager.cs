using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnManager : MonoBehaviour
{
    //각 소환 간의 시간 간격
    public float spawnTime = 5f;

    //소환 시작 전 대기 시간
    public float spawnDelay = 3f;

    //적 프리팹(prefabs)의 배열
    public GameObject[] enemies;

    private void Start() {
        //임의의 적 생성
        //지정된 대기 시간 후 반복적으로 Spawn 함수를 호출하기 시작
        InvokeRepeating("Spawn", spawnDelay, spawnTime);
    }

    void Spawn() {
        //임의의 적 생성
        int enemyIndex = Random.Range(0, enemies.Length);
        Instantiate(enemies[enemyIndex], transform.position, transform.rotation);
    }
}
