namespace Algorithms.Sort
{
    public class MergeSortBottomUp : ISort
    {
        public void Sort<T>(IList<T> list) where T : IComparable<T>
        {
            var buffer = new T[list.Count];
            
            for (var width = 1; width < list.Count; width *= 2)
            {
                for (var i = 0; i < list.Count; i += 2 * width)
                {
                    Merge(list, i, Math.Min(i + width, list.Count), Math.Min(i + 2 * width, list.Count), buffer);
                }
                
                Copy(list, buffer);
            }
        }

        private static void Merge<T>(IList<T> list, int left, int right, int end, IList<T> buffer) where T : IComparable<T>
        {
            var l = left;
            var r = right;
            
            for (var k = left; k < end; k++)
            {
                if (l < right && (r >= end || list[l].CompareTo(list[r]) <= 0))
                {
                    buffer[k] = list[l];
                    l++;
                }
                else
                {
                    buffer[k] = list[r];
                    r++;
                }
            }
        }

        private static void Copy<T>(IList<T> list, IList<T> buffer) where T : IComparable<T>
        {
            for (var i = 0; i < buffer.Count; i++)
            {
                list[i] = buffer[i];
            }
        }
    }
}