using UnityEngine;
using ICities;

public class TerrainMasteryController : MonoBehaviour
{
    public void SetTerrainDetailLevel(int detailLevel)
    {
        Terrain.activeTerrain.detailObjectDistance = detailLevel;
        Terrain.activeTerrain.treeDistance = detailLevel;
        Terrain.activeTerrain.heightmapMaximumLOD = detailLevel;
        
    }


    public void SetTerrainShiftLevel(int shiftLevel)
    {
        Terrain.activeTerrain.heightmapPixelError = shiftLevel;
        Terrain.activeTerrain.basemapDistance = shiftLevel;
        Terrain.activeTerrain.treeMaximumFullLODCount = shiftLevel;
    }

}
