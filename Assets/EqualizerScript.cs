using UnityEngine;

public class EqualizerScript : MonoBehaviour
{
    public GameObject cubePrefab; // Reference to the cube prefab
    public int numberOfCubes = 64; // Number of cubes in the visualizer
    public float scaleMultiplier = 10.0f; // Scale multiplier for cube size
    public float maxScale = 20.0f; // Maximum scale for the cubes
    public float audioSensitivity = 5.0f; // Sensitivity of audio visualization
    public int cubSize = 2;
    private GameObject[] cubes;

    void Start()
    {
        cubes = new GameObject[numberOfCubes];



        // Instantiate cubes and position them in a row
        for (int i = 0; i < numberOfCubes; i++)
        {
            cubes[i] = Instantiate(cubePrefab, transform);
            cubes[i].transform.position = new Vector3(i * cubSize, 20, 0); // Adjust spacing
        }
    }

    void Update()
    {
        // Get audio spectrum data
        float[] spectrumData = new float[numberOfCubes];
        AudioListener.GetSpectrumData(spectrumData, 0, FFTWindow.Rectangular);

        for (int i = 0; i < numberOfCubes; i++)
        {
            float scale = spectrumData[i] * scaleMultiplier * audioSensitivity;
            scale = Mathf.Clamp(scale, 0.1f, maxScale); // Clamp scale value



            Vector3 newScale = new Vector3(1, scale, 1);
            cubes[i].transform.localScale = newScale;
        }
    }

}
