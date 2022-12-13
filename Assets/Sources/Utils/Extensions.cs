using System.Collections.Generic;
using UnityEngine;

public static class Extensions
{
    public static float InverseLerpUnclamped(float a, float b, float value)
    {
        if(b == a) return 0;
        return (value - a) / (b - a);
    }

    public static T GetRandom<T>(this List<T> list)
    {
        return list[Random.Range(0, list.Count)];
    }
}
