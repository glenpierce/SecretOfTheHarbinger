using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class giant : MonoBehaviour {

    public Player _giant;
    public GameObject target;
    public GameObject[] patrolPoints;
    public int nextPatrolPointIndex = 0;

    private float _moveX;
    private float _moveY;
    private float _lookX;
    private float _lookY;

    public float targetZ;
    public float targetX;

    public bool onTargetX = false;
    public bool onTargetY = false;

    public float _rotationY;
    public float _rotationX;

    private bool _isSitting;

    Rigidbody myRigidBody;
    private Animator _animator;

    public void Start() {
        myRigidBody = GetComponentInParent<Rigidbody>();
        myRigidBody.useGravity = true;
        _animator = gameObject.GetComponentInChildren<Animator>();
    }

    public void Update() {

    }

    public void FixedUpdate() {

        if(patrolPoints.Length != 0) {
            if(patrolPoints[nextPatrolPointIndex].transform.position == this.transform.position) { // todo add epsilon to this.
                nextPatrolPointIndex ++;
            }
        }
        
        planMovement();

        var xMovement = _moveX;
        var zMovement = _moveY;
        var yMovement = 0;
        var movementVector = new Vector3(xMovement, yMovement, zMovement) * _giant.speed;
        myRigidBody.AddRelativeForce(movementVector);

        _isSitting = myRigidBody.velocity.magnitude < 0.1f;

        if(_animator != null) {
            // _animator.SetBool("Sit_b", _isSitting);
            _animator.SetFloat("Speed_z", movementVector.z);
            _animator.SetFloat("Speed_x", movementVector.x);
            // _animator.SetBool("IsDashing", _isDashing);
        }

        _rotationX = _rotationX + (_lookY * _giant.turnSpeed);
        _rotationY = _rotationY + (_lookX * _giant.turnSpeed);

        transform.localEulerAngles = new Vector3(_rotationX, _rotationY);
    }

    void planMovement() {
        if(patrolPoints.Length == 0)
            return;
        if(nextPatrolPointIndex == patrolPoints.Length)
                nextPatrolPointIndex = 0;

        onTargetX = false;
        onTargetY = false;

        UnityEngine.Vector3 destination = patrolPoints[nextPatrolPointIndex].transform.position;

        float epsilon = 100;

        if(destination.z < this.transform.position.z + epsilon) {
            _moveY = -1;
        } else if (destination.z > this.transform.position.z - epsilon) {
            _moveY = 1;
        } else {
            onTargetY = true;
        }

        if(destination.x < this.transform.position.x + epsilon) {
            _moveX = -1;
        } else if (destination.x > this.transform.position.x - epsilon) {
            _moveX = 1;
        } else {
            onTargetX = true;
            if(onTargetY) {
                nextPatrolPointIndex++;
            }
        }

        targetZ = destination.z - this.transform.position.z;
        targetX = destination.x - this.transform.position.x;
    }
}
