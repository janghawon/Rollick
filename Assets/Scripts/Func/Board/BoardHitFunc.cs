using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.Events;

public class BoardHitFunc : MonoBehaviour
{
    [Header("¼ÂÆÃ")]
    [SerializeField] private BoardFunc _boardFunc;
    [SerializeField] private Rigidbody _rigid;
    [SerializeField] private Transform _cameraTrm;
    [SerializeField] private CinemachineImpulseSource cis;
    [SerializeField] private UnityEvent _endEvent;

    [Header("°ª")]
    [SerializeField] private float _forceValue;

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Obstacle") && _boardFunc.isHit)
        {
            Vector3 dir = (_cameraTrm.position - _boardFunc.transform.position).normalized;

            cis.m_DefaultVelocity = new Vector3(0.5f, 0.5f, 0.5f);
            cis.GenerateImpulse();

            _rigid.angularVelocity = Random.insideUnitCircle * _forceValue;
            _rigid.AddForce(dir * _forceValue, ForceMode.Impulse);

            _endEvent?.Invoke();
        }
    }
}
