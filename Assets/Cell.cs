using UnityEngine;

public class Cell : MonoBehaviour
{
    public bool IsAlive;
    public SpriteRenderer SpriteRenderer;

    public void Start()
    {
        SpriteRenderer = GetComponent<SpriteRenderer>();
        UpdateCellColor();
    }

    public void SetAlive(bool alive)
    {
        IsAlive = alive;
        UpdateCellColor();
    }

    private void UpdateCellColor()
    {
        SpriteRenderer.color = IsAlive ? Color.white : Color.black;
    }
}