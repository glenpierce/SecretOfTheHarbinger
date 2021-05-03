using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {
    public int weight = 50;
    public int drag = 5;
    public static float speedNormalizer = 1000.0f;
    public float speed = 1.0f * speedNormalizer;
    public float normalSpeed = 1.0f * speedNormalizer;
    public float dashSpeed = 9f * speedNormalizer;
    public float turnSpeed = 3f;
}
