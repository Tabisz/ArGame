using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;
using UnityEngine.XR.ARFoundation;

public class Tower : MonoBehaviour
{
    Text text;

    public int maxEnemiesCount = 10;
    public int currentEnemiesCount;

    [SerializeField]
    GameObject enemyPrefab;


    public Vector2 spawningArea;
    public Transform spawnHeight;

    public Transform enemyTarget;
    
    

    void Start()
    {
        text = GameObject.Find("EnemysCount").GetComponent<Text>();
        SetEnemiesCountText();

        currentEnemiesCount = 0;
        StartCoroutine(SpawnEnemysCoroutine());
    }


    private void SetEnemiesCountText()
    {
        text.text ="Current enemies count:"+ currentEnemiesCount.ToString();
    }

    IEnumerator SpawnEnemysCoroutine()
    {
        while(true)
        {
            if(currentEnemiesCount < maxEnemiesCount)
            {
                EnemyScript enemy = Instantiate(enemyPrefab, Vector3.zero, Quaternion.identity, this.transform.GetChild(0)).GetComponent<EnemyScript>();
                enemy.transform.localPosition = GetSpawnPosition();
                enemy.Init(this);
                currentEnemiesCount++;
                SetEnemiesCountText();
            }
                yield return new WaitForSeconds(.5f);

        }
    }

    private Vector3 GetSpawnPosition()
    {
        return new Vector3(Random.Range(spawningArea.x, -spawningArea.x), spawnHeight.localPosition.y, Random.Range(spawningArea.y, -spawningArea.y));
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position, new Vector3(spawningArea.x*2, .1f, spawningArea.y*2));
    }

    private void OnDestroy()
    {
        StopAllCoroutines();
    }
}
