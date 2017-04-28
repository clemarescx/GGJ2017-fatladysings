using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.AI;
using UnityEngine;

public class  Attack : State
{

	public override EState Execute()
	{
	    if (!_thisEnemy.IsWithinArena)
	    {
	        _thisEnemy.Approach();
	        return EState.APPROACH;
	    }
            if (_thisEnemy.IsWithinAttackRange && !_thisEnemy.IsTooWeak)
	    {
            _thisEnemy.Attack();
            return EState.ATTACK;
	    }
	    if (_thisEnemy.IsTooClose || _thisEnemy.IsTooWeak)
	    {
//	        _thisEnemy.Flee(); //TODO: find out whether to call this here or not
			return EState.FLEE;
	    }
	    if (_thisEnemy.IsTooFarAway/* && _thisEnemy.HealthRatio > 0.5*/)
	    {
//	        _thisEnemy.Approach();
			return EState.APPROACH;
	    }
	    return EState.ATTACK;
	}


    /*
	public override double Relevance(double distance) {
		double relevance = 0.0;
		if (distance <= _thisEnemy.MaxAttackRange && distance >= _thisEnemy.MinAttackRange) {
			relevance = 0.7;
		}

		return relevance;
	}

	*/
}
