using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReturnPatrolState : IState
{
    private FiniteStateMachine _FSM;
    private NewEnemy _this;
    private PatrolState _patrolState;

    public ReturnPatrolState(FiniteStateMachine FSM, NewEnemy enemy, PatrolState patrolState)
    {
        _FSM = FSM;
        _this = enemy;
        _patrolState = patrolState;
    }

    public void OnStart()
    {
        _this.PathToFollow = GameManager.instance.Pathfinding.AStar(_this.closeNode, _patrolState.CurrentWP);
        Debug.Log("Retorno a Patrol");
    }

    public void OnUpdate()
    {
        if (_this.PathToFollow.Count > 0) _this.FollowPath();
        else _FSM.ChangeState(States.Patrol);

        GotTarget();
    }

    public void OnExit()
    {
        _this.PathToFollow.Clear();
    }

    private void GotTarget()
    {
        if (_this.InFOV(GameManager.instance.player))
        {
            foreach (var enemy in GameManager.instance.AllEnemies)
            {
                if (enemy != _this)
                {
                    if (_FSM.currentState != States.Chase && _FSM.currentState != States.Hunt)
                    {
                        enemy.ChangeState(States.Hunt);    
                    }
                }
            }

            _FSM.ChangeState(States.Chase);
        }
    }
}