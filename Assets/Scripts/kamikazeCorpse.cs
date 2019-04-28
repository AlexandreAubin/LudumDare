using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Diagnostics;

public class kamikazeCorpse : MonoBehaviour
{
    public float timeDestroy;
    Stopwatch timerDestroy = new Stopwatch();

    // Start is called before the first frame update
    void Start()
    {
        timerDestroy.Start();
    }

    // Update is called once per frame
    void Update()
    {
        if (timerDestroy.ElapsedMilliseconds > timeDestroy * 1000)
        {
            DestroyImmediate(this.gameObject);
        }
    }
}
