using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SceneType
{
    title = 0,
    lobby = 1,
    ingame = 2,
    gameover = 3
}

public class SceneManager : MonoBehaviour
{
    public static SceneManager Instance;
    [SerializeField] private List<GameObject> _scenePrefabList = new List<GameObject>();
    [SerializeField] private GameObject _selectScene;
    [SerializeField] private SceneType _selectSceneType;
    [SerializeField] private Transform _sceneTrans;

    private void Awake()
    {
        if(Instance != null)
        {
            Debug.LogError("!!!!!1");
            return;
        }
        Instance = this;
    }

    public void ChangeScene(SceneType st)
    {
        if (_selectSceneType == st) return;
        if(_selectScene != null)
        {
            Destroy(_selectScene);
        }

        GameObject Scene = Instantiate(_scenePrefabList[(int)st], _sceneTrans);
        Scene.name = st.ToString();

        _selectSceneType = st;
        _selectScene = Scene;
    }
}
