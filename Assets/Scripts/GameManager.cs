using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
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
    }

    [SerializeField]
    private SceneLoader sceneLoader;

    [SerializeField]
    private InputHandler inputManager;

    [SerializeField]
    private Spawner spawner;

    [SerializeField]
    LineAnimator lineAnimator;

    private void Start()
    {
        inputManager.OnObjectSelect += HandlePointSelect;
    }
    private void HandlePointSelect(GameObject selectedPoint)
    {
        if (spawner.points.IndexOf(selectedPoint) == 0)
        {
            selectedPoint.GetComponent<PointScript>().SelectPoint();
            spawner.points.Remove(selectedPoint);
            lineAnimator.MakeLine(selectedPoint.transform);
        }
    }
    public void EndLevel()
    {
        PlayerPrefs.SetInt("levelReached", sceneLoader.GetSceneIndex() + 2);
        sceneLoader.Load(0);

    }
}
