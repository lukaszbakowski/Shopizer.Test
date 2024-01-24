using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Newtonsoft.Json;
using Xunit.Sdk;
using System.Reflection;

namespace Shopizer.Test
{
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = true, Inherited = true)]
    public class JsonInlineData : DataAttribute
    {

        private readonly string _filePath;

        public JsonInlineData(string filePath)
        {
            string baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
            _filePath = $"{baseDirectory}{filePath}";
        }

        public override IEnumerable<object[]> GetData(MethodInfo testMethod)
        {
            if (testMethod == null)
                throw new ArgumentNullException(nameof(testMethod));

            if (!File.Exists(_filePath))
                throw new FileNotFoundException($"File not found: {_filePath}");

            var fileContent = File.ReadAllText(_filePath, Encoding.UTF8);
            var data = JsonConvert.DeserializeObject<List<object[]>>(fileContent);

            return data ?? throw new InvalidOperationException($"Unable to deserialize data from {_filePath}");
        }

    }
}
