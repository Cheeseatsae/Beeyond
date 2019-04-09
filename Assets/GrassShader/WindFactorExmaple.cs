using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindFactorExmaple : MonoBehaviour
{
    // This script will demonstrate how to change the 'Wind Strength' property of the Grass Shader
    // First, you need to create a variable to store the MAT, and access to the wind variable you want to hook it up to
    [Range(0f,1f)] public float MyWindVariable;
    public Material GrassShader;
    
    // Drag the shader from the gameobject onto the field in the inspector (done outside of script)

    // Then, assign the wind variable to its property 'Wind Strength' in the script here
    // IMPORTANT: Use the variable name written on the SHADER ITSELF (hint: it should start with '_')

    void Start()
    {
        GrassShader.SetFloat("_WindStrength", MyWindVariable);
    }

    private void FixedUpdate()
    {
       GrassShader.SetFloat("_WindStrength", Roo.WindScript.windSpeed * MyWindVariable);
    }
}
