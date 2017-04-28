using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConeDamageScript : MonoBehaviour {

    public int Damage { get; set; }

    public Transform ConeTransform { get; set; }

    public Vector3 Source { get; set; }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            var enemy = other.GetComponent<Enemy>();
            enemy.TakeDamage(Damage);
        }
        var _rb = other.GetComponent<Rigidbody>();
        if (_rb == null) return;
        _rb.AddForce((other.transform.position - Source).normalized * Damage, ForceMode.Impulse);
    }
}