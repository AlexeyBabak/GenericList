using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GenericList
{
    public class GenericList<T> : IEnumerable<T>
    {
        private T[] items;
        private const int InitialSize = 2;

        public int Count { get; private set; }
        public int Capacity => items.Length;

        public T this[int index]
        {
            get { return items[index]; }
            set { items[index] = value; }
        }

        public void Add(T value)
        {
            CheckCapacity(Count + 1);
            items[Count] = value;
            Count++;
        }

        public void RemoveAtIndex(int index)
        {
            if (index >= Count)
            {
                throw new IndexOutOfRangeException();
            }

            T[] left = items.Slice(0, index);
            T[] right = items.Slice(index + 1, items.Length - 1 - index);

            T[] slice = new T[items.Length];

            // Add left slice
            Array.Copy(left, slice, left.Length);

            // Add right slice
            for (int i = 0; i < right.Length - 1; i++)
            {
                slice[index + i] = right[i];
            }

            items = slice;
            Count--;
        }

        private void CheckCapacity(int size)
        {
            // Initialize array
            if (items == null)
            {
                items = new T[InitialSize];
            }

            while (items.Length < size)
            {
                Array.Resize(ref items, items.Length * 2);
            }
        }


        public delegate GenericList<T> FilterDelegate(GenericList<T> list);

        private FilterDelegate filter;
        public void RegisterOnMyFilter(FilterDelegate filter)
        {
            this.filter += filter;
        }


        public GenericList<T> FilterList(GenericList<T> list)
        {
            return filter?.Invoke(list);
        }

        public event Action<T> ItemWasRemoved;


        public IEnumerator<T> GetEnumerator()
        {
            int count = 0;

            foreach (T item in items)
            {
                if (count >= Count)
                {
                    break;
                }

                count++;
                yield return item;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }


    }
}
