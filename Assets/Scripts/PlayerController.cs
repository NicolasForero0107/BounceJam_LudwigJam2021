using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using static UnityEngine.InputSystem.InputAction;

public class PlayerController : MonoBehaviour
{
    [Range(0.1f, 1.0f)]
    [SerializeField] float movementFalloffPercent = 0.1f;
    [SerializeField] float movementFalloffThreshold = 1.0f;
    [SerializeField] float movementSpeed = 1.0f;
    [SerializeField] bool hasMomentum = false;
    [SerializeField] float momentumBuildupSpeed = 0.25f;
    [Tooltip("Momentum lerps between movementFalloffPercent and this value, then uses the resulting value to add momentum to the player.")]
    [SerializeField] float maxMomentum = 0.1f;
    [SerializeField] float jumpScalar = 1.0f;
    [Tooltip("True: Allows the player to move while in the air.\nFalse: Will use momentum instead of direct input instead.")]
    [SerializeField] bool allowXAxisInputWhileJumping = true;

    [Header("References")]
    [SerializeField] new Rigidbody2D rigidbody = null;
    [SerializeField] GroundCollider groundCollider = null;
    [SerializeField] Transform crosshair = null;
    [SerializeField] JumpBar jumpBar = null;


    Vector2 _moveVec = new Vector2();
    [HideInInspector] public bool doMovementFalloff = true;
    float _momentumTValue = 0.0f;
    bool _canJump = false;

    // Start is called before the first frame update
    void Start()
    {

        void EnableJump()
        {
            _canJump = true;
        }
        void DisableJump()
        {
            _canJump = false;
        }
        groundCollider.OnStayGround.AddListener(EnableJump);
        groundCollider.OnLeaveGround.AddListener(DisableJump);
    }

    private void Update()
    {
        if (_moveVec.magnitude == 0.0f)
        {
            _momentumTValue -= Time.deltaTime * momentumBuildupSpeed * 2.0f;
            if (_momentumTValue <= 0.0f)
                _momentumTValue = 0.0f;
            return;
        }
        _momentumTValue += Time.deltaTime * momentumBuildupSpeed;
        if (_momentumTValue >= 1.0f)
            _momentumTValue = 1.0f;
    }

    public void OnJump(CallbackContext ctx)
    {
        if (!_canJump)
            return;

        //accuracy. sliver value changes to be between either -1 or 1
        var direction = (crosshair.position - transform.position).normalized;
        direction.y = Mathf.Abs(direction.y);
        direction += new Vector3(jumpBar.sliderValue, jumpBar.sliderValue, 0.0f);
        direction = direction.normalized;
        Debug.DrawLine(transform.position, direction * jumpScalar, Color.red, 1.0f);
        rigidbody.AddForce(direction * jumpScalar, ForceMode2D.Impulse);
    }
}
