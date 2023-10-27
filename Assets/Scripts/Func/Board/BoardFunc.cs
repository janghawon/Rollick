using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class BoardFunc : MonoBehaviour
{
    private Camera _mainCam;
    [SerializeField] private LayerMask _roadMask;
    [SerializeField] private CinemachineImpulseSource cis;
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
            if (pos.z <= -3.6f || pos.z >= 3.6f)
            {
                pos = transform.position;
            }
            dir = (pos - transform.position).normalized;
            transform.position = pos;
        }
    }

    private void Rollick()
    {
        isHit = true;
        if (dir.z == 0) return;
        cis.GenerateImpulse();
        ScoreManager.Instance.AddScore(5000);
        _isRoll = true;
    }

    private void Update()
    {
        MoveSkateBoard();
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Obstacle") && !_isRoll)
        {
            Rollick();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Obstacle") && _isRoll)
        {
            _isRoll = false;
            isHit = false;
        }
    }
}
