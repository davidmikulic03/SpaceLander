using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ExtendedMath
{
    public static float Bias(float value, float bias)
    {
        bias = Mathf.Clamp01(bias);

        float exponent = (1 - bias) / bias;
        return Mathf.Pow(value, exponent);
    }
}
