using System.Collections.Generic;

namespace Assets.Scripts.Utility
{
    public static class ListExtension 
    {
        public static void Resize<T>(this List<T> list, int size)
        {
            if (list == null)
            {
                list = new List<T>();
            }

            if (list.Count == size)
            {
                return;
            }

            if (list.Count < size)
            {
                while (list.Count != size)
                {
                    list.Add(default(T));
                }
                return;;
            }

            if (list.Count > size)
            {
                while (list.Count != size)
                {
                    list.RemoveAt(list.Count - 1);
                }
                return; ;
            }
        }
    }
}
