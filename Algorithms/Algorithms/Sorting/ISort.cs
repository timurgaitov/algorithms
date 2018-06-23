using System;
using System.Collections.Generic;

namespace Algorithms.Sorting
{
    public interface ISort
    {
        void Sort<T>(IList<T> list) where T : IComparable<T>;
    }
}