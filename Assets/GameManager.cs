using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    public int minX = -50;
    public int maxX = 50;
    public int minY = -50;
    public int maxY = 50;

    private float[] seed = new float[] {0.123456789f, 0.987654321f, 0.1243568790f};
    private float scale = 10;
    private float maxHeight = 2;
    private float[,] heights = new float[100,100];
    private float sparcity = 0.4f;

    [SerializeField]
    private GameObject[] placeableObjectPrefabs;
    [SerializeField]
    private GameObject[] groundObjects;

    async void Start() {
        for(int i = minX; i < maxX; i++) {
            for(int j = minY; j < maxY; j++) {
                if(i == 0 && j == 0) {

                } else {
                    float perlinNoise = Mathf.PerlinNoise(i * seed[0] + -minX, j * seed[0] + -minY);
                    if(perlinNoise < sparcity) {
                        
                    } else {
                        int index = Random.Range(0, placeableObjectPrefabs.Length);
                        GameObject currentPlaceableObject = Instantiate(placeableObjectPrefabs[index]);
                        int randX = i * Random.Range(70, 120);
                        int randY = j * Random.Range(70, 120);
                        currentPlaceableObject.transform.position = new Vector3(randX, 0, randY);
                    }
                }
            }
        }

        for(int i = -90; i < 90; i++) {
            for(int j = -90; j < 90; j++) {
                GameObject currentGroundObject = Instantiate(groundObjects[0]);
                currentGroundObject.transform.position = new Vector3(i * 10 + 5, -10, j * 10 + 5);
            }
        }

        // for(int x = 0; x < maxX; x++) {
        //     for(int y = 0; y < maxY; y++) {
        //         // heights [x,y] = maxHeight * Mathf.PerlinNoise((x + seed[0]) * scale, (y + seed[0]) * scale) + 0.5f * maxHeight * Mathf.PerlinNoise((x + seed[1]) * 0.5f * scale, (y + seed[1]) * 0.5f * scale);
        //         heights [x,y] = Mathf.PerlinNoise(x, y);
        //         Debug.Log(heights[x,y]);
        //     }
        // }
    }

    void Update() {
        
    }
}
