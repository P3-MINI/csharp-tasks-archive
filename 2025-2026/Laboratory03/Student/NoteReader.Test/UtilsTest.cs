using System.IO.Compression;
using Xunit.Abstractions;
using Xunit.Sdk;

namespace NoteReader.Test
{
    public class UtilsTestSetupTeardown : IDisposable
    {
        public UtilsTestSetupTeardown()
        {
            if (Directory.Exists("non"))
                Directory.Delete("non", true);
            Directory.CreateDirectory("non");
            using var file1 = File.Create("non/archive");
            using var file2 = File.Create("non/zip.gzip");
            ZipFile.ExtractToDirectory("content.zip", "non/zipped");
        }
        public void Dispose()
        {
            Directory.Delete("non", true);
        }
    }
    public class UtilsTest : IClassFixture<UtilsTestSetupTeardown>
    {
        ITestOutputHelper output;
        public UtilsTest(ITestOutputHelper output)
        {
            this.output = output;
        }
        [Theory]
        [InlineData("./non/existent")]
        [InlineData("./non/existent.zip")]
        [InlineData("./non/archive")]
        [InlineData("./non/zip.gzip")]
        public void PrepareDirectory_Exception(string path)
        {
            Assert.Throws<FileSystemUtils.FileSystemException>(() => FileSystemUtils.PrepareDirectory(path));
        }
        [Fact]
        public void PrepareDirectory_Zip()
        {
            string? result = null;
            try
            {
                DateTime before = DateTime.Now;
                result = FileSystemUtils.PrepareDirectory("./content.zip");
                Assert.StartsWith(Path.GetTempPath(), result);
                Assert.EndsWith("content", result);
                Assert.True(Directory.Exists(result));
                Assert.InRange(Directory.GetCreationTime(result), before, DateTime.Now);
            }
            finally
            {
                if (result != null && Directory.Exists(result))
                    Directory.Delete(result, true);
            }
        }
        [Theory]
        [InlineData("..")]
        [InlineData("non")]
        public void PrepareDirectory_Directory(string path)
        {
            string result = FileSystemUtils.PrepareDirectory(path);
            Assert.Equal(path, result);
        }
        [Theory]
        [InlineData("non", "", 100)]
        [InlineData("non/zipped", "en.md", 49)]
        public void CountFiles_Count(string path, string end, int expected)
        {
            Assert.Equal(expected, FileSystemUtils.CountFiles(path, end));
        }
        [Fact]
        public void DirectoryWatcher_Tracking()
        {
            try
            {
                Directory.CreateDirectory("non/static");
                using DirectoryWatcher directoryWatcher = new("non/static");
                CancellationTokenSource raised = new();
                directoryWatcher.DirectoryChanged += (s, e) => { raised.Cancel(); };
                TimeSpan timeout = TimeSpan.FromMilliseconds(500);

                Assert.Empty(directoryWatcher.Files);
                Assert.Empty(directoryWatcher.Directories);

                Directory.CreateDirectory("non/static/dir");
                if (!raised.Token.WaitHandle.WaitOne(timeout))
                    Assert.Fail("Did not detect directory creation");
                raised = new();

                Assert.Empty(directoryWatcher.Files);
                Assert.Single(directoryWatcher.Directories);

                File.Create("non/static/file").Dispose();
                if (!raised.Token.WaitHandle.WaitOne(timeout))
                    Assert.Fail("Did not detect file creation");

                Assert.Single(directoryWatcher.Files);
            }
            finally
            {
                Directory.Delete("non/static", true);
            }
        }
        [Fact]
        public void DirectoryWatcher_Events()
        {
            try
            {
                Directory.CreateDirectory("non/static");
                using DirectoryWatcher directoryWatcher = new("non/static");
                CancellationTokenSource raised = new();
                directoryWatcher.DirectoryChanged += (s, e) => { raised.Cancel(); };
                TimeSpan timeout = TimeSpan.FromMilliseconds(100);

                Directory.CreateDirectory("non/static/dir");
                Assert.True(raised.Token.WaitHandle.WaitOne(timeout), "Event not raised on directory creation");
                raised = new();

                Directory.CreateDirectory("non/static/dir/nested");
                Assert.False(raised.Token.WaitHandle.WaitOne(timeout), "Event raised on nested directory creation");

                File.Create("non/static/dir/nested/file").Dispose();
                Assert.False(raised.Token.WaitHandle.WaitOne(timeout), "Event raised on nested file creation");

                File.Create("non/static/file").Dispose();
                Assert.True(raised.Token.WaitHandle.WaitOne(timeout), "Event not raised on file creation");
                raised = new();

                File.Move("non/static/file", "non/static/renamed");
                Assert.True(raised.Token.WaitHandle.WaitOne(timeout), "Event not raised on file renamed");
                raised = new();

                Directory.Move("non/static/dir", "non/static/renamedDir");
                Assert.True(raised.Token.WaitHandle.WaitOne(timeout), "Event not raised on directory renamed");
            }
            finally
            {
                Directory.Delete("non/static", true);
            }
        }
        [Fact]
        public void WatchDirectory_Content()
        {
            var o = Console.Out;
            StringWriter stringWriter = new();
            StringReader stringReader = new(Environment.NewLine);
            var path = "non/zipped";
            try
            {
                Console.SetOut(stringWriter);
                Console.SetIn(stringReader);

                FileSystemUtils.WatchDirectory(path);
                var lines = stringWriter.ToString().Split(Environment.NewLine);
                bool directoriesDone = false;
                foreach (var line in lines)
                {
                    if (string.IsNullOrEmpty(line))
                        continue;
                    var data = line.Split(":");
                    data[1] = Path.Combine(path, data[1]);
                    if (data[0] == "d")
                        if (directoriesDone)
                            Assert.Fail("Directory listed after file");
                        else
                            Assert.True(Directory.Exists(data[1]), $"Directory {data[1]} does not exist");
                    else if (data[0] == "f")
                    {
                        directoriesDone = true;
                        Assert.True(File.Exists(data[1]), $"File {data[1]} does not exist");
                    }
                    else
                        Assert.Fail($"Entry {line} in wrong format");
                }
            }
            finally
            {
                FileSystemUtils.Log?.Dispose();
                Console.SetOut(o);
            }
        }
    }
}