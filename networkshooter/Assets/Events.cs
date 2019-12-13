using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public static class Events {

    public static System.Action<GameObject, bool> OnTarget = delegate { };
    public static System.Action GameStart = delegate { };
    public static System.Action GameOver = delegate { };

	public static System.Action<string> OnMusic = delegate { };
	public static System.Action<string> OnSoundFX = delegate { };
	
	
}
