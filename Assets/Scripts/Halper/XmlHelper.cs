using System.Globalization;
using System.IO;
using System.Xml.Serialization;
using UnityEngine;

namespace Models.Managers
{
  public class XmlHelper
  {
    public static string GetPath(string filePath)
    {
      return $"{Application.persistentDataPath}/{filePath}.xml";
    }

    public static void Save<T>(T instance, string filePath) where T : class
    {
      var serializer = new XmlSerializer(typeof(T));

      using (var stream = new FileStream(GetPath(filePath), FileMode.Create))
      {
        serializer.Serialize(stream, instance);
      }
    }

    public static T Load<T>(string filePath) where T : class
    {
      var serializer = new XmlSerializer(typeof(T));
      var instance = default(T);

      using (var stream = new FileStream(GetPath(filePath), FileMode.Open))
      {
        instance = (T) serializer.Deserialize(stream);
      }

      return instance;
    }
  }
}