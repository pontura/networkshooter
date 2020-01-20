using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Settings : MonoBehaviour
{
    public Color[] colors;

    public Color GetColor(int num)
    {
        if (num >= colors.Length-1 || num < 0)
            num = 0;
        return colors[num];
    }
}
