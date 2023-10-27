using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class BoardFunc : MonoBehaviour
{
    private Camera _mainCam;
    [SerializeField] private LayerMask _roadMask;
    [SerializeField] private CinemachineImpulseSource cis;
    [SerializeField] private float _limitZ;
    private Vector3 dir;

    private bool _isRoll = false;
    public bool isHit = false;

    private void Awake()
    {
        _mainCam = Camera.main;
    }

    private void Start()
    {
        cis.m_DefaultVelocity = Vector3.one * 0.1f;
    }

    private void MoveSkateBoard()
    {
        Ray ray = _mainCam.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Input.GetMouseButton(0) && Physics.Raycast(ray, out hit, 100, _roadMask))
        {
            Vector3 pos = new Vector3(transform.position.x, transform.position.y, hit.point.z);
            if (pos.z <= -_limitZ || pos.z >= _limitZ)
            {
                pos = transform.position;
            }
            dir = (pos - transform.position).normalized;
            transform.position = pos;
        }
    }

    private void Rollick(Vector3 pos)
    {
        isHit = true;
        if (dir.z == 0) return;
        cis.GenerateImpulse();
        ScoreManager.Instance.AddScore(5000);
        ScoreManager.Instance.LookToAddScore(5000, pos);
        _isRoll = true;
    }

    private void Update()
    {
        MoveSkateBoard();
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Obstacle") && !_isRoll && !CompareTag("Range"))
        {
            Vector3 pos = other.ClosestPointOnBounds(transform.position);
            Rollick(pos);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Obstacle") && _isRoll && !CompareTag("Range"))
        {
            _isRoll = false;
            isHit = false;
        }
    }
}
