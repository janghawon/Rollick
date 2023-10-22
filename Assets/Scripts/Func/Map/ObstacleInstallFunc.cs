using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleInstallFunc : MonoBehaviour
{
    Dictionary<Transform, List<Transform>> _obstacleData = new Dictionary<Transform, List<Transform>>();
    [SerializeField] private List<Transform> _obstaclesPrefab = new List<Transform>();
    [SerializeField] [Range(12, 52)] private int _minPaddingDistance;
    [SerializeField] [Range(12, 52)] private int _maxPaddingDistance;

    [SerializeField]private Vector3[] _saveObstacle = new Vector3[2] { Vector3.zero, Vector3.zero };

    private List<Transform> CollocateObstacle(Transform obsTrm)
    {
        _saveObstacle = new Vector3[2] { Vector3.zero, Vector3.zero };
        List<Transform> setObstacleData = new List<Transform>();

        for(int i = 0; i < Random.Range(8, 16); i++)
        {
            int idx = Random.Range(0, _obstaclesPrefab.Count);
            Transform obs = Instantiate(_obstaclesPrefab[idx], obsTrm);

            idx = Random.Range(_minPaddingDistance, _maxPaddingDistance + 1);
            int set = i % 2 == 0 ? 1 : -1;

            obs.rotation = Quaternion.Euler(0, -90 * set, 0);
            Debug.Log(_saveObstacle[i % 2]);
            obs.position += new Vector3(_saveObstacle[i % 2].x += idx, 0, 0);
            //obs.position = _saveObstacle[i % 2];
            obs.position += new Vector3(0, 0, 3 * set);// 홀수 = 오른쪽
            
            setObstacleData.Add(obs);
        }

        return setObstacleData;
    }

    public void InstallObstacle(Transform parent)
    {
        Transform obsTrm = parent.Find("Obstacle");
        if(!_obstacleData.ContainsKey(obsTrm))
        {
            _obstacleData.Add(obsTrm, CollocateObstacle(obsTrm));
            return;
        }
        foreach(Transform t in _obstacleData[obsTrm])
        {
            Destroy(t.gameObject); // PoolManager로 교체 예정
        }
        _obstacleData[obsTrm] = CollocateObstacle(obsTrm);
    }
}
