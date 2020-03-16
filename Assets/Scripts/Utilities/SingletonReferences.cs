using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingletonReferences : MonoBehaviour
{
    // This script is for initialize the instance of MasterManager, so I can use it as static object without gettin null reference exception
    [SerializeField] private MasterManager masterManager;
}
