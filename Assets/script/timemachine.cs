using UnityEngine;
using System.Collections;

public class timemachine : MonoBehaviour {
	public static float timeset;
	public static float	slowMoTimeset;
	// Use this for initialization
	void Start () {
		timeset = 1f;
		slowMoTimeset = 0.01f;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		if (timeset == 0)
		{
			timeset = slowMoTimeset;
		}
	}
}
