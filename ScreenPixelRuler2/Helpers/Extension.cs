using System;
using System.Collections.Generic;

namespace ScreenPixelRuler2
{
    static class Extension
    {
        public static int ClosestTo(this IEnumerable<int> collection, int target)
        {
            // NB Method will return int.MaxValue for a sequence containing no elements.
            // Apply any defensive coding here as necessary.
            int closest = int.MaxValue;
            int minDifference = int.MaxValue;
            foreach (int element in collection)
            {
                long difference = Math.Abs((long)element - target);
                if (minDifference > difference)
                {
                    minDifference = (int)difference;
                    closest = element;
                }
            }
            return closest;
        }
    }
}
