using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FiniteStateMachine
{
    private IState _currentState;
    private Dictionary<States, IState> _allStates = new Dictionary<States, IState>();

    public void Update()
    {
        _currentState.OnUpdate();
    }

    public void ChangeState(States state)
    {
        if (!_allStates.ContainsKey(state)) return;
        if (_currentState != null) _currentState.OnExit();
        _currentState = _allStates[state];
        _currentState.OnStart();
    }

    public void AddState(States key, IState value)
    {
        if (!_allStates.ContainsKey(key)) _allStates.Add(key, value);
        else _allStates[key] = value;
    }

    //internal void AddState(States hunt1, HuntState hunt2)
    //{
    //    throw new NotImplementedException();
    //}
}