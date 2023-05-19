using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputHandler : MonoBehaviour
{
    Player player;

    [Header("Input")]
    [SerializeField] InputActionReference _movementInput;
    [SerializeField] InputActionReference _jumpInput;
    [SerializeField] InputActionReference _dashInput;
    [SerializeField] InputActionReference _interactInput;


    public Vector2 MovementInput => _movementInput.action.ReadValue<Vector2>();
    public bool JumpInput => _jumpInput.action.triggered;
    public bool JumpCutInput => _jumpInput.action.WasReleasedThisFrame();
    public bool DashInput => _dashInput.action.triggered;
    public bool InteractInput => _interactInput.action.triggered;
    // public string InteractInputButton => _interactInput.action.name;
    public string InteractInputButton => "E";


    void Start()
    {
        player = GetComponent<Player>();
    }





}
