namespace Lab11_task2;


internal class Program
{
    public static int DoSomeWork(IBackgroundWorker w)
    {
        const int failProbability = 8;
        Random random = new Random();
        int progress = 0;

        while (progress < 100)
        {
            if (w.IsCancellationRequested)
            {
                throw new WorkerCanceled();
            }

            if (failProbability > random.Next(100))
            {
                throw new InvalidOperationException();
            }

            progress = Math.Min(100, progress + random.Next(5, 10));
            Thread.Sleep(random.Next(250, 400));
            w.ReportProgress(progress);
        }

        return random.Next(10);
    }

    record WorkerData
    {
        public int progress = 0;
    }

    public static void Main()
    {
        const int workerLimit = 25;
        Dictionary<SimpleBackgroundWorker<int>, WorkerData> workers = new Dictionary<SimpleBackgroundWorker<int>, WorkerData>();

        void ReportWorkerProgress(object? sender, int progress)
        {
            if (sender is SimpleBackgroundWorker<int> w)
            {
                workers[w].progress = progress;
                Redraw(workers);
            }
        }

        void ReportWorkerStatusChanged(object? sender, EventArgs args)
        {
            Redraw(workers);
        }

        SimpleBackgroundWorker<int> CreateNewWorker(int i)
        {
            var worker = new SimpleBackgroundWorker<int>();
            worker.SetDoWorkAction(DoSomeWork);
            worker.ProgressChanged += ReportWorkerProgress;
            worker.WorkerStatusChanged += ReportWorkerStatusChanged;
            worker.RunWorkerAsync($"Worker {i + 1}");
            return worker;
        }

        int workerCounter = 0;
        while (workerCounter < 3)
        {
            lock (workers)
            {
                workers.Add(CreateNewWorker(workerCounter++), new WorkerData { });
            }
        }

        Redraw(workers);

        while (true)
        {
            var key = Console.ReadKey();
            switch (key.KeyChar)
            {
                case 'n':
                    lock (workers)
                    {
                        if (workerCounter < workerLimit)
                            workers.Add(CreateNewWorker(workerCounter++), new WorkerData { });
                    }
                    break;
                case 'c':
                    lock (workers)
                    {
                        foreach (var worker in workers.Keys)
                        {
                            worker.Cancel();
                        }
                    }
                    break;
            }
        }

    }

    #region DrawMenuFunction
    static string DrawProgressBar(int progress, int total, int barSize)
    {
        double percentage = (double)progress / total;
        int filled = (int)Math.Round(percentage * barSize);

        char[] barChars = new char[barSize];
        for (int i = 0; i < barSize; i++)
        {
            barChars[i] = (i < filled) ? '#' : '-';
        }

        string percStr = $"{progress}%";
        int startPos = barSize / 2 - (percStr.Length / 2);

        for (int i = 0; i < percStr.Length && (startPos + i) < barSize; i++)
        {
            barChars[startPos + i] = percStr[i];
        }

        return $"[{new string(barChars)}]";
    }

    static void Redraw(Dictionary<SimpleBackgroundWorker<int>, WorkerData> workers)
    {
        lock (workers)
        {
            var sb = new System.Text.StringBuilder();
            char[] buffer = new char[Console.WindowHeight * Console.WindowWidth];
            Array.Fill(buffer, ' ');

            {
                var line = " n - Create new worker, c - Cancel all workers";
                line.CopyTo(0, buffer, 0, Math.Min(line.Length, Console.WindowWidth));
            }

            int i = 0;
            foreach (var (worker, workerData) in workers)
            {
                var line = $" {i + 1,2}. {worker.Name,-15} {DrawProgressBar(workerData.progress, 100, 50)} {worker.Status}";
                line.CopyTo(0, buffer, (i + 1) * Console.WindowWidth, Math.Min(line.Length, Console.WindowWidth));
                i++;
                if (i + 1 >= Console.WindowHeight)
                    break;
            }

            Console.SetCursorPosition(0, 0);
            Console.Write(new string(buffer));
        }
    }
    #endregion
}
