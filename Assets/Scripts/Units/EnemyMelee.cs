using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMelee : Enemy
{

    private MeeleeDamageScript _damageScript;

    private SphereCollider _collider;
    new void Start()
    {
        base.Start();
        _damageScript =
            transform.FindChild("joint1").FindChild("Rake").FindChild("AttackHitBox").GetComponent<MeeleeDamageScript>();
        _damageScript.Damage = DamageOutput;

        _collider = _damageScript.gameObject.GetComponent<SphereCollider>();
        _collider.enabled = false;
    }

    protected override void SetAttackRanges()
    {
        MaxAttackRange = 5;
        MinAttackRange = 0.5;
    }

    // Update is called once per frame
	void Update () {
		/*if(Distance < 4)
			InvokeRepeating("Decay", 0.5f, 0.5f);*/


	}

    public override void Attack()
    {
        base.Attack();
        StartCoroutine(AttackHitBoxChange());
    }

    void Decay()
	{
		CurrentHealth--;
	}

    private IEnumerator AttackHitBoxChange()
    {
        yield return new WaitForSeconds(0.4f);
        _collider.enabled = true;
        yield return new WaitForSeconds(0.6f);
        _collider.enabled = false;
    }
}
