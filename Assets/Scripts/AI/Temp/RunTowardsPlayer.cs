using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunTowardsPlayer : MonoBehaviour
{
    private Enemy _stats;
    private CharacterController _cc;
    private Transform _target;

	// Use this for initialization
	void Start () {
	    _cc = GetComponent<CharacterController>();
	    _stats = GetComponent<Enemy>();
	    _target = GameObject.FindGameObjectWithTag("Player").transform;
	}
	
	// Update is called once per frame
	void Update ()
	{
	    _cc.Move((_target.position - transform.position).normalized * _stats.MovementSpeed/50);
	}
}
