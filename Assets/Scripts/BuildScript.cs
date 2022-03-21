﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildScript : MonoBehaviour {
    [SerializeField]
    private GameObject[] placeableObjectPrefabs;

    private GameObject currentPlaceableObject;

    private float mouseWheelRotation;
    private int currentPrefabIndex = -1;

    private void Update() {
        HandleNewObjectHotkey();

        if (currentPlaceableObject != null) {
            Cursor.lockState = CursorLockMode.Confined;
            MoveCurrentObjectToMouse();
            RotateFromMouseWheel();
            ReleaseIfClicked();
        }
    }

    private void HandleNewObjectHotkey() {
        for (int i = 0; i < placeableObjectPrefabs.Length; i++) {
            if (Input.GetKeyDown(KeyCode.Alpha0 + 1 + i)) {
                if (PressedKeyOfCurrentPrefab(i)) {
                    Destroy(currentPlaceableObject);
                    currentPrefabIndex = -1;
                } else {
                    if (currentPlaceableObject != null) {
                        Destroy(currentPlaceableObject);
                    }

                    currentPlaceableObject = Instantiate(placeableObjectPrefabs[i]);
                    currentPrefabIndex = i;
                }

                break;
            }
        }
    }

    private bool PressedKeyOfCurrentPrefab(int i) {
        return currentPlaceableObject != null && currentPrefabIndex == i;
    }

    private void MoveCurrentObjectToMouse() {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        RaycastHit hitInfo;
        if (Physics.Raycast(ray, out hitInfo)) {
            currentPlaceableObject.transform.position = hitInfo.point;
            currentPlaceableObject.transform.rotation = Quaternion.FromToRotation(Vector3.up, hitInfo.normal);
        }
    }

    private void RotateFromMouseWheel() {
        Debug.Log(Input.mouseScrollDelta.y);
        mouseWheelRotation = Input.mouseScrollDelta.y;
        currentPlaceableObject.transform.Rotate(Vector3.up, currentPlaceableObject.transform.rotation.y + mouseWheelRotation * 10f);
    }

    private void ReleaseIfClicked() {
        if (Input.GetMouseButtonDown(0)) {
            currentPlaceableObject.AddComponent<MeshCollider>();
            currentPlaceableObject = null;
            Cursor.lockState = CursorLockMode.Locked;
        }
    }
}