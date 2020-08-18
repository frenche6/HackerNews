using System;
using System.ComponentModel;
using HackerNews.Models;

namespace HackerNews
{
    public static class Extensions
    {
        /// <summary>
        /// Used example on stack overflow as reference
        /// https://stackoverflow.com/questions/630803/associating-enums-with-strings-in-c-sharp
        /// </summary>
        /// <param name="type"></param>
        /// <returns>A string description value assigned to enum</returns>
        public static string GetLiveDataTypeDescription(this LiveDataType type)
        {
            DescriptionAttribute[] attributes = (DescriptionAttribute[]) type
                .GetType()
                .GetField(type.ToString())
                .GetCustomAttributes(typeof(DescriptionAttribute), false);
            return attributes.Length > 0 ? attributes[0].Description : String.Empty;
        }
    }
}