namespace SalesTaxesCalculation.Application
{
    public class OutputConfiguration
    {
        public string Path { get; set; }
    }

    public class FileSystemConfiguration
    {
        public string InputPath { get; set; }
        public string BackupPath { get; set; }
        public string ErrPath { get; set; }
    }


}
