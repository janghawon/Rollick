using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;

public class GameEndFunc : MonoBehaviour
{
    [SerializeField] private Image _brokeImage;
    [SerializeField] private Transform[] _blackScreens;
    [SerializeField] private Image _bitLine;
    [SerializeField] private Transform _rtBtn;
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
        yield return new WaitForSeconds(0.5f);
        _rtBtn.transform.localPosition = new Vector3(0, -230, 0);
        StartCoroutine(BitLine());
    }

    IEnumerator BitLine()
    {
        _bitLine.enabled = true;

        while(true)
        {
            _bitLine.fillAmount = 0;
            _bitLine.color = new Color(1, 1, 1, 1);

            while (_bitLine.fillAmount < 1)
            {
                _bitLine.fillAmount += 0.01f;
                yield return null;
            }
            Sequence seq = DOTween.Sequence();
            seq.Append(_bitLine.DOFade(0, 0.6f));
            seq.Join(_bitLine.transform.DOScale(1.5f, 0.1f));
            seq.Join(_bitLine.transform.DOScale(1f, 0.1f));
            yield return new WaitForSeconds(1f);
        }
    }
}
