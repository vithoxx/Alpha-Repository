using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    public bool lamparaEncendida;
    public bool tieneLampara;

    public Cordura corduraManager;
    public TMP_Text sanityText;
    public Image sanityImage;

    public GameObject _playerRef;

    public List<GameObject> _areaList;
    public GameObject _enemyCurrentArea;

    public List<Transform> _patrolAreaList;


    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }


        _playerRef = GameObject.FindGameObjectWithTag("Player");
        GetReferences();
    }

    private void Update()
    {
        DebugSanity();
    }

    void GetReferences()
    {
        corduraManager = FindObjectOfType<Cordura>();
        _areaList.AddRange(GameObject.FindGameObjectsWithTag("Casa Area"));
    }

    public List<Transform> GetAreaPatrolList()
    {
        _patrolAreaList.AddRange(_enemyCurrentArea.GetComponentsInChildren<Transform>());
        return _patrolAreaList;
    }

    public void Clear()
    {
        _patrolAreaList.Clear();
    }

    void DebugSanity()
    {
        sanityImage.fillAmount = corduraManager.GetSanity() / 100;
        Debug.Log($"{corduraManager.GetSanity() / 100}");
    }
    public void BotonReiniciar()
    {
        SceneManager.LoadScene(0);
    }
}
