using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Entity
{
    [SerializeField] float _playerSpeed;

    private void Start()
    {
        GameManager.instance.player = this;
    }

    private void Update()
    {
        var h = Input.GetAxisRaw("Horizontal");
        var v = Input.GetAxisRaw("Vertical");

        if (h != 0 || v != 0)
        {
            transform.position += new Vector3(h, 0, v) * _playerSpeed * Time.deltaTime;
        }

        CloseNode();
    }
}