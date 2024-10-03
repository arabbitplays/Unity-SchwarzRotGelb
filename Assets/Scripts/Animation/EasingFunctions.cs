using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EasingFunctions
{
    private EasingFunctions() {}

    public static float LinearEasing(float t) {
        return t;
    }

    public static float EaseInEaseOutSin(float t) {
        return (-Mathf.Cos(Mathf.PI * t) + 1) / 2;
    }

    public static float EaseInOutElastic(float x) {
        float c5 = (2f * Mathf.PI) / 4.5f;

        return  x == 0
        ? 0
        : x == 1
        ? 1
        : x < 0.5
        ? -(Mathf.Pow(2f, 20f * x - 10f) * Mathf.Sin((20f * x - 11.125f) * c5)) / 2f
        : (Mathf.Pow(2f, -20f * x + 10f) * Mathf.Sin((20f * x - 11.125f) * c5)) / 2f + 1f;
    }
}
