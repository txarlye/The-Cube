using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AllocateElements : MonoBehaviour
{
    public GameObject prefabA;
    public GameObject prefabB;
    public float percentageA = 50f;
    public float percentageB = 50f;
    public int maxNumA = 10;
    public int maxNumB = 10;

    private void Start()
    {
        GameObject[,] suelo = MapGenerator.instance.getSuelo();

        int totalTiles = suelo.GetLength(0) * suelo.GetLength(1);

        int numSelectedA = Mathf.RoundToInt(totalTiles * percentageA / 100f);
        int numSelectedB = Mathf.RoundToInt(totalTiles * percentageB / 100f);

        numSelectedA = Mathf.Clamp(numSelectedA, 0, maxNumA);
        numSelectedB = Mathf.Clamp(numSelectedB, 0, maxNumB);

        List<GameObject> tiles = new List<GameObject>();
        for (int i = 0; i < suelo.GetLength(0); i++)
        {
            for (int j = 0; j < suelo.GetLength(1); j++)
            {
                tiles.Add(suelo[i, j]);
            }
        }

        List<GameObject> selectedTilesA = new List<GameObject>();
        List<GameObject> selectedTilesB = new List<GameObject>();

        while (selectedTilesA.Count < numSelectedA && tiles.Count > 0)
        {
            int index = Random.Range(0, tiles.Count);
            selectedTilesA.Add(tiles[index]);
            tiles.RemoveAt(index);
        }

        while (selectedTilesB.Count < numSelectedB && tiles.Count > 0)
        {
            int index = Random.Range(0, tiles.Count);
            selectedTilesB.Add(tiles[index]);
            tiles.RemoveAt(index);
        }

        foreach (GameObject tile in selectedTilesA)
        {
            Vector3 position = tile.transform.position+Vector3.up;
            GameObject element = Instantiate(prefabA, position, Quaternion.identity);
        }

        foreach (GameObject tile in selectedTilesB)
        {
            Vector3 position = tile.transform.position+new Vector3(0,1,0);
            GameObject element = Instantiate(prefabB, position, Quaternion.Euler(90f, 0f, 0f));
        }
    }
}
