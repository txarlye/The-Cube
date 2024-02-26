using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;
using static GuardaID;

public class MapGenerator : Singleton<MapGenerator>
{
    public GameObject tilePrefab;   // Prefab de la pieza del suelo 
    public int width = 50;          // Ancho de la matriz
    public int height = 50;         // Alto de la matriz
    public Material[] materialesSuelo;
    public float scaleA = 1.0f;     // Escala vertical para el porcentaje A
    public float scaleB = 1.0f;     // Escala vertical para el porcentaje B
    public float percentageA = 50f; // Porcentaje de tiles a escalar en el grupo A
    public float percentageB = 50f; // Porcentaje de tiles a escalar en el grupo B
    public int scaleCount = 1;      // Número de veces que se escala cada grupo 
    public float coinPercentage = 10f;  // Porcentaje de tiles donde aparecen monedas

    private GameObject[,] suelo;
    private GuardaID guardaID;
    private float tamanoX;
    private float tamanoZ;

    private void Awake()
    {
        //sacamos el tamaño del prefab máximo
        tamanoX = tilePrefab.GetComponent<Renderer>().bounds.size.x;
        tamanoZ = tilePrefab.GetComponent<Renderer>().bounds.size.z;
        generarSuelo(tamanoX, tamanoZ, width, height);
    }

    public void generarSuelo(float tamanoX, float tamanoZ, int width, int height)
    {
        suelo = new GameObject[Mathf.RoundToInt(width / tamanoX), Mathf.RoundToInt(height / tamanoZ)];
        //suelo = new GameObject[width, height];
        for (float x = 0; x < width; x += tamanoX)
        {
            for (float z = 0; z < height; z += tamanoZ)
            {
                Vector3 tilePosition = new Vector3(x, 0.01f, z);
                GameObject loseta = Instantiate(tilePrefab, tilePosition, Quaternion.identity);

                int auxID = Random.Range(0, materialesSuelo.Length);
                loseta.GetComponent<Renderer>().material = materialesSuelo[auxID];
                guardaID = loseta.AddComponent<GuardaID>();
                guardaID.SetID(auxID);

                // Escalamos el tile en el porcentaje A o B, según corresponda
                float randomPercentage = Random.Range(0f, 100f);
                float scale = 1.0f;
                if (randomPercentage < percentageA)
                {
                    scale = scaleA;
                }
                else if (randomPercentage < percentageA + percentageB)
                {
                    scale = scaleB;
                }

                // Escalamos el tile solo en la dirección vertical (eje Y)
                if (scale != 1.0f)
                {
                    loseta.transform.localScale = new Vector3(loseta.transform.localScale.x, scale, loseta.transform.localScale.z);

                    // Escalamos el tile el número de veces especificado
                    for (int i = 0; i < scaleCount; i++)
                    {
                        loseta.transform.localScale = new Vector3(loseta.transform.localScale.x, loseta.transform.localScale.y * scale, loseta.transform.localScale.z);
                    }
                }
                // Almacenamos el objeto de suelo generado en la matriz suelo
                int row = Mathf.RoundToInt(x / tamanoX);
                int column = Mathf.RoundToInt(z / tamanoZ);
                suelo[row, column] = loseta;
            }
        }
        
    }

    public GameObject[,] getSuelo()
    {
        return suelo;
    }
    
    public void ReiniciarMapa(float elevacion)
    {
        // Destruir el mapa actual
        for (int i = 0; i < suelo.GetLength(0); i++)
        {
            for (int j = 0; j < suelo.GetLength(1); j++)
            {
                Destroy(suelo[i, j]);
            }
        }

        // Generar un nuevo mapa en la posición elevada
        Vector3 nuevaPosicion = new Vector3(transform.position.x, elevacion, transform.position.z);
        transform.position = nuevaPosicion;
        generarSuelo(tamanoX, tamanoZ, width, height);
    }
    
}
