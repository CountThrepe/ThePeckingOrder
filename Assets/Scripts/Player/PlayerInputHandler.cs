using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputHandler : MonoBehaviour
{
    private Vector2 move;
    private PlayerMovement movement;

    void Start() {
        movement = GetComponent<PlayerMovement>();
    }

    void Update()
    {
        movement.Move(move.x, move.y > 0);
    }

    public void OnMove(InputValue value)
    {
        move = value.Get<Vector2>();
    }
}