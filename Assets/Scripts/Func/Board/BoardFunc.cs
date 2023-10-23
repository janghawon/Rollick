using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class BoardFunc : MonoBehaviour
{
    private Camera _mainCam;
    [SerializeField] private LayerMask _roadMask;
    private float _dotProduct;

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
            Vector3 dir = (pos - transform.position).normalized;
            _dotProduct = Vector3.Dot(dir, Vector3.right);
            transform.position = pos;
        }
    }

    private void Rollick()
    {
        if (_isRoll) return;
        Debug.Log(1);
        _isRoll = true;
        Sequence seq = DOTween.Sequence();
        if(_dotProduct > 0)
        {
            //¿À¸¥ÂÊ
            seq.Append(transform.DORotateQuaternion(Quaternion.Euler(0, -90, 359), 0.5f));
        }
        else if(_dotProduct < 0)
        {
            //¿ÞÂÊ
            seq.Append(transform.DORotateQuaternion(Quaternion.Euler(0, -90, -359), 0.5f));
        }
        seq.AppendCallback(() =>
        {
            transform.rotation = Quaternion.Euler(0, -90, 0);
            _isRoll = false;
        });
    }

    private void Update()
    {
        MoveSkateBoard();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Obstacle"))
        {
            Rollick();
        }
    }
}
