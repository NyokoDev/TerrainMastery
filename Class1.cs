using UnityEngine;
using System.Reflection;
using System.Collections.Generic;
using ICities;
using UnityEngine.UI;
using ColossalFramework.UI;
using HarmonyLib;

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

    // Initialize the mod
    public void OnEnabled()
    {
        // Perform any setup or initialization for your mod here
        // This method is called when the mod is enabled (e.g., when the game starts)

        // Apply the Harmony patches
        Harmony harmony = new Harmony("com.nyoko.terrainmastery");
        harmony.PatchAll();

        // Check if the toolController exists and is not null before adding the TerrainMasteryBridge component
        if (ToolsModifierControl.toolController != null)
        {
            TerrainMasteryBridge bridge = ToolsModifierControl.toolController.gameObject.GetComponent<TerrainMasteryBridge>();
            if (bridge == null)
            {
                bridge = ToolsModifierControl.toolController.gameObject.AddComponent<TerrainMasteryBridge>();
            }
            bridge.Initialize(this);
        }
        else
        {
            Debug.LogError("TerrainMasteryMod: ToolsModifierControl.toolController is null. Cannot initialize TerrainMasteryBridge.");
        }
    }

    // Clean up the mod
    public void OnDisabled()
    {
        // Perform any cleanup or resource release for your mod here
        // This method is called when the mod is disabled (e.g., when the game closes)
    }

    // Called when the mod's settings are changed
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
    }

    private void OnResetToDefault()
    {
        // Reset mod settings to default values
        TerrainDetailLevel = 480;
        terrainDetailSlider.value = TerrainDetailLevel; // Update the slider UI to show the default value

        TerrainShiftLevel = 2;
        terrainShiftSlider.value = TerrainShiftLevel; // Update the slider UI to show the default value
    }
}

// Harmony patches
public static class TerrainMasteryPatches
{
    [HarmonyPatch(typeof(TerrainManager), "DETAIL_RESOLUTION")]
    [HarmonyPrefix]
    public static bool PrefixSetTerrainDetailLevel(ref int value)
    {
        TerrainMasteryBridge bridge = ToolsModifierControl.toolController.gameObject.GetComponent<TerrainMasteryBridge>();
        if (bridge != null)
        {
            value = bridge.TerrainDetailLevel;
            return false; // Skip the original method
        }
        return true;
    }

    [HarmonyPatch(typeof(TerrainManager), "DETAIL_SHIFT")]
    [HarmonyPrefix]
    public static bool PrefixSetTerrainShiftLevel(ref int value)
    {
        TerrainMasteryBridge bridge = ToolsModifierControl.toolController.gameObject.GetComponent<TerrainMasteryBridge>();
        if (bridge != null)
        {
            value = bridge.TerrainShiftLevel;
            return false; // Skip the original method
        }
        return true;
    }
}

// MonoBehaviour bridge class
public class TerrainMasteryBridge : MonoBehaviour
{
    private TerrainMasteryMod modInstance;

    public void Initialize(TerrainMasteryMod mod)
    {
        modInstance = mod;
    }

    public int TerrainDetailLevel => modInstance.TerrainDetailLevel;

    public int TerrainShiftLevel => modInstance.TerrainShiftLevel;
}
