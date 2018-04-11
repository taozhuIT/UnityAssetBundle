using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForechTest : MonoBehaviour {
    private Dictionary<int, int> ttt = new Dictionary<int, int>();
    private int i = 1;
	void Start () {
        for (int i = 0; i < 2000; ++i)
        {
            ttt[i] = i;
        }
	}
	
	void Update () 
    {
        UpdateTest1();
        //UpdateTest2();
	}

    private void UpdateTest1()
    {
        if (i > 0)
        {
            foreach (KeyValuePair<int, int> kv in ttt)
            {
                //Debug.Log(kv.Value);
            }

            i -= 1;
        }
    }

    private void UpdateTest2()
    {
        if (i > 0)
        {
            var kv = ttt.GetEnumerator();
            try
            {
                while(kv.MoveNext())
                {
                    //Debug.Log(kv.Current.Value);
                }
            }
            finally
            {
                kv.Dispose();
            }

            i -= 1;
        }
    }
}
