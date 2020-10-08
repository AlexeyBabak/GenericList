using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenericList
{
    public class Program
    {
        static void Main(string[] args)
        {
            var genericList = new GenericList<int>();
            var deletedItems = new GenericList<int>();

            for (int i = 1; i <= 100; i++)
            {
                genericList.Add(i);
            }
            genericList.ItemWasRemoved += ItemWasRemoved;

            Console.WriteLine($"Number of items in list: {genericList.Count}");
            Console.WriteLine($"List capacity: {genericList.Capacity}");
            for (int i = 0; i < genericList.Count; i++)
            {
                Console.WriteLine(genericList[i]);
            }

            genericList.ItemWasRemoved += delegate (int data)
            {
                deletedItems.Add(data);
            };

            genericList.RegisterOnMyFilter(NotEvenNumber);

            GenericList<int> newList = genericList.FilterList(genericList);

            Console.WriteLine("try");

            Console.WriteLine("Loop with foreach");
            foreach (var item in newList)
            {
                Console.WriteLine(item);
            }


            Console.WriteLine($"Removing index 4 with value {genericList[4]}");
            genericList.RemoveAtIndex(4);

            Console.WriteLine($"Number of items in list: {genericList.Count}");
            Console.WriteLine($"List capacity: {genericList.Capacity}");
            for (int i = 0; i < genericList.Count; i++)
            {
                Console.WriteLine(genericList[i]);
            }

 
            Console.ReadKey();

            void ItemWasRemoved<T>(T data)
            {
                Console.WriteLine($"Item {data} was removed from the list");
            }

            GenericList<int> NotEvenNumber(GenericList<int> list)
            {

                GenericList<int> newFilteredList = list;

                foreach (var item in newFilteredList)
                {
                    if ((int)item % 2 == 0)
                    {

                        list.RemoveAtIndex(2);
                    }
                }
                return newFilteredList;

            }
        }
    }
}
