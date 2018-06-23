using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Globalization;
using System.Linq;
using Algorithms.Sorting;
using Xunit;
using Xunit.Abstractions;

namespace Algorithms.Tests.Sorting
{
    public class SortTests
    {
        private readonly ITestOutputHelper _out;

        public SortTests(
            ITestOutputHelper @out)
        {
            _out = @out;
        }

        [Fact]
        public void Test_InsertionSort()
        {
            TestImpl(new InsertionSort());
        }
        
        [Fact]
        public void Test_MergeSort()
        {
            TestImpl(new MergeSort());
        }
        
        private void TestImpl(ISort sort)
        {
            var source = new ObservableCollection<ValueIndex>
            {
                new ValueIndex(4, 0),
                new ValueIndex(5),
                new ValueIndex(2),
                new ValueIndex(4, 3),
                new ValueIndex(6),
                new ValueIndex(1),
                new ValueIndex(3),
                new ValueIndex(4, 7),
                new ValueIndex(0)
            };

            PrintIfStable(source);
            
            TrackChanges(source);

            sort.Sort(source);

            var expected = new[]
            {
                new ValueIndex(0),
                new ValueIndex(1),
                new ValueIndex(2),
                new ValueIndex(3),
                new ValueIndex(4, 0),
                new ValueIndex(4, 3),
                new ValueIndex(4, 7),
                new ValueIndex(5),
                new ValueIndex(6)
            };
            
            Assert.Equal(expected, source);
        }

        private void TrackChanges<T>(T source) where T 
            : class, INotifyCollectionChanged, ICollection<ValueIndex>
        {
            source.CollectionChanged +=
                (sender, args) => PrintIfStable((T) sender);
        }

        private void PrintIfStable(ICollection<ValueIndex> state)
        {
            if (state.GroupBy(i => i).Any(g => g.Count() > 1))
            {
                return;
            }
            
            _out.WriteLine(string.Join("\t", state));
        }

        private struct ValueIndex : IComparable<ValueIndex>
        {
            private readonly int _value;
            private readonly int? _index;

            public ValueIndex(int value, int? index = null)
            {
                _value = value;
                _index = index;
            }

            public int CompareTo(ValueIndex other)
            {
                return _value.CompareTo(other._value);
            }

            public override string ToString()
            {
                return _index.HasValue 
                    ? $"{_value}:{_index}" 
                    : $"{_value}";
            }
        }
    }
}