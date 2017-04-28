using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArenaTrigger : MonoBehaviour {
    private void OnTriggerEnter(Collider other)
    {
        if (!other.transform.CompareTag("Enemy")) return;
        var enemy = other.GetComponent<Enemy>();
        enemy.IsWithinArena = true;
    }

    private void OnTriggerExit(Collider other)
    {
        if (!other.transform.CompareTag("Enemy")) return;
        var enemy = other.GetComponent<Enemy>();
        enemy.IsWithinArena = false;
    }
}
