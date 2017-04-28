using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.AI;
using UnityEngine;

public abstract class State : MonoBehaviour
{
	//public abstract double Relevance(double distance); 
	public abstract EState Execute();

	[SerializeField]protected Transform playerTarget;
	[SerializeField] public Enemy _thisEnemy { get; set; }



    public void SetFields()
    {
		playerTarget = GameObject.FindGameObjectWithTag("Player")
			.transform;
		_thisEnemy = gameObject.GetComponent<Enemy>();
    }
}
