using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.AI;
using UnityEngine;

public class Flee : State {

	public override EState Execute()
    {
        if (!_thisEnemy.IsWithinArena)
        {
            _thisEnemy.Approach();
            return EState.APPROACH;
        }
        if (_thisEnemy.IsTooWeak || _thisEnemy.IsTooClose)
        {
            _thisEnemy.Flee();
            return EState.FLEE;
        }
	    if (_thisEnemy.HasRegainedHealth && _thisEnemy.Distance > _thisEnemy.MaxAttackRange )
	    {
//	        _thisEnemy.Approach();
		    return EState.APPROACH;
	    }
	    if (_thisEnemy.IsWithinAttackRange && !_thisEnemy.IsTooWeak)
	    {
//	        _thisEnemy.Attack();
		    return EState.ATTACK;
	    }

        _thisEnemy.Approach();
        return EState.APPROACH;
    }
	
	/*
	public override double Relevance(double distance)
	{
	    double relevance = 0.0;
		healthRatio = (double) _thisEnemy.CurrentHealth / _thisEnemy.MaxHealth;

		if (distance < _thisEnemy.MinAttackRange || healthRatio < 0.2)
		{
			relevance = 1.0;
		}
		else if (distance > _thisEnemy.MinAttackRange + 2 && healthRatio > 0.5)
		{
			relevance = 0.0;
		}

		return relevance;
	}
	*/
	public void GainHealth()
	{
		_thisEnemy.CurrentHealth++;
	}
}
