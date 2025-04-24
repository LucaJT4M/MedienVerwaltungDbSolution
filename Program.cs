using System.Diagnostics;
using medienVerwaltungDbSolution;
Stopwatch watch = new();

watch.Start();
_ = new MainFunction();
watch.Stop();
System.Console.WriteLine("Finished in: " + watch.Elapsed.TotalSeconds);