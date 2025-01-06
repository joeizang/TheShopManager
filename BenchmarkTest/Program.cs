
using BenchmarkDotNet.Running;
using BenchmarkTest;

Console.WriteLine("Hi");

BenchmarkRunner.Run<CategoryBenchmarks>();