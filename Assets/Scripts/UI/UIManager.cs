using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;
    private UIDocument _doc;

    private Label _scoreTxt;

    private void Awake()
    {
        if (Instance != null)
        {
            Debug.LogError("Warnning!! UIManager has Multiplying Rinning!!");
            return;
        }
        Instance = this;
        _doc = GetComponent<UIDocument>();
    }

    private void OnEnable()
    {
        var root = _doc.rootVisualElement;
        _scoreTxt = root.Q<Label>("score-txt");
        _scoreTxt.text = 0.ToString();
    }

    public void SetScore(int value)
    {
        _scoreTxt.text = value.ToString();
    }
}
