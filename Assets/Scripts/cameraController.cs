using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraController : MonoBehaviour {

    private string _lookYAxisName = "Mouse Y";
    private float _lookY;
    
    public float _rotationY;
    public float _rotationX;

    private float maxValue = 40;
    private float minValue = -15;

    private Vector2 sensitivity = new Vector2(.5f, .5f);

    void Update() {
        var mouseDelta = new Vector2(0, -Input.GetAxisRaw("Mouse Y"));
        mouseDelta = Vector2.Scale(mouseDelta, new Vector2(0, sensitivity.y));
        _lookY = mouseDelta.y;

        var newRotation = _rotationY + _lookY;

        if(newRotation < maxValue && newRotation > minValue) {
            _rotationY = newRotation;
        }

        transform.localEulerAngles = new Vector3(_rotationY, 0);
    }
}
