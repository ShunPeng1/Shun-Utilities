using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Shun_Utilities
{
    public static class LogExtensions
    {
        /// <summary>
        /// LogError with default header is the caller class name in string format
        /// </summary>
        /// <param name="o">the caller</param>
        /// <param name="message">message to be logged out</param>
        /// <param name="customHeader">custom header instead of default header</param>
        public static void LogError(this object o, string message, string customHeader = "")
        => Logger.LogError(message, customHeader: string.IsNullOrEmpty(customHeader) ? o.GetType().ToString() : string.Empty);
        /// <summary>
        /// Log with default header is the caller class name in string format
        /// </summary>
        /// <param name="o">the caller</param>
        /// <param name="message">message to be logged out</param>
        /// <param name="customHeader">custom header instead of default header</param>
        public static void Log(this object o, string message, string customHeader = "")
        => Logger.Log(message, customHeader: string.IsNullOrEmpty(customHeader) ? o.GetType().ToString() : string.Empty);
        /// <summary>
        /// Log with default header is the caller class name in string format and coloring the content
        /// </summary>
        /// <param name="o">the caller</param>
        /// <param name="message">message to be logged out</param>
        /// <param name="color">color in Color type (default is white)</param>
        /// <param name="customHeader">custom header instead of default header</param>
        public static void Log(this object o, string message, Color color, string customHeader = "")
        => Logger.Log(message, color, customHeader: string.IsNullOrEmpty(customHeader) ? o.GetType().ToString() : string.Empty);
        /// <summary>
        /// Log with default header is the caller class name in string format and coloring the content
        /// </summary>
        /// <param name="o">the caller</param>
        /// <param name="message">message to be logged out</param>
        /// <param name="color">color string in hex format (with or without "#")</param>
        /// <param name="customHeader">custom header instead of default header</param>
        public static void Log(this object o, string message, string colorHex, string customHeader = "")
        => Logger.Log(message, colorHex, customHeader: string.IsNullOrEmpty(customHeader) ? o.GetType().ToString() : string.Empty);
        /// <summary>
        /// LogWarning with default header is the caller class name in string format
        /// </summary>
        /// <param name="o">the caller</param>
        /// <param name="message">message to be logged out</param>
        /// <param name="customHeader">custom header instead of default header</param>
        public static void LogWarning(this object o, string message, string customHeader = "")
        => Logger.LogWarning(message, customHeader: string.IsNullOrEmpty(customHeader) ? o.GetType().ToString() : string.Empty);
        /// <summary>
        /// From IEnumerator type (List, HashSet, Queue, etc...), grab all its elements into "[]" seperated by comma ","
        /// </summary>
        /// <param name="list"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static string ListToString<T>(this IEnumerable<T> list)
        {
            string result = "[";
            int count = list.Count();
            if (count <= 0)
            {
                return "[]";
            }
            int i = 0;
            string lastItemString = string.Empty;
            foreach (var item in list)
            {
                if (i >= count - 1)
                {
                    lastItemString = item.ToString();
                    break;
                }
                result += item.ToString() + ", ";
                i++;
            }
            result += lastItemString + "]";
            return result;
        }
    }
}

