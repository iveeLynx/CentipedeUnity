using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SegmentInfo
{
    public static ArrayList centipede;

    static SegmentInfo()
    {
        centipede = new ArrayList();
    }

    public static int AddSegment()
    {
        return segmentNumber++;
    }

    public int segmentCount = 0;
    public static int segmentNumber = 0;



}
