namespace KT41_GarbageCollection
{
    class Program
    {
        static List<byte[]> longLived = new List<byte[]>();
        static List<byte[]> heavyLoh = new List<byte[]>();

        static void Main()
        {
            while (true)
            {
                Console.WriteLine("========== МЕНЮ ==========");
                Console.WriteLine("1. ShortLived()");
                Console.WriteLine("2. LongLived()");
                Console.WriteLine("3. HeavyLoh()");
                Console.WriteLine("4. Выполнить все проверки");
                Console.WriteLine("5. Принудительный GC.Collect()");
                Console.WriteLine("0. Выход");
                Console.Write("Выберите действие: ");

                string input = Console.ReadLine()!;

                if (!int.TryParse(input, out int choice))
                {
                    Console.WriteLine("Ошибка! Введите целое число.");
                    continue;
                }

                try
                {
                    switch (choice)
                    {
                        case 1:
                            GCMonitor.Measure("ShortLived", () => ShortLived());
                            break;

                        case 2:
                            GCMonitor.Measure("LongLived", () => LongLived());
                            Console.WriteLine($"Объектов в живых: {longLived.Count}");
                            break;

                        case 3:
                            GCMonitor.Measure("HeavyLoh", () => HeavyLoh());
                            Console.WriteLine($"Объектов в живых: {heavyLoh.Count}");
                            break;

                        case 4:
                            GCMonitor.Measure("ShortLived", () => ShortLived());

                            GCMonitor.Measure("LongLived", () => LongLived());
                            Console.WriteLine($"Объектов в живых: {longLived.Count}");

                            GCMonitor.Measure("HeavyLoh", () => HeavyLoh());
                            Console.WriteLine($"Объектов в живых: {heavyLoh.Count}");
                            break;

                        case 5:
                            GCMonitor.ForcedGC();
                            break;

                        case 0:
                            Console.WriteLine("Программа завершена.");
                            return;

                        default:
                            Console.WriteLine("Такого пункта нет. Попробуйте ещё раз.");
                            break;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Ошибка выполнения: {ex.Message}");
                }

                Console.WriteLine();
            }
        }

        static void ShortLived()
        {
            for (int i = 0; i < 3_000_000; i++)
            {
                byte[] data = new byte[200];
                _ = data;
            }
        }

        static void LongLived()
        {
            for (int i = 0; i < 50_000; i++)
            {
                longLived.Add(new byte[512]);
            }
        }

        static void HeavyLoh()
        {
            for (int i = 0; i < 1_000; i++)
            {
                heavyLoh.Add(new byte[100_000]);
            }
        }
    }
}
