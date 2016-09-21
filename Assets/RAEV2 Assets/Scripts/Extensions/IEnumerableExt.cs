using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Extensions
{
    public static class IEnumerableExt
    {
        

        /// <summary>
        /// http://stackoverflow.com/a/1262619
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        public static void Shuffle<T>(this IList<T> list)
        {
            int n = list.Count;
            while (n > 1)
            {
                n--;
                int k = Random.Range(0, n + 1);
                T value = list[k];
                list[k] = list[n];
                list[n] = value;
            }
        }
    }
}
