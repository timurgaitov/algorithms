namespace Algorithms.Sort
{
    public class QuickSort : ISort
    {
        public void Sort<T>(IList<T> list) 
            where T : IComparable<T>
        {
            Q(list, 0, list.Count - 1);
        }
        
        private static void Q<T>(IList<T> list, int l, int r)
            where T : IComparable<T>
        {
            if (l >= r)
            {
                return;
            }

            int p = (r + l) / 2;
            T pval = list[p];

            int i = l - 1;
            int j = r + 1;
            
            while (true)
            {
                do
                {
                    i++;
                } while (list[i].CompareTo(pval) < 0);

                do
                {
                    j--;
                } while (list[j].CompareTo(pval) > 0);
                
                if (i >= j)
                {
                    break;
                }

                (list[i], list[j]) = (list[j], list[i]);
            }
            
            Q(list, l, j);
            Q(list, j + 1, r);
        }
    }
}