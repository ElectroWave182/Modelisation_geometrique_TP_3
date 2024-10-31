using UnityEngine;

public class ImplicitSurface : MonoBehaviour
{
    public int gridResolution = 10; // Résolution de la grille de cubes
    public float cubeSize = 1.0f; // Taille des cubes
	
    public float[,,] potentialGrid; // Grille de potentiels
    public float threshold = 0.5f; // Seuil de visibilité des cubes
    public float potentialChange = 0.1f; // Changement de potentiel avec l'outil

    public Transform tool; // Outil pour modifier le potentiel (une sphère par exemple)
    public float toolRadius = 20.0f; // Rayon de l'outil

    void Start()
    {
        // Initialisation de la grille de potentiels
        potentialGrid = new float[gridResolution, gridResolution, gridResolution];

        InitializePotentials();
        DisplayPotentialGrid();
    }

    void Update()
    {
        if (Input.GetMouseButton(0)) // Ajouter de la matière
        {
            ModifyPotential(tool.position, potentialChange);
        }
        else if (Input.GetMouseButton(1)) // Enlever de la matière
        {
            ModifyPotential(tool.position, -potentialChange);
        }

        DisplayPotentialGrid();
    }

    void InitializePotentials()
    {
        // Initialiser les potentiels (par exemple à zéro partout)
        for (int x = 0; x < gridResolution; x++)
        {
            for (int y = 0; y < gridResolution; y++)
            {
				for (int z = 0; z < gridResolution; z++)
				{
					potentialGrid[x, y, z] = 0.0f;
				}
            }
        }
    }

    void ModifyPotential(Vector3 toolPosition, float change)
    {
        for (int x = 0; x < gridResolution; x++)
        {
            for (int y = 0; y < gridResolution; y++)
            {
				for (int z = 0; z < gridResolution; z++)
				{
					Vector3 cubePosition = new Vector3(x * cubeSize, y * cubeSize, z * cubeSize);
					float distance = Vector3.Distance(cubePosition, toolPosition);
					if (distance < toolRadius)
					{
						potentialGrid[x, y, z] += change;
					}
				}
            }
        }
    }

    void DisplayPotentialGrid()
    {
        for (int x = 0; x < gridResolution; x++)
        {
            for (int y = 0; y < gridResolution; y++)
            {
				for (int z = 0; z < gridResolution; z++)
				{
					Vector3 cubePosition = new Vector3(x * cubeSize, y * cubeSize, z * cubeSize);
					if (potentialGrid[x, y, z] > threshold)
					{
						DrawCube(cubePosition);
					}
				}
            }
        }
    }

    void DrawCube(Vector3 position)
    {
        GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
        cube.transform.position = position;
        cube.transform.localScale = Vector3.one * cubeSize;
    }
}
