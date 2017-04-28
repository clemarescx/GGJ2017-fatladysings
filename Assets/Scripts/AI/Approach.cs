using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.AI;
using UnityEngine;

public class Approach : State
{
    public override EState Execute()
    {
        if (!_thisEnemy.IsWithinArena)
        {
            _thisEnemy.Approach();
            return EState.APPROACH;
        }
        if (_thisEnemy.Distance > _thisEnemy.AvgAttackRange && !_thisEnemy.IsTooWeak)
        {
            _thisEnemy.Approach();
            return EState.APPROACH;
        }
        if (_thisEnemy.Distance < _thisEnemy.MinAttackRange || _thisEnemy.IsTooWeak)
        {
//            _thisEnemy.Flee();
            return EState.FLEE;
        }
        if (_thisEnemy.IsWithinAttackRange)
        {
//            _thisEnemy.Attack();
            return EState.ATTACK;
        }
        _thisEnemy.Approach();
        return EState.APPROACH;
    }

    /*
    public override double Relevance(double distance)
    {
            double _relevance = 0.5;
            if (distance < _thisEnemy.MaxAttackRange)
            {
                _relevance = 0;
            }

            return _relevance;
    }
    */
}