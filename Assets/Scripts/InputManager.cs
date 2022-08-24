using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class InputManager : MonoBehaviour, IBeginDragHandler, IDragHandler
{
    public Action<Vector2Int> ChangeDirectionAction;

    #if UNITY_EDITOR_WIN
    private void Update()
    {
        if (ChangeDirectionAction == null) return;

        if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W))
        {
            ChangeDirectionAction.Invoke(Vector2Int.up);
            return;
        }

        if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A))
        {
            ChangeDirectionAction.Invoke(Vector2Int.left);
            return;
        }

        if (Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S))
        {
            ChangeDirectionAction.Invoke(Vector2Int.down);
            return;
        }

        if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D))
        {
            ChangeDirectionAction.Invoke(Vector2Int.right);
            return;
        }
    }
    #endif

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (ChangeDirectionAction == null) return;

        if (Mathf.Abs(eventData.delta.x) > Mathf.Abs(eventData.delta.y))
            ChangeDirectionAction.Invoke(eventData.delta.x < 0f ? Vector2Int.left : Vector2Int.right);
        else
            ChangeDirectionAction.Invoke(eventData.delta.y < 0f ? Vector2Int.down : Vector2Int.up);
    }

    public void OnDrag(PointerEventData eventData) { }
}