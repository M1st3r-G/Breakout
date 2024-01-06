using UnityEngine;

[CreateAssetMenu]
public class ColorLibrary : ScriptableObject
{
    [SerializeField] private Color[] Set1;
    [SerializeField] private Color[] Set2;
    [SerializeField] private Color[] Set3;

    public Color[] GetColorScheme(int i)
    {
        return i switch
        {
            1 => Set2,
            2 => Set3,
            _ => Set1
        };
    }

    public static int NumberOfSchemes() => 3;
    // Crucial that this is Right
}
