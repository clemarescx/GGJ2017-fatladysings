using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeeleeDamageScript : MonoBehaviour {

    public int Damage { get; set; }

    void OnTriggerEnter(Collider other) {
        if (!other.CompareTag("Player")) return;
        var player = other.GetComponent<Player>();
        player.TakeDamage(Damage);
    }
}
