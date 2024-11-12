using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatalogManager : MonoBehaviour
{
    public static CatalogManager instance;

    private void Awake()
    {
        instance = this;
    }
}
