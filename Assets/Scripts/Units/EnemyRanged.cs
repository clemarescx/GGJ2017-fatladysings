using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRanged : Enemy
{

    [SerializeField] private Rigidbody _bullet;
    [SerializeField] private float _fireRate;
    [SerializeField] private float _velocity = 10;
    private Transform _bulletSpawnPos;
    private float _bulletTimer = 10f;

    public bool TimeHasPassed { get { return _bulletTimer > _fireRate; } }

    new void Start()
    {
        base.Start();
        _bulletSpawnPos = transform.FindChild("BulletSpawn");
        _fireRate = 2;
    }

    protected override void SetAttackRanges()
    {
        MaxAttackRange = 15;
        MinAttackRange = 5;
    }

    public override void Attack()
    {
        LookAtPlayer();
        base.Attack();
        if (TimeHasPassed)
        {
            //reset timer
            _bulletTimer = 0f;

            //instantiate prefab
            var instantiatedProjectile = Instantiate(_bullet.gameObject, _bulletSpawnPos, false);
            instantiatedProjectile.GetComponent<BulletScript>().BulletDamage = DamageOutput;
            instantiatedProjectile.transform.parent = null;

            //Add velocity in TargetDirection
            instantiatedProjectile.GetComponent<Rigidbody>().AddForce(transform.forward * _velocity, ForceMode.Impulse);
        }
        _bulletTimer += Time.deltaTime;
    }

    public void ResetTimer()
    {
        _bulletTimer = _fireRate +1;
    }
}
