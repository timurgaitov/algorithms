namespace Algorithms.Sort
{
    public interface ISort
    {
        void Sort<T>(IList<T> list) where T : IComparable<T>;
    }
}