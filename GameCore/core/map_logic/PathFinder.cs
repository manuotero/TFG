
using System;
using System.Collections.Generic;
using System.Linq;

public static class PathFinder
{
    public static List<(int, int)> HallwayPathFinder(List<(int, int)> centerList)
    {
        var pointList = centerList.OrderBy( x => Guid.NewGuid()).ToList();
        var pathPointList = new List<(int, int)>();


        for(int i = 0; i < pointList.Count - 1; i++)
        {
            var roomA = pointList[i];
            var roomB = pointList[i+1];

            var xDistance = roomA.Item1 - roomB.Item1;
            var yDistance = roomA.Item2 - roomB.Item2;

            var actualPoint = roomA;

            if (Math.Abs(xDistance) > Math.Abs(yDistance))
            {
                while (xDistance != 0)
                {
                    if(xDistance > 0)
                    {
                        actualPoint.Item1--;
                        xDistance--;
                    } else if (xDistance < 0)
                    {
                        actualPoint.Item1++;
                        xDistance++;
                    }

                    pathPointList.Add(actualPoint);
                }
            }
            
            if (Math.Abs(xDistance) < Math.Abs(yDistance))
            {
                while (yDistance != 0)
                {
                    if(yDistance > 0)
                    {
                        actualPoint.Item2--;
                        yDistance--;
                    } else if (yDistance < 0)
                    {
                        actualPoint.Item2++;
                        yDistance++;
                    }

                    pathPointList.Add(actualPoint);
                }
            }
        }

        return pathPointList;
    }
}