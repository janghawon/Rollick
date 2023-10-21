using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class MapMoveFunc : MonoBehaviour
{
    [SerializeField] private Transform _mapTrans;
    [SerializeField] private Transform _cityLoadPrefab;
    [SerializeField] private Transform[] _maps = new Transform[2];
    [SerializeField] private float _limitPos;
    [SerializeField] private float _mapLength;

    public float _moveSpeed;

    private Transform _currentShowMap;
    private int _mapIdx = 0;

    BuildingInstallFunc _buildingInstallFunc;

    private void Awake()
    {
        _buildingInstallFunc = GetComponent<BuildingInstallFunc>();
        for(int i = 0; i < _maps.Length; i++)
        {
            _maps[i] = Instantiate(_cityLoadPrefab, _mapTrans);
            _buildingInstallFunc.RandomBuild(_maps[i]);
            if (i == 0)
            {
                _maps[i].position = Vector3.zero;
                continue;
            }
            _maps[i].position = _maps[i-1].position + new Vector3(_mapLength, 0, 0);
        }

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

            _buildingInstallFunc.ResetBuilding(_currentShowMap);
            _buildingInstallFunc.RandomBuild(_currentShowMap);

            _mapIdx++;
            if (_mapIdx == _maps.Length)
                _mapIdx = 0;
            _currentShowMap = _maps[_mapIdx];
        }
    }
}
