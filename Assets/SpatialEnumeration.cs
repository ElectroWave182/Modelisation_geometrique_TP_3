using System.Collections.Generic;
using UnityEngine;

public class SpatialEnumeration : MonoBehaviour
{
    public int gridResolution = 10; // Résolution de la grille de cubes
    public Vector3 gridSize = new Vector3(10, 10, 10); // Taille de la boîte englobante
    public float cubeSize = 1.0f; // Taille des cubes

    public List<Vector3> sphereCenters = new List<Vector3>(); // Liste des centres de sphères
    public List<float> sphereRadii = new List<float>(); // Liste des rayons de sphères

    // Génération de la grille
    void Start()
    {
        // Ajouter deux sphères pour commencer
        sphereCenters.Add(new Vector3(2, 2, 2));
        sphereRadii.Add(3.0f);

        sphereCenters.Add(new Vector3(5, 8, 5));
        sphereRadii.Add(1.5f);

        GenerateGrid();
    }

    void GenerateGrid()
    {
        // Itération à travers chaque point de la boîte englobante
        for (int x = 0; x < gridResolution; x++)
        {
            for (int y = 0; y < gridResolution; y++)
            {
                for (int z = 0; z < gridResolution; z++)
                {
                    Vector3 cubePosition = new Vector3(
                        x * cubeSize,
                        y * cubeSize,
                        z * cubeSize
                    );

                    // Vérification si ce cube est dans une des sphères
                    bool isInside = IsInsideAnySphere(cubePosition);

                    if (isInside)
                    {
                        // Si le cube est à l'intérieur de la sphère, on le dessine
                        DrawCube(cubePosition);
                    }
                }
            }
        }
    }

    // Vérifie si un point donné est à l'intérieur d'une des sphères
    bool IsInsideAnySphere(Vector3 point)
    {
        for (int i = 0; i < sphereCenters.Count; i++)
        {
            float distance = Vector3.Distance(point, sphereCenters[i]);
            if (distance < sphereRadii[i])
            {
                return true;
            }
        }
        return false;
    }

    // Méthode pour dessiner un cube
    void DrawCube(Vector3 position)
    {
        GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
        cube.transform.position = position;
        cube.transform.localScale = Vector3.one * cubeSize;
    }
	
	bool IsInsideAllSpheres(Vector3 point)
	{
		foreach (var center in sphereCenters)
		{
			float distance = Vector3.Distance(point, center);
			if (distance >= sphereRadii[sphereCenters.IndexOf(center)])
			{
				return false; // Si le point n'est pas dans une sphère, retourner false
			}
		}
		return true; // Si le point est dans toutes les sphères
	}

	bool IsInsideUnionOfSpheres(Vector3 point)
	{
		foreach (var center in sphereCenters)
		{
			float distance = Vector3.Distance(point, center);
			if (distance < sphereRadii[sphereCenters.IndexOf(center)])
			{
				return true; // Si le point est dans au moins une sphère
			}
		}
		return false; // Si le point n'est dans aucune sphère
	}
}
