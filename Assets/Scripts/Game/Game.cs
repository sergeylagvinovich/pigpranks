using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Game : MonoBehaviour
{
    public static Game instantiate;

    [SerializeField] Transform[] _spawnPointStones;

    [SerializeField] Stone _stonePrefab;
    [SerializeField] Transform _parentStones;
    private PoolObjects<Stone> _stonesPool;

    [SerializeField] Transform _parentBoombs;
    [SerializeField] Boomb _prefabBoomb;
    private PoolObjects<Boomb> _boombsPool;

    [SerializeField] Transform _parentExpl;
    [SerializeField] Explosion _prefabExpl;
    private PoolObjects<Explosion> _explsPool;
    [SerializeField] GameObject _gameUI;
    [SerializeField] GameObject _endUI;
    [SerializeField] Text _endText;
    [SerializeField] Image _colorPanel;

    public int CountDogs { get; set; }


    private float _radiousBoomb = 2f;
    private int _damageBoomb = 1;

    private void Start()
    {
        if (instantiate != null)
        {
            instantiate.Init();
            Destroy(this);
        }
        else
        {
            instantiate = this;
            instantiate.Init();
        }
    }

    public void EndGame(Color color, string text)
    {
        _gameUI.SetActive(false);
        _endUI.SetActive(true);
        _colorPanel.color = color;
        _endText.text = text;
        Time.timeScale = 0;
    }

    public void RestartGame()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(0);
    }

    private void Init()
    {
        _gameUI.SetActive(true);
        _endUI.SetActive(false);
        CountDogs = 3;
        _stonesPool = new PoolObjects<Stone>(_stonePrefab, 20, _parentStones);
        _stonesPool.AutoFill = true;
        CreateStones();

        _boombsPool = new PoolObjects<Boomb>(_prefabBoomb, 5, _parentBoombs);
        _boombsPool.AutoFill = true;
        SetBoombRadiousAndDamage();

        _explsPool = new PoolObjects<Explosion>(_prefabExpl, 5, _parentBoombs);
        _explsPool.AutoFill = true;

    }

    private void SetBoombRadiousAndDamage()
    {
        for (int i = 0; i < _boombsPool.Count(); i++)
        {

            _boombsPool[i].InitOnCreate(_radiousBoomb, _damageBoomb);
        }
    }

    private void CreateStones()
    {
        for (int i = 0; i < _spawnPointStones.Length; i++)
        {
            for (int j = 0; j < 8; j++)
            {
                Stone newStone = _stonesPool.GetFreeObject();
                newStone.transform.position = new Vector2(_spawnPointStones[i].position.x + 1.7f * j, _spawnPointStones[i].position.y);
            }
        }
    }

    public Boomb GetFreeBoomb()
    {
        return _boombsPool.GetFreeObject();
    }

    public Explosion GetFreeExpl()
    {
        return _explsPool.GetFreeObject();
    }
}
