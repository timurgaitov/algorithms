using System.Collections.ObjectModel;
using System.Collections.Specialized;
using Xunit;
using Xunit.Abstractions;

namespace Algorithms.Sort
{
    public class Tests
    {
        private readonly ITestOutputHelper _out;

        public Tests(
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

        [Fact]
        public void Test_MergeSortBottomUp()
        {
            TestImpl(new MergeSortBottomUp());
        }
        
        private void TestImpl(ISort sort)
        {
            var source = new ObservableCollection<ValueIndex>
            {
                new ValueIndex(4, '¹'),
                new ValueIndex(5),
                new ValueIndex(2),
                new ValueIndex(4, '²'),
                new ValueIndex(6),
                new ValueIndex(1),
                new ValueIndex(3),
                new ValueIndex(4, '³'),
                new ValueIndex(0)
            };
     
            TrackChanges(source, out var changes);
            
            changes.Add(ToStringIfStable(source));

            sort.Sort(source);

            var expected = new[]
            {
                new ValueIndex(0),
                new ValueIndex(1),
                new ValueIndex(2),
                new ValueIndex(3),
                new ValueIndex(4, '¹'),
                new ValueIndex(4, '²'),
                new ValueIndex(4, '³'),
                new ValueIndex(5),
                new ValueIndex(6)
            };
            
            string lastChange = null;
            
            foreach (var change in changes)
            {
                if (change == null || change == lastChange)
                {
                    continue;
                }

                lastChange = change;
                
                _out.WriteLine(change);
            }
            
            Assert.Equal(expected, source);
        }

        private static void TrackChanges<T>(T source, out IList<string> changes) where T 
            : class, INotifyCollectionChanged, ICollection<ValueIndex>
        {
            changes = new List<string>();

            var changesRef = changes;
            
            source.CollectionChanged +=
                (sender, args) => changesRef.Add(ToStringIfStable((T) sender));
        }

        private static string ToStringIfStable(ICollection<ValueIndex> state)
        {
            if (state.GroupBy(i => i).Any(g => g.Count() > 1))
            {
                return null;
            }
            
            return string.Join("\t", state);
        }

        private struct ValueIndex : IComparable<ValueIndex>
        {
            private readonly int _value;
            private readonly char? _index;

            public ValueIndex(int value, char? index = null)
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
                    ? $"{_value}{_index}" 
                    : $"{_value}";
            }
        }
    }
}