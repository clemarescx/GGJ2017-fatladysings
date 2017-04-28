using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{

    public int BulletDamage { get; set; }
    [SerializeField] private AudioClip _impactSound;
    [SerializeField] private AudioClip _fireSound;
    [SerializeField] [Range(0, 1)] private float _volume = 0.3f;

    private void Start()
    {
        UtilsAudio.PlayClip2D(_fireSound, _volume);
    }

    //Source = _cryConeSpawn.transform.position
    public Vector3 Source { get; set; }

    void OnCollisionEnter(Collision other)
    {

        if (other.gameObject.GetComponent<Unit>())
        {
            other.gameObject.GetComponent<Unit>().TakeDamage(BulletDamage);
            UtilsAudio.PlayClip2D(_impactSound, _volume);
        }
        Destroy(gameObject);
    }
}
