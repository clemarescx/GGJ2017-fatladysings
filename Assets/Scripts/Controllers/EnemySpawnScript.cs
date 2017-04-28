using System.Collections;
using System.Collections.Generic;
using Medallion;
using UnityEngine;

public class EnemySpawnScript : MonoBehaviour
{

    [SerializeField] private GameObject _enemyMelee;
    [SerializeField] private GameObject _enemyRanged;
    private GameObject[] _enemyArray;
    private GameObject[] _spawnZones;
//    public int NumberOfEnemies { get; set; }
//    [SerializeField]

//    private int _maxNumberOfEnemies = 20;

//    [SerializeField] private float _spawnInterval = 1f;

	// Use this for initialization
	public void SetVariables ()
	{
	    _spawnZones = GameObject.FindGameObjectsWithTag("SpawnZone");
	    _enemyArray = new[] {_enemyMelee, _enemyRanged};
//	    SpawnRepeating();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void Spawn()
    {
        var spawnZone = _spawnZones[Rand.Next(0, 4)].transform;
        var rndPosWithin = new Vector3(Random.Range(-1f, 1f), 0, Random.Range(-1f, 1f));
        rndPosWithin = spawnZone.transform.TransformPoint(rndPosWithin * 4);
        var enemy = Instantiate(_enemyArray[Rand.Next(0, 2)], new Vector3(rndPosWithin.x,
            GameObject.FindGameObjectWithTag("Player").transform.position.y, rndPosWithin.z), new Quaternion());
        enemy.transform.SetParent(transform);
//        NumberOfEnemies++;
    }

    private void SpawnRepeating()
    {
//        InvokeRepeating("Spawn", 0, _spawnInterval);
    }
}
