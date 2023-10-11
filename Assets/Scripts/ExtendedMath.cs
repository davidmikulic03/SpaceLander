using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ExtendedMath
{
    // https://www.desmos.com/calculator/qr4advfhzc
    // The link above illustrates what the function does, where the x-axis is equivalent to the t value in this function, and the y, the output.
    //
    // Summary:
    //     Evaluates the Bias Function (of my creation) where 
    //
    // Parameters:
    //   t:
    //
    //   bias:
    public static float Bias(float t, float bias)
    {
        bias = Mathf.Clamp01(bias);

        float exponent = (1 - bias) / bias;
        return Mathf.Pow(t, exponent);
    }

    public static float EvaluateFromBias(this float t, float bias)
    {
        return Bias(t, bias);
    }
}
