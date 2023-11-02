using AlgernonCommons.Keybinding;
using TerrainMastery;
using System;
using System.Xml.Serialization;
using UnityEngine;
using System.IO;

internal class ModSettings
{
    [XmlElement("ToggleKey")]
    public Keybinding XMLToggleKey
    {
        get => UUIKey.Keybinding;
        set => UUIKey.Keybinding = value;
    }

    [XmlIgnore]
    internal static UnsavedInputKey ToggleKey => UUIKey;
    // UUI hotkey.
    [XmlIgnore]
    private static readonly TerrainMastery.UnsavedInputKey UUIKey = new TerrainMastery.UnsavedInputKey(name: "TerrainMastery hotkey", keyCode: KeyCode.L, control: true, shift: false, alt: true);


    internal static void Save()
    {
        XmlSerializer serializer = new XmlSerializer(typeof(ModSettings));
        using (var stream = new FileStream("TerrainMastery.xml", FileMode.Create))
        {
            serializer.Serialize(stream, new ModSettings());
        }
    }
}
