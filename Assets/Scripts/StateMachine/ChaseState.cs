using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaseState :  IState
{
    private NewEnemy _this;
    private FiniteStateMachine _FMS;
    private float _speed;

    public ChaseState(FiniteStateMachine FMS, NewEnemy enemy, float speed)
    {
        _FMS = FMS;
        _this = enemy;
        _speed = speed;
    }

    public void OnStart()
    {
        Debug.Log("Entr� a Chase");
    }

    public void OnUpdate()
    {
        Chase();
        NoTarget();
    }
    public void OnExit()
    {
        Debug.Log("Sal� de Chase");
    }

    private void Chase()
    {
        var dir = GameManager.instance.player.transform.position - _this.transform.position;
        _this.MoveToDir(dir);
    }

    private void NoTarget()
    {
        if (!_this.InFOV(GameManager.instance.player))
        {
            _FMS.ChangeState(States.ReturnPatrol);
        }
    }
}