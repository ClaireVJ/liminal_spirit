using System;
using UnityEngine;

public class PossessionEvents
{
    public event Action<GameObject> OnTransformationComplete;
    public void CompleteTransformation(GameObject possessedObject)
    {
        OnTransformationComplete?.Invoke(possessedObject);
    }
}
