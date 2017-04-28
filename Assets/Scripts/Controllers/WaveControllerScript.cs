using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveControllerScript : MonoBehaviour {

    public bool ScreenIsFull { get { return ActiveEnemies >= _maxEnemiesOnScreen; } }
    [SerializeField] private int _maxEnemiesOnScreen = 10;
    [SerializeField] private int _baseEnemyAmount = 5;
    public int EnemiesInWave { get { return _baseEnemyAmount + CurrentWave * 3; } }
    public bool MoreEnemiesLeftToSpawnInWave { get { return EnemiesLeftToSpawn > 0; } }
    public int EnemiesLeftToSpawn { get { return EnemiesInWave - ActiveEnemies + DefeatedEnemiesInWave; } }
    public int RemainingEnemiesInWave { get { return EnemiesInWave - DefeatedEnemiesInWave; } }
    public int ActiveEnemies { get; set; }
    public int DefeatedEnemiesInWave { get; set; }
    public int DefeatedEnemiesInTotal { get; set; }
    public bool WaveDefeated { get { return DefeatedEnemiesInWave >= EnemiesInWave; } }
    public bool TimeHasPassed { get { return _spawnTimer > _enemySpawnRate; } }

    public int CurrentWave { get; set; }

    private float _spawnTimer;

    [SerializeField] private float _enemySpawnRate = 1f;
    [SerializeField] private float _waveOffset = 2f;

    private EnemySpawnScript _spawnScript;

	void Start ()
	{
	    InitializeFields();
	    StartCoroutine(NewWave());
	}

    private IEnumerator NewWave()
    {
        CurrentWave++;
        DefeatedEnemiesInWave = 0;
        ActiveEnemies = 0;
        _spawnTimer = 0;

        while (!WaveDefeated && MoreEnemiesLeftToSpawnInWave)
        {
            if (!ScreenIsFull && TimeHasPassed)
            {
               _spawnScript.Spawn();
                ActiveEnemies++;
                _spawnTimer = 0;
                yield return new WaitForEndOfFrame();
                continue;
            }
            _spawnTimer += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }

        while (ActiveEnemies > 0)
        {
            yield return new WaitForEndOfFrame();
        }

        yield return new WaitForSeconds(_waveOffset);

        StartCoroutine(NewWave());
    }

    private void InitializeFields()
    {
        CurrentWave = 0;
        ActiveEnemies = 0;
        DefeatedEnemiesInTotal = 0;
        _spawnScript = gameObject.GetComponent<EnemySpawnScript>();
        _spawnScript.SetVariables();
    }

    public void DefeatedEnemy()
    {
        ActiveEnemies--;
        DefeatedEnemiesInWave++;
        DefeatedEnemiesInTotal++;
    }
}
