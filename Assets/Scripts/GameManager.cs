using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public Player player;
    public NodeGrid nodeGrid;
    private Pathfinding _pathfinding = new Pathfinding();
    public List<NewEnemy> _allEnemies = new List<NewEnemy>();

    private void Awake()
    {
        if (instance == null) instance = this;
        else Destroy(this.gameObject);
    }

    public Pathfinding Pathfinding { get { return _pathfinding; } }
    public List<NewEnemy> AllEnemies { get { return _allEnemies; } }
}