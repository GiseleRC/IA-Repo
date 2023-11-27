using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum States
{
    Patrol,
    ReturnPatrol,
    Chase,
    Hunt
}

public class NewEnemy : Entity
{
    [SerializeField] Node[] _waypoints;
    [SerializeField] float _speed;
    [SerializeField] float viewRadius;
    [SerializeField] float viewAngle;
    private List<Node> _pathToFollow = new List<Node>();

    private FiniteStateMachine _FSM = new FiniteStateMachine();

    public LayerMask wallLayer;

    void Start()
    {
        GameManager.instance._allEnemies.Add(this);

        var patrol = new PatrolState(_FSM, _waypoints, this, _speed);
        var chase = new ChaseState(_FSM, this, _speed);
        var returnPatrol = new ReturnPatrolState(_FSM, this, patrol);
        var hunt = new HuntState(_FSM, this);

        _FSM.AddState(States.Patrol, patrol);
        _FSM.AddState(States.Chase, chase);
        _FSM.AddState(States.ReturnPatrol, returnPatrol);
        _FSM.AddState(States.Hunt, hunt);

        _FSM.ChangeState(States.Patrol);
    }

    void Update()
    {
        CloseNode();
        _FSM.Update();
    }

    public bool InFOV(Player obj)
    {
        if (Vector3.Distance(transform.position, obj.transform.position) > viewRadius) return false;
        if (Vector3.Angle(transform.forward, obj.transform.position - transform.position) > (viewAngle / 2)) return false;

        return InLineOfSight(transform.position, obj.transform.position);
    }

    public bool InLineOfSight(Vector3 start, Vector3 end)
    {
        Vector3 dir = end - start;
        return !Physics.Raycast(start, dir, dir.magnitude, wallLayer);
    }

    public void FollowPath()
    {
        Vector3 nextPos = _pathToFollow[0].transform.position;
        Vector3 dir = nextPos - transform.position;
        MoveToDir(dir);

        if (dir.magnitude < 0.1f)
        {
            _pathToFollow.RemoveAt(0);
        }
    }

    public void MoveToDir(Vector3 dir)
    {
        dir = dir.normalized;
        transform.forward = dir;
        transform.position += transform.forward * Time.deltaTime * _speed;
    }

    public Vector3 GetVectorFromAngle(float angle)
    {
        return new Vector3(Mathf.Sin(angle * Mathf.Deg2Rad), 0, Mathf.Cos(angle * Mathf.Deg2Rad));
    }

    public List<Node> PathToFollow { get { return _pathToFollow; } set { _pathToFollow = value; } }

    public void ChangeState(States state)
    {
        _FSM.ChangeState(state);
    }
}