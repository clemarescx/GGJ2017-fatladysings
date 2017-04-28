using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class WaveInfo : MonoBehaviour
{
    private WaveControllerScript _waveControllerScript;
    public Text CurrentWaveNumber;
    public Text NumberOfEnemiesLeft;
    public Text NumberOfEnemiesKilled;

    // Use this for initialization
    void Start()
    {
        _waveControllerScript = gameObject.GetComponent<WaveControllerScript>();
    }

    // Update is called once per frame
    void Update()
    {
        NumberOfEnemiesKilled.text = "Total enemies killed: " + _waveControllerScript.DefeatedEnemiesInTotal;
        NumberOfEnemiesLeft.text = "Enemies left in wave: " + _waveControllerScript.RemainingEnemiesInWave;
        CurrentWaveNumber.text = "Current wave " + _waveControllerScript.CurrentWave;
    }
}