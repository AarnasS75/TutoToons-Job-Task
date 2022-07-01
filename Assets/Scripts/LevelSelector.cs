using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelSelector : MonoBehaviour
{
    public GameObject grid;

    Button[] levelButtons;
 
    private void Start()
    {
        int levelReached = PlayerPrefs.GetInt("levelReached", 1);

        levelButtons = new Button[grid.transform.childCount];

        for (int i = 0; i < grid.transform.childCount; i++)
        {
            levelButtons[i] = grid.transform.GetChild(i).GetComponent<Button>();

            if (i + 1 > levelReached)
            {
                levelButtons[i].interactable = false;
            }
        }
    }

    public void Select(int level)
    {
        SceneManager.LoadScene(level);
    }
}
