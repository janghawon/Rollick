using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance;
    private int _score;
    private int _bestScore;
    [SerializeField] private Canvas _can;
    [SerializeField] private TextMeshProUGUI _scoreText;
    [SerializeField] private List<Sprite> _emoziList = new List<Sprite>();

    private Camera _mainCam;

    private void Awake()
    {
        if(Instance != null)
        {
            Debug.LogError("Warnning!! ScoreManager has Multiplying Rinning!!");
            return;
        }
        Instance = this;
        _mainCam = Camera.main;
    }

    public void AddScore(int value)
    {
        _score += value;
        UIManager.Instance.SetScore(_score);
    }

    public void GeneradeScore()
    {
        if(_score > PlayerPrefs.GetInt("bestScore"))
            PlayerPrefs.SetInt("bestScore", _score);
    }

    public void LookToAddScore(int value, Vector3 pos)
    {
        TextMeshProUGUI textpa = Instantiate(_scoreText, _can.transform);
        textpa.transform.position = _mainCam.WorldToScreenPoint(pos);
        textpa.text = $"+{value}";
        Image img = textpa.transform.Find("Emozi").GetComponent<Image>();
        img.sprite = _emoziList[Random.Range(0, _emoziList.Count)];

        Sequence seq = DOTween.Sequence();
        float targetv = textpa.transform.position.y + 100;
        seq.Append(textpa.transform.DOMoveY(targetv, 1f));
        seq.Append(textpa.DOFade(0, 0.5f));
        seq.Join(img.DOFade(0, 0.5f));

        seq.AppendCallback(() =>
        {
            Destroy(textpa.gameObject);
        });
    }
}
