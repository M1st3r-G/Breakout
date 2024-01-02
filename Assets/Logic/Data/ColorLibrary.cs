using UnityEngine;

[CreateAssetMenu]
public class ColorLibrary : ScriptableObject
{
    [SerializeField] private Sprite[] Set1;
    [SerializeField] private Sprite[] Set2;
    [SerializeField] private Sprite[] Set3;

    public Sprite[] GetColorScheme(int i)
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
