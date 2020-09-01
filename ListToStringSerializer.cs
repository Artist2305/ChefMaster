using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Dinner
{
    public static class ListToStringSerializer
    {
        public static string SerializeListToString(this List<string> listToSerialzie)
        {
            string result = String.Empty;
            foreach(var item in listToSerialzie)
            {
                result = String.Join("|", listToSerialzie);
            }
            return result;
        }
        public static List<string> DeserializeStringToList(this string stringToDeserialize)
        {
            string []wordsArray = stringToDeserialize.Split("|");
            List<string> result = new List<string>();

            foreach (var item in wordsArray)
            {
                result.Add(item);
            }
            return result;
        }
    }
}
