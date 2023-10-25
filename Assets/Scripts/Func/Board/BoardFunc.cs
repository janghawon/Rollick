using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardFunc : MonoBehaviour
{
    private Camera _mainCam;
    [SerializeField] private LayerMask _roadMask;
    private Vector3 dir;

    private bool _isRoll = false;

    private void Awake()
    {
        _mainCam = Camera.main;
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
        if (dir.z == 0) return;

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
        }
    }
}
