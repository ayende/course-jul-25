using System;
using System.IO;
using System.Reflection;
using System.Runtime.Serialization.Formatters.Binary;
using NHibernate;
using NHibernate.Cfg;

namespace MediaMind.Course.Controllers
{
    public class ConfigurationSaver
    {
        private static string SerializedConfiguration =
            Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "configurtion.serialized");

        public static bool IsConfigurationFileValid
        {
            get
            {
                //var ass = Assembly.GetCallingAssembly();
                var configInfo = new FileInfo(SerializedConfiguration);
                return configInfo.Exists;
                //var assInfo = new FileInfo(ass.Location);
                //if (configInfo.LastWriteTime < assInfo.LastWriteTime)
                //    return false;
                //return true;
            }
        }

        public static void SaveConfigurationToFile(Configuration configuration)
        {
            using (var file = File.Open(SerializedConfiguration, FileMode.Create))
            {
                var bf = new BinaryFormatter();
                bf.Serialize(file, configuration);
            }
        }

        public static Configuration LoadConfigurationFromFile()
        {
            if (IsConfigurationFileValid == false)
                return null;
            try
            {
                using (var file = File.Open(SerializedConfiguration, FileMode.Open))
                {
                    var bf = new BinaryFormatter();
                    return bf.Deserialize(file) as Configuration;
                }
            }
            catch (Exception)
            {
                return null;
            }

        }
    }
}