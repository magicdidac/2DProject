using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CombustibleModel", menuName = "Models/Combustible", order = 0)]
public class CombustibleModel : ItemModel
{
    public float duration;
    public float quantity;
    public float amplitude;
    public float speed;
}
