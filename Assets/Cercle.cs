using System;
using System. Collections;
using System. Collections. Generic;
using UnityEngine;


public class Cercle
{
	private float rayon;
	
	
	public Cercle (float r = 32)
	{
		this. rayon = Math. Abs (r);
	}
	
	
	public void modeliser (Vector3 translation)
	{
		List <Vector3> sommets = new List <Vector3> ();
		List <int> triangles   = new List <int> ();
		
		
		// Calcul des barycentres des cubes
		int x, y;
		double centreNormalise;
		int limite = (int) Math. Ceiling (Math. Sqrt (2) / 2 * this. rayon);
		for (int centreCube = -limite; centreCube < limite; centreCube ++)
		{
			centreNormalise = centreCube / this. rayon;
			
			// Haut du cercle
			x = centreCube;
			y = (int) Math. Round (this. rayon * Math. Sin (Math. Acos (centreNormalise)));
			Generation. cube (ref sommets, ref triangles, new Vector3 (x, y, 0) + translation);
			
			// Bas du cercle
			y *= -1;
			Generation. cube (ref sommets, ref triangles, new Vector3 (x, y, 0) + translation);
			
			// Droite du cercle
			x = (int) Math. Round (this. rayon * Math. Cos (Math. Asin (centreNormalise)));
			y = centreCube;
			Generation. cube (ref sommets, ref triangles, new Vector3 (x, y, 0) + translation);
			
			// Gauche du cercle
			x *= -1;
			Generation. cube (ref sommets, ref triangles, new Vector3 (x, y, 0) + translation);
		}
		
		Generation. generer ("cercle", ref sommets, ref triangles);
	}
}


// Force brute : O (8 ^ s)
