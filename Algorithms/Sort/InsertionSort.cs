namespace Algorithms.Sort
{
    public class InsertionSort : ISort
    {
        public void Sort<T>(IList<T> list) where T : IComparable<T>
        {
            for (var r = 1; r < list.Count; r++)
            {
                var current = list[r];
                
                for (var l = r - 1; l >= 0 && list[l].CompareTo(current) > 0; l--)
                {
                    list[l + 1] = list[l];
                    list[l] = current;
                }
            }
        }
    }
}