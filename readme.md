# CSCommon
This a set of reusable common classes needed in C# projects.

Enumerated Strings:
==================
sometimes you want to assign string description to enums or define enumerated strings.

### How to use
    public enum FactoryType
    {
        [Description("Wood")]
        Wood,

        [Description("Aluminium")]
        Aluminium
    }
    
    string value1 = EnumDictionary.ToString(FactoryType.Wood);        // value1 = Wood
    FactoryType factoryType = (FactoryType)EnumDictionary.ToEnum(typeof(FactoryType), "Wood");      // factoryType = FactoryType.Wood
