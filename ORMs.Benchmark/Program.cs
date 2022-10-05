using BenchmarkDotNet.Running;
using ORMs.Benchmark.Benchmarks;

namespace ORMs.Benchmark
{
    class Program
    {
        static void Main(string[] args)
        {
            BenchmarkRunner.Run<BenchmarkUser>();
        }
    }
}
