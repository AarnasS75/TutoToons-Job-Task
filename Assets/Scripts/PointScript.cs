using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PointScript : MonoBehaviour
{
    SpriteRenderer renderer;

    [SerializeField]
    private TextMeshProUGUI UItext;

    public Sprite pinkSprite, blueSprite;
    public bool selected { get; private set; }
    public int value { get; private set; }

    private void Awake()
    {
        renderer = GetComponent<SpriteRenderer>();
    }
    public void SelectPoint()
    {
        selected = true;
        renderer.sprite = blueSprite;
        transform.GetChild(0).gameObject.SetActive(false);
    }
    public void AssignValue(int nr)
    {
        value = nr;
        UItext.text = value.ToString();
    }
}
