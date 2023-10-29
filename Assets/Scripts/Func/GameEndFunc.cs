using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class GameEndFunc : MonoBehaviour
{
    [SerializeField] private Image _brokeImage;
    [SerializeField] private Transform[] _blackScreens;
    [SerializeField] private GameObject _toolkit;
    
    public void GameEnd()
    {
        
        StartCoroutine(GameEndCo());
    }

    IEnumerator GameEndCo()
    {
        yield return new WaitForSeconds(0.2f);
        _toolkit.SetActive(false);
        _brokeImage.enabled = true;
        yield return new WaitForSeconds(0.5f);
        _blackScreens[0].DOLocalMoveY(0, 0.7f).SetEase(Ease.OutBounce);
        yield return new WaitForSeconds(0.4f);
        _blackScreens[1].DOLocalMoveY(0, 0.7f).SetEase(Ease.OutBounce);
    }
}
