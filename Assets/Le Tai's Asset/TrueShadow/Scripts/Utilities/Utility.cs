using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;


namespace LeTai
{
    public static class Utility
    {
        public static void LogList<T>(IEnumerable<T> list, Func<T, object> getData)
        {
            StringBuilder sb = new StringBuilder();

            int i = 0;
            foreach (T el in list)
            {
                sb.Append(i + ":    ");
                sb.Append(getData(el).ToString());
                sb.Append("\n");
                i++;
            }

            Debug.Log(sb.ToString());
        }

        public static int SimplePingPong(int t, int max)
        {
            if (t > max)
                return 2 * max - t;
            return t;
        }
    }
}
