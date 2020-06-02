using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using SalesTaxesCalculation.Core;

namespace SalesTaxesCalculation
{

    public class FileRepository<T> : IRepository<T>
    {
        private readonly IMapper<T> _serializer;
        private readonly string _inputPath;
        private readonly string _errPath;
        private readonly string _backupPath;

        public FileRepository(IMapper<T> serializer)//, RepositoryConfig config)
        {
            _serializer = serializer;

            //todo to inject
            //_inputPath = config.InputPath;
            //_backupPath = config.BackupPath;
            //_errPath = config.ErrPath;
            _inputPath = "C:\\SalesTaxes\\Purchases\\";
            _backupPath = "C:\\SalesTaxes\\Backup\\";
            _errPath = "C:\\SalesTaxes\\Error\\";
        }

        public async Task<T> GetData()
        {
            try
            {
                var directoryInfo = GetDirectory();
                var files = directoryInfo.GetFiles();
                if (!files.Any())
                    throw new Exception($"No files on folder {_inputPath}");
                var myFile = files.OrderBy(f => f.LastWriteTime).First();
                var content = GetObjectFromFile(myFile);
                string destFileName = $"{_backupPath}{DateTime.Now:yyyyMMddHHmmss}_Purchase.json";
                myFile.MoveTo(destFileName);
                return content;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        private DirectoryInfo GetDirectory()
        {
            try
            {
                return new DirectoryInfo(_inputPath);
            }
            catch (Exception e)
            {
                throw new Exception($"Directory not found {_inputPath}", e);
            }
        }

        private T GetObjectFromFile(FileInfo file)
        {
            try
            {
                string text;
                using (var stream = file.OpenText())
                {
                    text = stream.ReadToEnd();
                }
                return _serializer.Map(text);
            }
            catch(MapperException)
            {
                file.MoveTo($"{_errPath}{DateTime.Now:yyyyMMddHHmmss}_ErrorPurchase.json");
                throw;
            }
            //catch (Exception e)
            //{

            //}
        }

        //private Task MoveToError(T data)
        //{
        //    throw new System.NotImplementedException();
        //}

        //private Task Backup(T data)
        //{
        //    throw new System.NotImplementedException();
        //}

    }


    public class RepositoryConfig
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


}
