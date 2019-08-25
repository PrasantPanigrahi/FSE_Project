using NBench;
using NUnit.Framework;
using PM.Utilities.Filter;
using System;
using System.Collections.Generic;

namespace PM.Web.Tests
{
    [TestFixture]
    public class TestFilterStateHelper
    {
        [Test]
        [PerfBenchmark(NumberOfIterations = 500, RunMode = RunMode.Throughput,
            TestMode = TestMode.Test, SkipWarmups = true, RunTimeMilliseconds = 6000)]
        [ElapsedTimeAssertion(MaxTimeMilliseconds = 5000)]
        [MemoryMeasurement(MemoryMetric.TotalBytesAllocated)]
        [TimingMeasurement]
        public void ToOperatorStr_Test()
        {
            //arrange
            var containsOperator = FilterOperator.Contains;
            var lessThanOperator = FilterOperator.LessThanEqual;
            var greaterThanOperator = FilterOperator.GreaterThanEqual;
            var eqOperator = FilterOperator.EqualTo;
            var invalidOperator = FilterOperator.Undefined;

            //act
            var containsOperatorStr = containsOperator.ToOperatorStr();
            var lessThanOperatorStr = lessThanOperator.ToOperatorStr();
            var greaterThanOperatorStr = greaterThanOperator.ToOperatorStr();
            var eqOperatorStr = eqOperator.ToOperatorStr();

            //assert
            Assert.AreEqual("contains", containsOperatorStr);
            Assert.AreEqual("lte", lessThanOperatorStr);
            Assert.AreEqual("gte", greaterThanOperatorStr);
            Assert.AreEqual("eq", eqOperatorStr);

            Assert.Throws<NotImplementedException>(() => invalidOperator.ToOperatorStr());
        }

        [Test]
        [PerfBenchmark(NumberOfIterations = 500, RunMode = RunMode.Throughput,
            TestMode = TestMode.Test, SkipWarmups = true, RunTimeMilliseconds = 6000)]
        [ElapsedTimeAssertion(MaxTimeMilliseconds = 5000)]
        [MemoryMeasurement(MemoryMetric.TotalBytesAllocated)]
        [TimingMeasurement]
        public void FilterDescriptor_Test()
        {
            //arrange
            var containsOperator = new FilterDescriptor() { Operator = "contains" };
            var lessThanOperator = new FilterDescriptor() { Operator = "lte" };
            var greaterThanOperator = new FilterDescriptor() { Operator = "gte" };
            var eqOperator = new FilterDescriptor() { Operator = "eq" };
            var neqOperator = new FilterDescriptor() { Operator = "neq" };
            var invalidOperator = new FilterDescriptor() { Operator = "test" };
            Func<FilterOperator> func = () => { return invalidOperator.FilterOperator; };

            //assert
            Assert.AreEqual(FilterOperator.Contains, containsOperator.FilterOperator);
            Assert.AreEqual(FilterOperator.LessThanEqual, lessThanOperator.FilterOperator);
            Assert.AreEqual(FilterOperator.GreaterThanEqual, greaterThanOperator.FilterOperator);
            Assert.AreEqual(FilterOperator.EqualTo, eqOperator.FilterOperator);
            Assert.AreEqual(FilterOperator.NotEqualTo, neqOperator.FilterOperator);
            Assert.Throws<NotImplementedException>(() => func());
        }

        [Test]
        [PerfBenchmark(NumberOfIterations = 500, RunMode = RunMode.Throughput,
            TestMode = TestMode.Test, SkipWarmups = true, RunTimeMilliseconds = 6000)]
        [ElapsedTimeAssertion(MaxTimeMilliseconds = 5000)]
        [MemoryMeasurement(MemoryMetric.TotalBytesAllocated)]
        [TimingMeasurement]
        public void SortDescriptor_Test()
        {
            //arrange
            var ascSortDescriptor = new SortDescriptor() { Dir = "asc" };
            var descSortDescriptor = new SortDescriptor() { Dir = "desc" };
            var emptySortDescriptor = new SortDescriptor() { Dir = "" };
            var invalidSortDescriptor = new SortDescriptor() { Dir = "test " };
            Func<SortDirection> func = () => { return invalidSortDescriptor.Direction; };

            //assert
            Assert.AreEqual(SortDirection.ASC, ascSortDescriptor.Direction);
            Assert.AreEqual(SortDirection.DSC, descSortDescriptor.Direction);
            Assert.AreEqual(SortDirection.Undefined, emptySortDescriptor.Direction);
            Assert.Throws<InvalidOperationException>(() => func());
        }

