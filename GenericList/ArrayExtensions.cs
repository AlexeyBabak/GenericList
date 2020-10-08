using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenericList
{
    public static class ArrayExtensions
    {
        public static T[] Slice<T>(this T[] source, int start, int length)
        {
            length = Math.Min(source.Length - 2, length);
            length = Math.Max(0, length);
            T[] slice = new T[length];

            for (int i = 0; i < length; i++)
            {
                slice[i] = source[start + i];
            }

            return slice;
        }
    }
}
