using System;

namespace CSCommon.Core.EnumStrings
{
    /// <summary>
    /// Description attribute that can be used to associate string values to an enum.
    /// It is meant only to be used in Enum fields
    /// </summary>
    [System.AttributeUsage(AttributeTargets.Field, Inherited = false, AllowMultiple = false)]
    public sealed class DescriptionAttribute : Attribute
    {
        /// <value>Cached type for avoiding calling typeof more than once.</value>
        public static readonly Type CachedType = typeof(DescriptionAttribute);
        /// <summary>
        /// Only one positional property is required
        /// </summary>
        /// <param name="descriptiveText">the description text to be associated with the enum value</param>
        public DescriptionAttribute(string descriptiveText)
        {
            DescriptiveText = descriptiveText;            
        }
        /// <value>read-only property for the descriptive text</value>
        public string DescriptiveText { get; }
    }
}
