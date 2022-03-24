using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    public int minX = -50;
    public int maxX = 50;
    public int minY = -50;
    public int maxY = 50;

    [SerializeField]
    private GameObject[] placeableObjectPrefabs;

    async void Start() {
        for(int i = minX; i < maxX; i++) {
            for(int j = minY; j < maxY; j++) {
                int index = Random.Range(0, placeableObjectPrefabs.Length);
                GameObject currentPlaceableObject = Instantiate(placeableObjectPrefabs[index]);
                currentPlaceableObject.transform.position = new Vector3(i * 100, 0, j * 100);
            }
        }
    }

    void Update() {
        
    }
}
