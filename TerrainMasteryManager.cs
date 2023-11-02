using UnityEngine;
using System.Collections.Generic;
using ICities;
using UnityEngine.UI;
using ColossalFramework.UI;
using HarmonyLib;
using AlgernonCommons;
using AlgernonCommons.UI;
using System.Reflection;
using AlgernonCommons.Translation;
using UnifiedUI.Helpers;
using System;
using static AlgernonCommons.UI.UISliders;

public class TerrainMasteryPanel : StandalonePanel


{
    TerrainMasteryPanel panel;
    private static TerrainMasteryPanel s_instance;
    internal UUICustomButton UUIButton => _uuiButton;
    internal UUICustomButton _uuiButton;
    internal static TerrainMasteryPanel Instance => s_instance;

    internal const float ContentWidth = 600f;
    // Layout constants - private.
    private const float TitleHeight = 60f;
    private const float ContainerHeight = 600f;
    public override float PanelWidth => ContentWidth + (Margin * 2f);
    public override void Awake()
    {
        base.Awake();

        // Basic behaviour.
        autoLayout = false;
        canFocus = true;
        isInteractive = true;

        // Appearance.
        atlas = UITextures.InGameAtlas;
        backgroundSprite = "UnlockingPanel2";
        opacity = PanelOpacity;
    }

    protected override float PanelOpacity => 0.5f;

    /// <summary>
    /// Gets the panel height.
    /// </summary>
    public override float PanelHeight => TitleHeight + ContainerHeight;

    /// <summary>
    /// Gets the panel's title.
    /// </summary>
    protected override string PanelTitle => Translations.Translate(TerrainMasteryTR.TranslationID.MOD_NAME);

    public string Name = "Terrain Mastery";
    private SliderValueFormat numberFormat;

    public override void Start()
    {
        base.Start();

        SetIcon(UITextures.LoadSprite("UUI"), "normal");
        //handler.LoadSettings();

        UISlider mySlider = UISliders.AddBudgetSlider(
            parent: panel,
            xPos: default,
            yPos: default,
            width: 600f,
            maxValue: 2f

 ); ; ; ;
    }

    // Panel Structure


   









































    internal static void OnLoad()
    {
        try
        {
            Log("[THEMEMASTERY] OnLoad called");

            // Set instance reference.
            if (s_instance == null)
            {
                s_instance = new TerrainMasteryPanel();
                Log("[THEMEMASTERY] s_instance created");
            }

            ModSettings Modsinstance = new ModSettings();

            // Add UUI button.
            s_instance._uuiButton = UUIHelpers.RegisterCustomButton(
                name: TerrainMasteryPanel.Instance.Name,
                groupName: null, // default group
                tooltip: "Terrain Mastery",
                icon: UUIHelpers.LoadTexture(UUIHelpers.GetFullPath<TerrainMasteryMod>("Resources", "UUI.png")),
                onToggle: (value) =>
                {
                    try
                    {
                        if (value)
                        {
                            StandalonePanelManager<TerrainMasteryPanel>.Create();
                            Log("[THEMEMASTERY] Panel created");
                        }
                        else
                        {
                            StandalonePanelManager<TerrainMasteryPanel>.Panel?.Close();
                            Log("[THEMEMASTERY] Panel closed");
                        }
                    }
                    catch (Exception ex)
                    {
                        LogError("[THEMEMASTERY] Error in onToggle: " + ex.Message);
                    }
                },

                hotkeys: new UUIHotKeys { ActivationKey = ModSettings.ToggleKey });

            Log("[THEMEMASTERY] Button registered");
        }
        catch (Exception ex)
        {
            LogError("[THEMEMASTERY] Error in OnLoad: " + ex.Message);
        }
    }

    private static void Log(string message)
    {
        Debug.Log(message);
    }

    private static void LogError(string message)
    {
        Debug.LogError(message);
    }



    protected override bool PreClose()
    {


        TerrainMasteryPanel.Instance.UUIButton.IsPressed = false;



        // Always okay to close.
        return true;
    }
}


    
