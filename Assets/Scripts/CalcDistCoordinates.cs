using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public  static class CalcDistCoordinates
{

    public static int getDistWithCoordinates(int x1, int y1, int z1, int x2, int y2, int z2)
    {
        int distance = 0;
        distance += Mathf.Abs(x1 - x2)* Mathf.Abs(x1 - x2);
        distance += Mathf.Abs(y1 - y2)* Mathf.Abs(y1 - y2);
        distance += Mathf.Abs(z1 - z2)* Mathf.Abs(z1 - z2);
        return (int)Mathf.Sqrt(distance);
    }

}
