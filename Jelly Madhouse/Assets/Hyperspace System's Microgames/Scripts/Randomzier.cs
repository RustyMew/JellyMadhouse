using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Randomizer : MonoBehaviour
{
    int[,] values = new int[8, 8] {
        {0,0,0,0,0,0,0,0} , {0,0,0,0,0,0,0,0} , {0,0,0,0,0,0,0,0} , {0,0,0,0,0,0,0,0} , {0,0,0,0,0,0,0,0} , {0,0,0,0,0,0,0,0} , {0,0,0,0,0,0,0,0} , {0,0,0,0,0,0,0,0}
    };
    // Start is called before the first frame update
    void Start()
    {
        for (int i1 = 0; i1 <= 7; i1++) {
            if (i1 > 7) {
                return;
            }
            for (int i2 = 0; i2 <= 7; i2++) {
                values[i1,i2] = Random.Range(1,2);
            }
        }
        return;
    }

    // Update is called once per frame
    void Update()
    {
        return;
    }
}
