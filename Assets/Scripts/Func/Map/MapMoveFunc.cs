using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class MapMoveFunc : MonoBehaviour
{
    [SerializeField] private Transform[] _maps = new Transform[2];
    [SerializeField] private float _limitPos;
    [SerializeField] private float _mapLength;

    public float _moveSpeed;

    private Transform _currentShowMap;
    private int _mapIdx = 0;

    private void Start()
    {
        _currentShowMap = _maps[_mapIdx];
    }

    private void Update()
    {
        for(int i = 0; i < _maps.Length; i++)
        {
            _maps[i].Translate(Vector3.left * _moveSpeed * Time.deltaTime);
        }
        if(_currentShowMap.transform.position.x <= _limitPos)
        {
            _currentShowMap.transform.position += new Vector3(_mapLength * 2, 0, 0);
            _mapIdx++;
            if (_mapIdx == _maps.Length)
                _mapIdx = 0;
            _currentShowMap = _maps[_mapIdx];
        }
    }
}
