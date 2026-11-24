using System.Diagnostics;

namespace KT41_GarbageCollection
{
    class GCMonitor
    {
        public static void Measure(string name, Action action)
        {
            int gen0Before = GC.CollectionCount(0);
            int gen1Before = GC.CollectionCount(1);
            int gen2Before = GC.CollectionCount(2);

            Stopwatch sw = Stopwatch.StartNew();

            action();

            sw.Stop();

            int gen0 = GC.CollectionCount(0) - gen0Before;
            int gen1 = GC.CollectionCount(1) - gen1Before;
            int gen2 = GC.CollectionCount(2) - gen2Before;

            Console.WriteLine($"Метод: {name}");
            Console.WriteLine("-----------------------------------------------");
            Console.WriteLine("| Время (мс) | Gen0 | Gen1 | Gen2 |");
            Console.WriteLine("-----------------------------------------------");
            Console.WriteLine(
                $"| {sw.Elapsed.TotalMilliseconds,10:F2} | {gen0,4} | {gen1,4} | {gen2,4} |");
            Console.WriteLine("-----------------------------------------------");
        }

        public static void ForcedGC()
        {
            Stopwatch sw = Stopwatch.StartNew();

            GC.Collect(2, GCCollectionMode.Forced, blocking: true);

            sw.Stop();

            Console.WriteLine();
            Console.WriteLine("Принудительная блокирующая сборка мусора Gen2");
            Console.WriteLine($"Время сборки: {sw.Elapsed.TotalMilliseconds:F2} мс");
            Console.WriteLine($"Gen0: {GC.CollectionCount(0)}");
            Console.WriteLine($"Gen1: {GC.CollectionCount(1)}");
            Console.WriteLine($"Gen2: {GC.CollectionCount(2)}");
        }
    }
}


