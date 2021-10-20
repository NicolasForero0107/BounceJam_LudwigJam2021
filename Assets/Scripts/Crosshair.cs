using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.InputSystem.InputAction;

public class Crosshair : MonoBehaviour
{
    [Tooltip("References")]
    [SerializeField] Camera camera = null;
    Vector2 _pointerPos = Vector2.zero;
    // Start is called before the first frame update
    void Start()
    {

    }

    public void OnPointerMove(CallbackContext ctx)
    {
        _pointerPos = ctx.ReadValue<Vector2>();
        _pointerPos = camera.ScreenToWorldPoint(_pointerPos);
        transform.position = new Vector3(_pointerPos.x, _pointerPos.y, 0.0f);
    }
}
