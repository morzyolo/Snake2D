using UnityEngine;

public class InputSystem : MonoBehaviour
{
	[SerializeField] private Snake _snake;
	[SerializeField] private MovementInput _movementInput;

	private void OnEnable()
	{
		_movementInput.DirectionChanged += _snake.ChangeDirection;
	}

	private void OnDisable()
	{
		_movementInput.DirectionChanged -= _snake.ChangeDirection;
	}
}
