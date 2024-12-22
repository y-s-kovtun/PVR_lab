using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateFractalScript : MonoBehaviour
{
    public int textureWidth = 512;
    public int textureHeight = 512;
    public float zoom = 1.5f;
    public Vector2 juliaSetParameters = new Vector2(-0.8f, 0.156f);

    void Start()
    {
        Texture2D juliaTexture = new Texture2D(textureWidth, textureHeight);

        for (int y = 0; y < textureHeight; y++)
        {
            for (int x = 0; x < textureWidth; x++)
            {
                float complexX = Map(x, 0, textureWidth, -zoom, zoom);
                float complexY = Map(y, 0, textureHeight, -zoom, zoom);

                int iterations = JuliaIterations(new Complex(complexX, complexY), new Complex(juliaSetParameters.x, juliaSetParameters.y));

                Color color = Color.Lerp(Color.black, Color.white, (float)iterations / 100.0f);
                juliaTexture.SetPixel(x, y, color);
            }
        }

        juliaTexture.Apply();
        GetComponent<Renderer>().material.mainTexture = juliaTexture;
    }

    float Map(float value, float fromMin, float fromMax, float toMin, float toMax)
    {
        return toMin + (value - fromMin) * (toMax - toMin) / (fromMax - fromMin);
    }

    int JuliaIterations(Complex z, Complex c)
    {
        int iterations = 0;

        while (z.MagnitudeSquared() < 4.0 && iterations < 100)
        {
            z = z * z + c;
            iterations++;
        }

        return iterations;
    }
}

public struct Complex
{
    public float Real;
    public float Imaginary;

    public Complex(float real, float imaginary)
    {
        Real = real;
        Imaginary = imaginary;
    }

    public static Complex Zero { get { return new Complex(0, 0); } }

    public static Complex operator *(Complex a, Complex b)
    {
        return new Complex(a.Real * b.Real - a.Imaginary * b.Imaginary, a.Real * b.Imaginary + a.Imaginary * b.Real);
    }

    public static Complex operator +(Complex a, Complex b)
    {
        return new Complex(a.Real + b.Real, a.Imaginary + b.Imaginary);
    }

    public float MagnitudeSquared()
    {
        return Real * Real + Imaginary * Imaginary;
    }

}
