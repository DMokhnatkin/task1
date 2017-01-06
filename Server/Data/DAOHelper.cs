using System;
using System.IO;
using System.Runtime.Serialization;

namespace Server.Data
{
    internal static class DAOHelper
    {
        internal static byte[] ObjectToByteArray(object obj)
        {
            DataContractSerializer serializer = new DataContractSerializer(typeof(object));

            using (var ms = new MemoryStream())
            {
                serializer.WriteObject(ms, obj);
                return ms.ToArray();
            }
        }

        internal static object ByteArrayToObject(Type destType, byte[] bytes)
        {
            DataContractSerializer serializer = new DataContractSerializer(destType);
            using (var ms = new MemoryStream(bytes))
            {
                return serializer.ReadObject(ms);
            }
        }
    }
}
