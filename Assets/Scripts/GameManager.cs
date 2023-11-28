using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public Player player;
    public NodeGrid nodeGrid;
    public List<NewEnemy> _allEnemies = new List<NewEnemy>();
    
    private Pathfinding _pathfinding = new Pathfinding();

    private void Awake()
    {
        if (instance == null) instance = this;
        else Destroy(this.gameObject);
    }

    public Pathfinding Pathfinding { get { return _pathfinding; } }
    public List<NewEnemy> AllEnemies { get { return _allEnemies; } }
}