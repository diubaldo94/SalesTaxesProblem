using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using SalesTaxesCalculation.Core;

namespace SalesTaxesCalculation.Application
{

    public class FileRepository<T> : IRepository<T>
    {
        private readonly IMapper<T> _serializer;
        private readonly string _inputPath;
        private readonly string _errPath;
        private readonly string _backupPath;

        public FileRepository(IMapper<T> serializer, IOptions<FileSystemConfiguration> dataAccessConfiguration)
        {
            _serializer = serializer;
            _inputPath = dataAccessConfiguration.Value.InputPath; 
            _backupPath = dataAccessConfiguration.Value.BackupPath; 
            _errPath = dataAccessConfiguration.Value.ErrPath; 
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
                string destFileName = Path.Combine(_backupPath, GetFilenameToApply());
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
                throw new Exception($"Input directory not found {_inputPath} : {e.Message}", e);
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
            catch (MapperException)
            {
                file.MoveTo(Path.Combine(_errPath, GetFilenameToApply()));
                throw;
            }
        }

        private static string GetFilenameToApply()
        {
            return $"{DateTime.Now:yyyyMMddHHmmss}_Purchase.json";
        }
    }
}
