using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 当一个物体对任意摄像机变得可见/不可见时被调用
/// </summary>
public class ObjBecameVisible : MonoBehaviour 
{
    private void OnBecameVisible()
    {
        Debug.Log("1");
    }

    private void OnBecameInvisible()
    {
        Debug.Log("2");
    }
}
