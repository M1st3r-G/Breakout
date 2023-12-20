using UnityEngine;

[CreateAssetMenu]
public class ColorLibrary : ScriptableObject
{
    [SerializeField] private Sprite[] Set1;
    [SerializeField] private Sprite[] Set2;
    [SerializeField] private Sprite[] Set3;
    [SerializeField] private Sprite[] Set4;

    public Sprite[] GetColorScheme(int i)
    {
        switch (i)
        {
            case 1:
                return Set2;
            case 2:
                return Set3;
            case 3:
                return Set4;
            case 0:
            default:
                return Set1;
        }
    }

    public static int NumberOfSchemes() => 4;
}
