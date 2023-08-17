using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PushBack : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Slice") || other.CompareTag("watermelon") || other.CompareTag("Ground"))
        {
            Katana.katana.PushBack();
        }
    }
}
