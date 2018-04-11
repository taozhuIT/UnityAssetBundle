using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameInstance;

public class SingletonTest : Singleton<SingletonTest>
{
    public SingletonTest() { }
    public void bbb()
    {
        Debug.Log("sssssss");
    }
}
