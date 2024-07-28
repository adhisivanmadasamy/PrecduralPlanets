using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneratePlanet : MonoBehaviour
{
    public ShapeSettings shapeSettings;
    public ColorSettings colorSettings;
    public Planet planet;

    public void Start()
    {
        planet = GetComponent<Planet>();        
    }

    public void SetShape()
    {
        shapeSettings.planetRadius = (int)Random.Range(4,8);
        AddNoiseLayers(shapeSettings);
        planet.GeneratePlanet();
    }

    public void SetColor()
    {        
        planet.GeneratePlanet();
    }

    public void AddNoiseLayers(ShapeSettings shapeSettings)
    {
        var newNoiseLayers = new ShapeSettings.NoiseLayer[3];

        newNoiseLayers[0] = AddSingleNoiseLayer(shapeSettings, false, "Simple", (int)Random.Range(3,5), (float)Random.Range(0.1f, 2.5f), (float)Random.Range(0.1f, 0.6f), (float)Random.Range(0.01f, 0.1f), (float)Random.Range(1f, 8f), (float)Random.Range(0.7f, 0.95f), new Vector3((float)Random.Range(-0.25f, 0.25f), (float)Random.Range(-0.25f, 0.25f), (float)Random.Range(-0.25f, 0.25f)));
        newNoiseLayers[1] = AddSingleNoiseLayer(shapeSettings, true, "Simple", (int)Random.Range(1, 4), (float)Random.Range(0.05f, 3f), (float)Random.Range(0.05f, 0.3f), (float)Random.Range(1f, 1.6f), (float)Random.Range(1f, 8f), (float)Random.Range(0.4f, 0.85f), new Vector3((float)Random.Range(-0.25f, 0.25f), (float)Random.Range(-0.25f, 0.25f), (float)Random.Range(-0.25f, 0.25f)));
        newNoiseLayers[2] = AddSingleNoiseLayer(shapeSettings, true, "Rigid", (int)Random.Range(1, 2), (float)Random.Range(0.1f, 3f), (float)Random.Range(0f, 0.2f), (float)Random.Range(1f, 5f), (float)Random.Range(0f, 1f), (float)Random.Range(0.4f, 0.85f), new Vector3((float)Random.Range(-0.25f, 0.25f), (float)Random.Range(-0.25f, 0.25f), (float)Random.Range(-0.25f, 0.25f)));
                    
        shapeSettings.noiseLayers = newNoiseLayers;
    }

    public ShapeSettings.NoiseLayer AddSingleNoiseLayer(ShapeSettings shapeSettings,bool maskLayer, string Type, int LayerNum, float BaseR, float Pers, float Strength, float Rough, float MinV, Vector3 Center)
    {
        var newNoiseLayer = new ShapeSettings.NoiseLayer();

        newNoiseLayer.enabled = true;
        newNoiseLayer.useFirstLayerAsMask = maskLayer;
        newNoiseLayer.noiseSettings = new NoiseSettings();

        if(Type == "Simple")
        {
            newNoiseLayer.noiseSettings.filterType = NoiseSettings.FilterType.Simple;
            newNoiseLayer.noiseSettings.simpleNoiseSettings = new NoiseSettings.SimpleNoiseSettings();

            newNoiseLayer.noiseSettings.simpleNoiseSettings.numLayers = LayerNum;
            newNoiseLayer.noiseSettings.simpleNoiseSettings.baseRoughness = BaseR;
            newNoiseLayer.noiseSettings.simpleNoiseSettings.persistence = Pers;
            newNoiseLayer.noiseSettings.simpleNoiseSettings.strength = Strength;
            newNoiseLayer.noiseSettings.simpleNoiseSettings.roughness = Rough;
            newNoiseLayer.noiseSettings.simpleNoiseSettings.centre.x = Center.x;
            newNoiseLayer.noiseSettings.simpleNoiseSettings.centre.y = Center.y;
            newNoiseLayer.noiseSettings.simpleNoiseSettings.centre.z = Center.z;
            newNoiseLayer.noiseSettings.simpleNoiseSettings.minValue = MinV;
        }
        else if(Type == "Rigid")
        {
            newNoiseLayer.noiseSettings.filterType = NoiseSettings.FilterType.Rigid;

            newNoiseLayer.noiseSettings.rigidNoiseSettings = new NoiseSettings.RigidNoiseSettings();

            newNoiseLayer.noiseSettings.rigidNoiseSettings.numLayers = LayerNum;
            newNoiseLayer.noiseSettings.rigidNoiseSettings.baseRoughness = BaseR;
            newNoiseLayer.noiseSettings.rigidNoiseSettings.persistence = Pers;
            newNoiseLayer.noiseSettings.rigidNoiseSettings.strength = Strength;
            newNoiseLayer.noiseSettings.rigidNoiseSettings.roughness = Rough;
            newNoiseLayer.noiseSettings.rigidNoiseSettings.centre.x = Center.x;
            newNoiseLayer.noiseSettings.rigidNoiseSettings.centre.y = Center.y;
            newNoiseLayer.noiseSettings.rigidNoiseSettings.centre.z = Center.z;
            newNoiseLayer.noiseSettings.rigidNoiseSettings.minValue = MinV;
            newNoiseLayer.noiseSettings.rigidNoiseSettings.weightMultiplier = 0.8f;
        }
        
        return newNoiseLayer;
    }

}
