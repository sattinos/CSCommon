using System;
using System.Collections.Generic;
using System.Reflection;

namespace CSCommon.Core.EnumStrings
{
    /// <summary>
    /// Use this class to access either the string value of an enum or vise versa
    /// The EnumDictionary caches the results in a forward/reverse dictionary to make the second call faster.
    /// Reflection overhead is only suffered once per enum
    /// </summary>
    public static class EnumDictionary
    {
        private static readonly Dictionary<Enum, string> _forwardDictionary = new Dictionary<Enum, string>();
        private static readonly Dictionary<string, Enum> _reverseDictionary = new Dictionary<string, Enum>();
        /// <summary>
        /// This method tries to find the description of an enum.
        /// It throws an exception if no description is associated with it.
        /// Make sure you associate a description to the enum value you pass before
        /// calling this method.
        /// </summary>
        /// <param name="value">an enum value declared previously</param>
        /// <returns>the description associated with the enum value</returns>
        public static string ToString(Enum value)
        {
            if( value == null)
            {
                throw new ArgumentNullException(nameof(value), $"{nameof(value)} cannot be null");
            }

            if (_forwardDictionary.ContainsKey(value))
            {
                return _forwardDictionary[value];
            }
            var descriptionAttribute = (DescriptionAttribute[])value.GetType().GetField(value.ToString()).GetCustomAttributes(DescriptionAttribute.CachedType);
            if (descriptionAttribute == null)
            {
                throw new Exception($"No description for value {value}");
            }

            var text = descriptionAttribute[0].DescriptiveText;
            _forwardDictionary.Add(value, text);
            if (!_reverseDictionary.ContainsKey(text))
            {
                _reverseDictionary.Add(text, value);
            }
            return descriptionAttribute[0].DescriptiveText;
        }

        /// <summary>
        /// Tries to find the enum value for a specific description.
        /// </summary>
        /// <param name="enumType">The enum type of the requested enum</param>
        /// <param name="value">The description of the needed enum value</param>
        /// <returns>The enum associated with the requested description</returns>
        public static object ToEnum(Type enumType, string value)
        {
            if (enumType == null)
            {
                throw new ArgumentNullException(nameof(enumType), $"{nameof(enumType)} cannot be null");
            }

            if (value == null)
            {
                throw new ArgumentNullException(nameof(value), $"{nameof(value)} cannot be null");
            }

            if (_reverseDictionary.ContainsKey(value))
            {
                return _reverseDictionary[value];
            }

            if (!enumType.IsEnum)
            {
                throw new Exception($"The type passed with value: {value} is not enum");
            }
            FieldInfo[] fieldsInfo = enumType.GetFields();
            foreach (var item in fieldsInfo)
            {
                DescriptionAttribute[] attributes = (DescriptionAttribute[])item.GetCustomAttributes(DescriptionAttribute.CachedType);
                if (attributes.Length > 0)
                {
                    if (attributes[0].DescriptiveText == value)
                    {
                        Enum enumValue = (Enum)Enum.Parse(enumType, value, false);
                        _reverseDictionary.Add(value, enumValue);
                        if( _forwardDictionary.ContainsKey(enumValue) )
                        {
                            _forwardDictionary.Add(enumValue, value);
                        }
                        return enumValue;
                    }
                }
            }
            return null;
        }
    }
}
