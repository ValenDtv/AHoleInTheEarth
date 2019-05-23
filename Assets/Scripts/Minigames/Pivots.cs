using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pivots : MonoBehaviour
{
    public bool[] isUsed;
    public GameObject[] pivots;

    public int Size()
    {
        return pivots.Length;
    }

    public GameObject Get(int i)
    {
        return pivots[i];
    }

    public void Occupy(int i)
    {
        isUsed[i] = true;
    }

    public void Free(int i)
    {
        isUsed[i] = false;
    }

    public bool IsUsed(int i)
    {
        return isUsed[i];
    }
}
