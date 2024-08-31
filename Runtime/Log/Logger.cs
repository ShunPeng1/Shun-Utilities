using System.Text.RegularExpressions;
using UnityEngine;

namespace Shun_Utilities
{
    public static class Logger
    {
        /// <summary>
        /// LogError with default header is "Logger"
        /// </summary>
        /// <param name="message">message to be logged out</param>
        /// <param name="customHeader">custom header instead of default header</param>
        public static void LogError(string message, string customHeader = "")
        {
            Debug.LogErrorFormat("<b>[{0}] {1}</b>", string.IsNullOrEmpty(customHeader) ? "Logger" : customHeader, message);
        }
        /// <summary>
        /// Log with default header is "Logger"
        /// </summary>
        /// <param name="message">message to be logged out</param>
        /// <param name="customHeader">custom header instead of default header</param>
        public static void Log(string message, string customHeader = "")
        {
            Debug.LogFormat("<b>[{0}] {1}</b>", string.IsNullOrEmpty(customHeader) ? "Logger" : customHeader, message);
        }
        /// <summary>
        /// Log with default header is "Logger" and coloring the content
        /// </summary>
        /// <param name="message">message to be logged out</param>
        /// <param name="colorHex">color in Color type (default is white)</param>
        /// <param name="customHeader">custom header instead of default header</param>
        public static void Log(string message, Color color, string customHeader = "")
        {
            string colorString = "#" + ColorUtility.ToHtmlStringRGBA(color);
            Debug.LogFormat("<b>[{0}] <color={1}>{2}</color></b>", string.IsNullOrEmpty(customHeader) ? "Logger" : customHeader, colorString, message);
        }
        /// <summary>
        /// Log with default header is "Logger" and coloring the content
        /// </summary>
        /// <param name="message">message to be logged out</param>
        /// <param name="colorHex">color string in hex format (with or without "#")</param>
        /// <param name="customHeader">custom header instead of default header</param>
        public static void Log(string message, string colorHex, string customHeader = "")
        {
            string defaultColor = "#" + ColorUtility.ToHtmlStringRGBA(Color.white);
            if (!colorHex.StartsWith("#"))
                colorHex = "#" + colorHex;
            bool isValidHexString = Regex.IsMatch(colorHex, @"[#][0-9A-Fa-f]{6}\b");
            if (!isValidHexString)
                colorHex = defaultColor;
            Debug.LogFormat("<b>[{0}] <color={1}>{2}</color></b>", string.IsNullOrEmpty(customHeader) ? "Logger" : customHeader, colorHex, message);
        }
        /// <summary>
        /// LogWarning with default header is "Logger"
        /// </summary>
        /// <param name="message">message to be logged out</param>
        /// <param name="customHeader">custom header instead of default header</param>
        public static void LogWarning(string message, string customHeader = "")
        {
            Debug.LogWarningFormat("<b>[{0}] {1}</b>", string.IsNullOrEmpty(customHeader) ? "Logger" : customHeader, message);
        }
    }
}