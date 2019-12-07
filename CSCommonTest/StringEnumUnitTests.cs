using Xunit;
using CSCommon.Core.EnumStrings;

namespace CommonTests
{
    public class StringEnumUnitTests
    {
        [Fact]
        public void ForwardTest()
        {
            string value1 = EnumDictionary.ToString(FactoryType.Wood);
            Assert.True(value1 == "Wood");

            string value2 = EnumDictionary.ToString(FactoryType.Aluminium);
            Assert.True(value2 == "Aluminium");
        }

        [Fact]
        public void ReverseTest()
        {
            FactoryType factoryType1 = (FactoryType)EnumDictionary.ToEnum(typeof(FactoryType), "Wood");
            Assert.True(factoryType1 == FactoryType.Wood);

            FactoryType factoryType2 = (FactoryType)EnumDictionary.ToEnum(typeof(FactoryType), "Aluminium");
            Assert.True(factoryType2 == FactoryType.Aluminium);
        }
    }

    public enum FactoryType
    {
        [Description("Wood")]
        Wood,

        [Description("Aluminium")]
        Aluminium
    }
}