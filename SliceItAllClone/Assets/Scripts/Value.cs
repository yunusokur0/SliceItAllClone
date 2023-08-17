using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Value : MonoBehaviour
{
    public int xAmount;
    public static Value Values;

    private void Awake()
    {
        Values = this;
    }
}
