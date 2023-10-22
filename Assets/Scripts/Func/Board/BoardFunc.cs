using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardFunc : MonoBehaviour
{
    private Camera _mainCam;

    private void Awake()
    {
        _mainCam = Camera.main;
    }

    private void Update()
    {
        Debug.Log(_mainCam.ScreenToWorldPoint(Input.mousePosition));
    }
}
