using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MakeCity : MonoBehaviour
{
    [SerializeField] TextAsset layout;
    [SerializeField] GameObject roadPrefab;
    [SerializeField] GameObject curvePrefab;
    [SerializeField] GameObject crossPrefab;
    [SerializeField] GameObject emptyPrefab;
    [SerializeField] GameObject buildingPrefab;
    [SerializeField] GameObject semaphorePrefab;
    [SerializeField] GameObject parkingPrefab;
    [SerializeField] int tileSize;

    //Listas con coordenadas para prefabs especificos
    public List<(int,int)> upLight = new List<(int,int)> {(0,12), (1,12), (6,1), (7,1), (13,1), (14,1)};
    public List<(int,int)> downLight = new List<(int,int)> {(6,15), (7,15), (22, 6), (23, 6), (16,21), (17,21)};
    public List<(int,int)> leftLight = new List<(int,int)> {(5,-1), (5,0), (12,-1), (12,0), (21,8), (21,7)};
    public List<(int,int)> rightLight = new List<(int,int)> {(2,11), (2,10), (8,17), (8,16), (18,22), (18,23)};
    public List<(int,int)> upParking = new List<(int,int)> {(10,6), (3,21)};
    public List<(int,int)> downParking = new List<(int,int)> {(19,1), (3,18)};
    public List<(int,int)> leftParking = new List<(int,int)> {(2,14), (18,13), (18,19)};
    public List<(int,int)> rightParking = new List<(int,int)> {(5, 3), (12,3), (21,3), (5,14), (12,14), (12,19), (21,21)};
    // Start is called before the first frame update
    void Start()
    {
        MakeTiles(layout.text);
    }
void MakeTiles(string tiles)
    {
        int x = 0;
        // Mesa has y 0 at the bottom
        // To draw from the top, find the rows of the file
        // and move down
        // Remove the last enter, and one more to start at 0
        int y = tiles.Split('\n').Length - 2;
        Debug.Log(y);

        Vector3 position;
        GameObject tile;

        for (int i=0; i<tiles.Length; i++) {
            if (tiles[i] == '>' || tiles[i] == '<') {
                position = new Vector3(x * tileSize, 0, y * tileSize);
                tile = Instantiate(roadPrefab, position, Quaternion.Euler(0, 90, 0));
                tile.transform.parent = transform;
                x += 1;
            } else if (tiles[i] == 'v' || tiles[i] == '^') {
                position = new Vector3(x * tileSize, 0, y * tileSize);
                tile = Instantiate(roadPrefab, position, Quaternion.Euler(0, 0, 0));
                tile.transform.parent = transform;
                x += 1;
            } else if (tiles[i] == 'x') {
                position = new Vector3(x * tileSize, 0, y * tileSize);
                tile = Instantiate(crossPrefab, position, Quaternion.Euler(0, 90, 0));
                tile.transform.parent = transform;
                x += 1;
            } else if (tiles[i] == 'S' || tiles[i] == 's') {
                position = new Vector3(x * tileSize, 0, y * tileSize);
                if (upLight.Contains((x,y))){
                    tile = Instantiate(roadPrefab, position, Quaternion.Euler(0, 0, 0));
                    tile.transform.parent = transform;
                    tile = Instantiate(semaphorePrefab, position, Quaternion.Euler(0, 0, 0));
                } else if (downLight.Contains((x,y))){
                    tile = Instantiate(roadPrefab, position, Quaternion.Euler(0, 0, 0));
                    tile.transform.parent = transform;
                    tile = Instantiate(semaphorePrefab, position, Quaternion.Euler(0, 180, 0));
                } else if (leftLight.Contains((x,y))) {
                    tile = Instantiate(roadPrefab, position, Quaternion.Euler(0, 90, 0));
                    tile.transform.parent = transform;
                    tile = Instantiate(semaphorePrefab, position, Quaternion.Euler(0, 270, 0));
                } else {
                    tile = Instantiate(roadPrefab, position, Quaternion.Euler(0, 90, 0));
                    tile.transform.parent = transform;
                    tile = Instantiate(semaphorePrefab, position, Quaternion.Euler(0, 90, 0));
                }
                tile.transform.parent = transform;
                x += 1;
            } else if (tiles[i] == 'e') {
                position = new Vector3(x * tileSize, 0, y * tileSize);
                tile = Instantiate(emptyPrefab, position, Quaternion.Euler(0, 0, 0));
                tile.transform.parent = transform;
                if (upParking.Contains((x, y))){
                    tile = Instantiate(parkingPrefab, position, Quaternion.Euler(0, 180, 0));
                } else if(downParking.Contains((x, y))){
                    tile = Instantiate(parkingPrefab, position, Quaternion.Euler(0, 0, 0));
                } else if(leftParking.Contains((x, y))){
                    tile = Instantiate(parkingPrefab, position, Quaternion.Euler(0, 90, 0));
                } else {
                    tile = Instantiate(parkingPrefab, position, Quaternion.Euler(0, 270, 0));
                }
                tile.transform.parent = transform;
                x += 1;
            } else if (tiles[i] == '#') {
                position = new Vector3(x * tileSize, 0, y * tileSize);
                tile = Instantiate(emptyPrefab, position, Quaternion.Euler(0, 0, 0));
                tile.transform.parent = transform;
                
                tile = Instantiate(buildingPrefab, position, buildingPrefab.transform.rotation); //Quaternion.identity);
                tile.transform.localScale = new Vector3(0.3f, Random.Range(0.07f, 0.2f), 0.3f);
                tile.transform.parent = transform;
                x += 1;
            } else if (tiles[i] == '\n') {
                x = 0;
                y -= 1;
            }
        }

    }
}