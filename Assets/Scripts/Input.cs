using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class Input : MonoBehaviour, IBeginDragHandler, IDragHandler
{
    public static Action<Vector2Int> DirectionChanged;

    #if UNITY_EDITOR_WIN
    private void Update()
    {
        if (DirectionChanged == null) return;

        if (UnityEngine.Input.GetKeyDown(KeyCode.UpArrow) || UnityEngine.Input.GetKeyDown(KeyCode.W))
        {
            DirectionChanged.Invoke(Vector2Int.up);
            return;
        }

        if (UnityEngine.Input.GetKeyDown(KeyCode.LeftArrow) || UnityEngine.Input.GetKeyDown(KeyCode.A))
        {
            DirectionChanged.Invoke(Vector2Int.left);
            return;
        }

        if (UnityEngine.Input.GetKeyDown(KeyCode.DownArrow) || UnityEngine.Input.GetKeyDown(KeyCode.S))
        {
            DirectionChanged.Invoke(Vector2Int.down);
            return;
        }

        if (UnityEngine.Input.GetKeyDown(KeyCode.RightArrow) || UnityEngine.Input.GetKeyDown(KeyCode.D))
        {
            DirectionChanged.Invoke(Vector2Int.right);
            return;
        }
    }
    #endif

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (DirectionChanged == null) return;

        if (Mathf.Abs(eventData.delta.x) > Mathf.Abs(eventData.delta.y))
            DirectionChanged.Invoke(eventData.delta.x < 0f ? Vector2Int.left : Vector2Int.right);
        else
            DirectionChanged.Invoke(eventData.delta.y < 0f ? Vector2Int.down : Vector2Int.up);
    }

    public void OnDrag(PointerEventData eventData) { }
}