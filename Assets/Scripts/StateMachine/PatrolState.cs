using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolState : IState
{
    private int _currentWP = 0;
    private Node[] _waypoints;
    private NewEnemy _this;
    private FiniteStateMachine _FSM;
    private float _speed;

    public PatrolState(FiniteStateMachine FSM, Node[] waypoints, NewEnemy enemy, float speed)
    {
        _this = enemy;
        _waypoints = waypoints;
        _FSM = FSM;
        _speed = speed;
    }

    public void OnStart()
    {
        Debug.Log("Entre a Patrol");
    }

    public void OnUpdate()
    {
        Patrol();
        GotTarget();
    }

    public void OnExit()
    {
        Debug.Log("Sal? de Patrol");
    }

    private void Patrol()
    {
        var waypoint = _waypoints[_currentWP];
        Vector3 dir = waypoint.transform.position - _this.transform.position;
        _this.MoveToDir(dir);

        if (dir.magnitude <= 0.5f)
        {
            _currentWP++;
            if (_currentWP > _waypoints.Length - 1) _currentWP = 0;
        }
    }

    private void GotTarget()
    {
        if (_this.InFOV(GameManager.instance.player))
        {
            foreach (var enemy in GameManager.instance.AllEnemies)
            {
                if (enemy != _this)
                {
                    enemy.ChangeState(States.Hunt);
                }
            }

            _FSM.ChangeState(States.Chase);
        }
    }

    public Node CurrentWP { get { return _waypoints[_currentWP]; } }
}