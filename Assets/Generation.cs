using System. Collections;
using System. Collections. Generic;
using UnityEngine;


public class Generation: MonoBehaviour
{
    void Start ()
    {
		/*
         *  new Cercle (). modeliser (Vector3. zero);
         *  new Sphere (64). modeliser (Vector3. zero);
		 */
    }
	
	
	// Sans normales
    public static void generer (string nom, ref List <Vector3> sommets, ref List <int> triangles)
	{
		Mesh modele = new Mesh ();
		modele. vertices  = sommets. ToArray ();
		modele. triangles = triangles. ToArray ();
		
		GameObject objet = new GameObject (nom, typeof (MeshRenderer), typeof (MeshFilter));
		objet. GetComponent <MeshFilter> (). mesh = modele;
	}
	
	// Avec normales
    public static void generer (string nom, ref List <Vector3> sommets, ref List <int> triangles, ref Vector3 [] normales)
	{
		Mesh modele = new Mesh ();
		modele. vertices  = sommets. ToArray ();
		modele. triangles = triangles. ToArray ();
		modele. normals   = normales;
		
		GameObject objet = new GameObject (nom, typeof (MeshRenderer), typeof (MeshFilter));
		objet. GetComponent <MeshFilter> (). mesh = modele;
	}
	
	
	// Modéliser un cube
	public static void cube (ref List <Vector3> sommets, ref List <int> triangles, Vector3 barycentre, float cote = 1)
	{
		int nbSommets = sommets. Count;
		float demiCote = cote / 2;
		
		for (float x = -demiCote; x < cote; x += cote)
		{
			for (float y = -demiCote; y < cote; y += cote)
			{
				for (float z = -demiCote; z < cote; z += cote)
				{
					sommets. Add (new Vector3 (x, y, z) + barycentre);
				}
			}
		}
		
		int [] nouveauxTriangles = new int []
		{
			0, 1, 3,
			0, 2, 6,
			0, 3, 2,
			0, 4, 5,
			0, 5, 1,
			0, 6, 4,
			1, 5, 7,
			1, 7, 3,
			2, 3, 7,
			2, 7, 6,
			4, 6, 7,
			4, 7, 5
		};
		somme (ref nouveauxTriangles, nbSommets);
		triangles. AddRange (nouveauxTriangles);
	}
	
	public static void somme (ref int [] liste, int ajout)
	{
		for (int indice = 0; indice < liste. Length; indice ++)
		{
			liste [indice] += ajout;
		}
	}
}