        [Test]
        [PerfBenchmark(NumberOfIterations = 500, RunMode = RunMode.Throughput,
             TestMode = TestMode.Test, SkipWarmups = true, RunTimeMilliseconds = 6000)]
        [ElapsedTimeAssertion(MaxTimeMilliseconds = 5000)]
        [MemoryMeasurement(MemoryMetric.TotalBytesAllocated)]
        [TimingMeasurement]
        public void AddFilter_Test()
        {
            //arrange
            var filterState = new FilterState();

            //act
            filterState.AddFilter("priority", 1, FilterOperator.EqualTo);

            //assert
            Assert.NotNull(filterState.Filter.Filters[0]);
        }

        [Test]
        [PerfBenchmark(NumberOfIterations = 500, RunMode = RunMode.Throughput,
            TestMode = TestMode.Test, SkipWarmups = true, RunTimeMilliseconds = 6000)]
        [ElapsedTimeAssertion(MaxTimeMilliseconds = 5000)]
        [MemoryMeasurement(MemoryMetric.TotalBytesAllocated)]
        [TimingMeasurement]
        public void AddFilter_ShouldThrowErrorIfAddingSameFilter()
        {
            //arrange
            var filterState = new FilterState();

            //act
            filterState.AddFilter("priority", 1, FilterOperator.EqualTo);

            //assert
            Assert.NotNull(filterState.Filter.Filters[0]);
            Assert.Throws<InvalidOperationException>(() => filterState.AddFilter("priority", 1, FilterOperator.EqualTo));
        }

        [Test]
        [PerfBenchmark(NumberOfIterations = 500, RunMode = RunMode.Throughput,
            TestMode = TestMode.Test, SkipWarmups = true, RunTimeMilliseconds = 6000)]
        [ElapsedTimeAssertion(MaxTimeMilliseconds = 5000)]
        [MemoryMeasurement(MemoryMetric.TotalBytesAllocated)]
        [TimingMeasurement]
        public void SearchFilterDescriptors_Test()
        {
            //arrange
            CompositeFilterDescriptor compositeFilter = null;

            //act
            var result = FilterStateHelper.SearchFilterDescriptors(compositeFilter, "priority");

            //assert
            Assert.NotNull(result);
        }

        [Test]
        [PerfBenchmark(NumberOfIterations = 500, RunMode = RunMode.Throughput,
             TestMode = TestMode.Test, SkipWarmups = true, RunTimeMilliseconds = 6000)]
        [ElapsedTimeAssertion(MaxTimeMilliseconds = 5000)]
        [MemoryMeasurement(MemoryMetric.TotalBytesAllocated)]
        [TimingMeasurement]
        public void FlattenCompositeFilterDescriptor_ShouldThrowErrorIfUnsupportedOperatorUsed()
        {
            //arrange
            var compositeFilter = new CompositeFilterDescriptor() { Filters = new List<dynamic>() };

            //act
            compositeFilter.Logic = "OR";

            //assert
            Assert.Throws<NotImplementedException>(() => FilterStateHelper.FlattenCompositeFilterDescriptor(compositeFilter));
        }

        [Test]
        [PerfBenchmark(NumberOfIterations = 500, RunMode = RunMode.Throughput,
             TestMode = TestMode.Test, SkipWarmups = true, RunTimeMilliseconds = 6000)]
        [ElapsedTimeAssertion(MaxTimeMilliseconds = 5000)]
        [MemoryMeasurement(MemoryMetric.TotalBytesAllocated)]
        [TimingMeasurement]
        public void FlattenCompositeFilterDescriptor_Test()
        {
            //arrange
            var compositeFilter = new CompositeFilterDescriptor() { Filters = new List<dynamic>() { new { logic = "and" } } };

            //act
            compositeFilter.Logic = "and";

            //assert
            Assert.Throws<Newtonsoft.Json.JsonReaderException>(() => FilterStateHelper.FlattenCompositeFilterDescriptor(compositeFilter));
        }
    }
}