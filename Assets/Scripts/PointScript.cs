using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PointScript : MonoBehaviour
{
    SpriteRenderer renderer;

    [SerializeField]
    Animator anim;

    [SerializeField]
    private TextMeshProUGUI UItext;

    public Sprite pinkSprite, blueSprite;
    public bool selected { get; private set; }
    public int value { get; private set; }

    private void Awake()
    {
        renderer = GetComponent<SpriteRenderer>();
    }
    private void Start()
    {
        anim.enabled = false;
    }
    public void SelectPoint()
    {
        selected = true;
        renderer.sprite = blueSprite;
        anim.enabled = true;
    }
    public void AssignValue(int nr)
    {
        value = nr;
        UItext.text = value.ToString();
    }
}
