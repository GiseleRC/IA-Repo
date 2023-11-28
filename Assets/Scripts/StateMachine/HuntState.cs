using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HuntState : IState
{
    private FiniteStateMachine _FSM;
    private NewEnemy _this;

    public HuntState(FiniteStateMachine FSM, NewEnemy enemy)
    {
        _FSM = FSM;
        _this = enemy;
    }
    public void OnStart()
    {
        _this.PathToFollow = GameManager.instance.Pathfinding.AStar(_this.closeNode, GameManager.instance.player.closeNode);
        Debug.Log("Hunt");
    }

    public void OnUpdate()
    {
        if (_this.PathToFollow.Count > 0) _this.FollowPath();
        else _FSM.ChangeState(States.ReturnPatrol);

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
                    enemy.ChangeState(States.Hunt);
                }
            }

            _FSM.ChangeState(States.Chase);
        }
    }
}