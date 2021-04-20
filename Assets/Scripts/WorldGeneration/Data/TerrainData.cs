using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu()]
public class TerrainData : UpdatableData
{
    public float uniformScale = 1f;

    public float meshHeightMultiplier;
    public bool useFlatShading;

    public AnimationCurve meshHeightCurve;
}
