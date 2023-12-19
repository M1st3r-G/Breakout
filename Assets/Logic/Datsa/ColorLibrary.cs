using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class ColorLibrary : ScriptableObject
{
    [SerializeField] private List<Sprite[]> colors;
}
