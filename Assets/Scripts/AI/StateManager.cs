using System;
using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.AI;
using UnityEngine;

public class StateManager : MonoBehaviour
{

    //private HashSet<State> _states;
    private Dictionary<EState, State> _states;
    [SerializeField]private State _currentState;

	protected Enemy _thisEnemy;


    //private readonly bool _isRanged;
	void Start()
    {
        //_isRanged = isRanged;
        InitialiseFields();
    }

    private void InitialiseFields()
    {
		var attack = gameObject.AddComponent<Attack>();
		var approach = gameObject.AddComponent<Approach>();
        var flee = gameObject.AddComponent<Flee>();
		_states = new Dictionary<EState, State>
		{
			{ EState.APPROACH,  approach},
			{ EState.ATTACK,    attack},
			{ EState.FLEE,      flee}
		};
        foreach (var state in _states)
        {
            state.Value.SetFields();
        }
		//_states = new HashSet<State>{approach, flee, attack};
		_currentState = _states[EState.APPROACH];
		_thisEnemy = gameObject.GetComponent<Enemy>();

	}

    // Update is called once per frame
    private void Update ()
    {
		EState newState = _currentState.Execute();
	    _currentState = _states[newState];
    }
	/*
    private void UpdateCurrentState()
    {

		var newState = _currentState;
        var maxRelevance = 0.0;
        foreach (var state in _states)
        {
            if (state._thisEnemy == null) state._thisEnemy = _thisEnemy;

            var stateRelevance = state.Relevance(_thisEnemy.distance);
            if (!(stateRelevance > maxRelevance)) continue;
            maxRelevance = stateRelevance;
            newState = state;
        }
        _currentState = newState;
    }
	*/
}
