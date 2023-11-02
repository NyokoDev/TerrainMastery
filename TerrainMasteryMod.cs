using UnityEngine;
using ICities;
using ColossalFramework.UI;
using HarmonyLib;
using UnifiedUI.Helpers;
using ColossalFramework.PlatformServices;

public class TerrainMasteryMod : IUserMod
{
    // Your mod's metadata
    public string Name => "Terrain Mastery";
    public string Description => "Switch Terrain's level of detail and shift.";

    // Custom mod options
    public int TerrainDetailLevel { get; private set; } = 480;
    public int TerrainShiftLevel { get; private set; } = 2;

    // Slider components references
    private UISlider terrainDetailSlider;
    private UISlider terrainShiftSlider;
    

    // MonoBehaviour controller reference
    private TerrainMasteryController terrainController;

    

// Initialize the mod
public void OnEnabled()
    {
        // Create the TerrainMasteryController instance and attach it to the GameObject
        GameObject terrainControllerObject = new GameObject("TerrainMasteryController");
        terrainController = terrainControllerObject.AddComponent<TerrainMasteryController>();

        // Apply the Harmony patches
        Harmony harmony = new Harmony("com.nyoko.terrainmastery");
        harmony.PatchAll();
    }

    // Clean up the mod
    public void OnDisabled()
    {
        // Cleanup code goes here
    }

    public void OnSettingsUI(UIHelperBase helper)
    {
        // Create the settings group for Terrain Mastery mod
        var group = helper.AddGroup("Terrain Mastery Settings");

        // Add a slider to adjust terrain detail level
        terrainDetailSlider = (UISlider)group.AddSlider("Terrain Detail Level", 100, 2000, 1, TerrainDetailLevel, OnTerrainDetailLevelChanged);

        // Add a slider to adjust terrain shift level
        terrainShiftSlider = (UISlider)group.AddSlider("Terrain Shift Level", 1, 10, 1, TerrainShiftLevel, OnTerrainShiftLevelChanged);

        // Close the group to finish settings UI for Terrain Mastery mod
        group.AddSpace(10);
        group.AddButton("Apply Settings", OnApplySettings);
        group.AddButton("Reset to Default", OnResetToDefault);
    }

    private void OnTerrainDetailLevelChanged(float value)
    {
        TerrainDetailLevel = Mathf.RoundToInt(value);
    }

    private void OnTerrainShiftLevelChanged(float value)
    {
        TerrainShiftLevel = Mathf.RoundToInt(value);
    }

    private void OnApplySettings()
    {
        // Apply the mod settings here
        // You can use the TerrainDetailLevel and TerrainShiftLevel values in your mod's logic
        terrainController.SetTerrainDetailLevel(TerrainDetailLevel);
        terrainController.SetTerrainShiftLevel(TerrainShiftLevel);
    }

   


    private void OnResetToDefault()
    {
        // Reset mod settings to default values
        TerrainDetailLevel = 480;
        terrainDetailSlider.value = TerrainDetailLevel; // Update the slider UI to show the default value

        TerrainShiftLevel = 2;
        terrainShiftSlider.value = TerrainShiftLevel; // Update the slider UI to show the default value

        terrainController.SetTerrainDetailLevel(TerrainDetailLevel);
        terrainController.SetTerrainShiftLevel(TerrainShiftLevel);
    }
}
