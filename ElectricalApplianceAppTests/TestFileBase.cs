namespace ElectricalApplianceAppTests
{
    public class TestFileBase
    {
        protected string testFilePath;
        protected string inputPath;
        protected string file1Path;
        protected string file2Path;

        [TestInitialize]
        public void SetUp()
        {
            string temp = Path.GetTempPath();

            testFilePath = Path.GetTempFileName();
            inputPath = Path.Combine(temp, "Electrical appliance.txt");
            file1Path = Path.Combine(temp, "file1.txt");
            file2Path = Path.Combine(temp, "file2.txt");
        }

        [TestCleanup]
        public void TearDown()
        {
            foreach (var path in new[] { testFilePath, inputPath, file1Path, file2Path })
            {
                if (File.Exists(path))
                    File.Delete(path);
            }
        }
    }
}
