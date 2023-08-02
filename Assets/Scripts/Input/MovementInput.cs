using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class MovementInput : MonoBehaviour, IBeginDragHandler, IDragHandler
{
	public event Action<Vector2Int> DirectionChanged;

#if UNITY_STANDALONE_WIN
	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W))
		{
			DirectionChanged?.Invoke(Vector2Int.up);
			return;
		}

		if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A))
		{
			DirectionChanged?.Invoke(Vector2Int.left);
			return;
		}

		if (Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S))
		{
			DirectionChanged?.Invoke(Vector2Int.down);
			return;
		}

		if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D))
		{
			DirectionChanged?.Invoke(Vector2Int.right);
			return;
		}
	}
#endif

	public void OnBeginDrag(PointerEventData eventData)
	{
		if (Mathf.Abs(eventData.delta.x) > Mathf.Abs(eventData.delta.y))
			DirectionChanged?.Invoke(eventData.delta.x < 0f ? Vector2Int.left : Vector2Int.right);
		else
			DirectionChanged?.Invoke(eventData.delta.y < 0f ? Vector2Int.down : Vector2Int.up);
	}

	public void OnDrag(PointerEventData eventData) { }
}