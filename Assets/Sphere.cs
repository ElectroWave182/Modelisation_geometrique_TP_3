using System;
using System. Collections;
using System. Collections. Generic;
using UnityEngine;


public class Sphere
{
	private float diametre;
	private int subdivisions;
	
	
	public Sphere (float d, int s = 6)
	{
		this. diametre = Math. Abs (d);
		this. subdivisions = Math. Abs (s);
	}
	
	
	public void modeliser (Vector3 translation)
	{
		List <Vector3> sommets = new List <Vector3> ();
		List <int> triangles   = new List <int> ();
		
		Generation. cube (ref sommets, ref triangles, Vector3. zero, diametre);
		
		Generation. generer ("sphere", ref sommets, ref triangles);
	}
}


// Force brute : O (8 ^ s)
