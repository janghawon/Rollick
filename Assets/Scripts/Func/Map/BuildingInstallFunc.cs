using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class BuildingElement
{
    public Transform building; // ���� ������
    public float nextBuildingPosIdx; // ���� ������ �������� �ϴ� ����
}

public class BuildingInstallFunc : MonoBehaviour
{
    public List<BuildingElement> buildingList = new List<BuildingElement>();
    private List<BuildingElement> _buildings = new List<BuildingElement>();
    private BuildingElement[] _saveBuilding = new BuildingElement[2];

    private float[] _positionValue = new float[2];

    private Dictionary<Transform, List<Transform>> _loadData = new Dictionary<Transform, List<Transform>>();

    private void CombineBuilding(Transform load, Transform buildingTrans) // ���ο� ��ġ�� �ǹ� ����
    {
        if(!_loadData.ContainsKey(load))
        {
            List<Transform> buildingDatas = new List<Transform>();
            buildingDatas.Add(buildingTrans);
            _loadData.Add(load, buildingDatas);
            return;
        }

        _loadData[load].Add(buildingTrans);
    }

    public void ResetBuilding(Transform load) // ���ο� ��ġ�� ���� �ʱ�ȭ
    {
        if (!_loadData.ContainsKey(load)) return;

        foreach(Transform t in _loadData[load])
        {
            Destroy(t.gameObject); //PoolManager�� ��ü ����
        }
        _loadData[load].Clear();
    }

    public void RandomBuild(Transform parent) // ���ο� �ǹ� �������� ��ġ
    {
        _saveBuilding = null;
        _positionValue = new float[2] { 0, 0 };
        _buildings.Clear();
        _saveBuilding = new BuildingElement[2] { null, null };

        for (int i = 0; i < buildingList.Count; i++)
            _buildings.Add(buildingList[i]);

        for (int i = 0; i < 8; i++)
        {
            int divideRL = i % 2;
            int idx = Random.Range(0, _buildings.Count);
            BuildingElement select = _buildings[idx];

            Transform selectTrm = Instantiate(select.building, parent);

            if (_saveBuilding[divideRL] != null)
            {
                _positionValue[divideRL] += _saveBuilding[divideRL].nextBuildingPosIdx;

                _buildings.Add(_saveBuilding[divideRL]);
            }
            selectTrm.position += new Vector3(_positionValue[divideRL], 0, 31 * (divideRL));

            foreach (Transform child in selectTrm)
            {
                child.rotation = Quaternion.Euler(0, 180 * (divideRL), 0);
            }

            _saveBuilding[divideRL] = _buildings[idx];
            _buildings.RemoveAt(idx);
            CombineBuilding(parent, selectTrm);
        }

    }
}
