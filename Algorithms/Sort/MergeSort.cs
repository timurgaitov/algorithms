namespace Algorithms.Sort
{
    public class MergeSort : ISort
    {
        public void Sort<T>(IList<T> list) where T : IComparable<T>
        {
            Sort(list, 0, list.Count - 1);
        }

        private static void Sort<T>(IList<T> list, int p, int r) where T : IComparable<T>
        {
            if (p < r)
            {
                var q = (p + r) / 2;
                
                Sort(list, p, q);
                Sort(list, q + 1, r);
                Merge(list, p, q, r);
            }
        }

        private static void Merge<T>(IList<T> list, int p, int q, int r) where T : IComparable<T>
        {
            var left = new T[q - p + 1];
            var right = new T[r - q];

            int i, j;
            
            for (i = 0; i < left.Length; i++)
            {
                left[i] = list[p + i];
            }
            
            for (j = 0; j < right.Length; j++)
            {
                right[j] = list[q + j + 1];
            }

            i = 0;
            j = 0;

            for (var k = p; k <= r; k++)
            {
                if (i >= left.Length)
                {
                    list[k] = right[j];
                    j++;
                }
                else if (j >= right.Length)
                {
                    list[k] = left[i];
                    i++;
                }
                else if (left[i].CompareTo(right[j]) <= 0)
                {
                    list[k] = left[i];
                    i++;
                }
                else
                {
                    list[k] = right[j];
                    j++;
                }
            }
        }
    }
}