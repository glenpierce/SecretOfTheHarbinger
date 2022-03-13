using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour {
    private string _moveXAxisName = "Horizontal";
    private string _moveYAxisName = "Vertical";
    private string _lookXAxisName = "Mouse X";
    private string _lookYAxisName = "Mouse Y";
    private string _aButtonAxisName = "Fire1";
    private string _bButtonAxisName = "Fire2";
    private string _dashButtonAxisName = "Fire3";
    private int _playerNumber;
    private Player _player;

    private float _moveX;
    private float _moveY;
    private float _lookX;
    private float _lookY;
    private float _aButton;
    private float _bButton;
    private float _dashButtonInputValue;

    public float _rotationY;
    public float _rotationX;

    public bool _isDashing;
    private float _dashFuel = 12;
    private float _nextInkTime;

    private bool _isSitting;

    public static readonly int MaxDashFuel = 12;
    public static readonly int DashFuelConsumeRate = 1;
    public static readonly int DashFuelReplenishRate = 1;
    public static readonly float InkIntervalSeconds = 0.25f;

    public Vector2 sensitivity = new Vector2(2, 2);
    public Vector2 smoothing = new Vector2(3, 3);

    Rigidbody myRigidBody;
    private Animator _animator;

    public void Start() {
        // Cursor.lockState = CursorLockMode.Locked;
        myRigidBody = GetComponentInParent<Rigidbody>();
        myRigidBody.useGravity = true;
        _player = gameObject.GetComponent<Player>();
        myRigidBody.drag = _player.drag;
        _animator = gameObject.GetComponentInChildren<Animator>();
    }

    public void Update() {
        myRigidBody.mass = _player.weight;
    }

    public void FixedUpdate() {

        UpdateInput();

        if (_isDashing) {
            _player.speed = _player.dashSpeed;
        } else {
            _player.speed = _player.normalSpeed;
        }


        var xMovement = _moveX;
        var zMovement = _moveY;
        var yMovement = (_aButton) - (_bButton);
        var movementVector = new Vector3(xMovement, yMovement, zMovement) * _player.speed * 5;
        myRigidBody.AddRelativeForce(movementVector);

        _isSitting = myRigidBody.velocity.magnitude < 0.1f;

        if(_animator != null) {
            _animator.SetBool("Sit_b", _isSitting);
            _animator.SetFloat("Speed_f", movementVector.magnitude);
            // _animator.SetBool("IsDashing", _isDashing);
        }

        // _rotationX = _rotationX + (_lookY * _player.turnSpeed);
        _rotationY = _rotationY + (_lookX * _player.turnSpeed);

        transform.localEulerAngles = new Vector3(_rotationX, _rotationY);

        //Debug.Log(myRigidBody.velocity);
        //Debug.Log(myRigidBody.velocity.magnitude);
        

        HandleDash();
    }

    private void HandleDash() { 
        // if the button is pressed
        if (DashButtonPressed()) {
            // if we have Fuel left
            if (_dashFuel > 0) {
                _isDashing = true;
                _dashFuel -= Time.deltaTime * DashFuelConsumeRate;
            } else {
                _isDashing = false;
                _dashFuel = 0;
            }

        } else {
            _isDashing = false;
            _dashFuel += Time.deltaTime * DashFuelReplenishRate;
            if (_dashFuel > MaxDashFuel) {
                _dashFuel = MaxDashFuel;
            }
        }
	}

    private bool DashButtonPressed() {
        return _dashButtonInputValue > 0;
    }

    private void UpdateInput() {
        _moveX = Input.GetAxis(_moveXAxisName) + getMoveX();
        _moveY = Input.GetAxis(_moveYAxisName) + getMoveY();
        var mouseDelta = new Vector2(Input.GetAxisRaw("Mouse X"), Input.GetAxisRaw("Mouse Y"));
        mouseDelta = Vector2.Scale(mouseDelta, new Vector2(sensitivity.x * smoothing.x, sensitivity.y * smoothing.y));
        _lookX = Input.GetAxis(_lookXAxisName);
        _lookY = Input.GetAxis(_lookYAxisName);
        _aButton = Input.GetAxis(_aButtonAxisName) + getJump();
        _bButton = Input.GetAxis(_bButtonAxisName);
        _dashButtonInputValue = Input.GetAxis(_dashButtonAxisName);
    }

    private int getMoveX() {
        if(Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D)) {
            return 1;
        } else if(Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A)) {
            return -1;
        } else {
            return 0;
        }
    }

    private int getMoveY() {
        if(Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W)) {
            return 1;
        } else if(Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S)) {
            return -1;
        } else {
            return 0;
        }
    }

    private int getJump() {
        if(Input.GetKey(KeyCode.Space)) {
            return 1;
        } else {
            return 0;
        }
    }
}
