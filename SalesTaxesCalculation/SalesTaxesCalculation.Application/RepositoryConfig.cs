namespace SalesTaxesCalculation.Application
{
    public interface IDataAccessConfiguration
    {
        string InputPath { get; }
        string BackupPath { get; }
        string ErrPath { get; }
    }
    public class RepositoryConfig : IDataAccessConfiguration
    {
        public RepositoryConfig(string inputPath, string backupPath, string errPath)
        {
            InputPath = inputPath;
            BackupPath = backupPath;
            ErrPath = errPath;
        }

        public string InputPath { get; }
        public string BackupPath { get; }
        public string ErrPath { get; }
    }

    public class FileSystemConfiguration
    {
        public string InputPath { get; set; }
        public string BackupPath { get; set; }
        public string ErrPath { get; set; }
    }


}
