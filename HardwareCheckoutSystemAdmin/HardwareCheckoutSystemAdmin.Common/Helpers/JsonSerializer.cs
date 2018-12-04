using System.IO;
using Newtonsoft.Json;

namespace HardwareCheckoutSystemAdmin.Common.Helpers
{
  public static class JsonSerializer<T>
  {
    public static T JsonReadFile(string path)
    {
      if (File.Exists(path))
      {
        string filetext = File.ReadAllText(path);
        return JsonConvert.DeserializeObject<T>(filetext);
      }
      else throw new FileNotFoundException();
    }

    public static void JsonWriteToFile(string path, T obj)
    {
      string filetext = JsonConvert.SerializeObject(obj, Formatting.Indented);
      File.WriteAllText(path, filetext);
    }
  }
}
