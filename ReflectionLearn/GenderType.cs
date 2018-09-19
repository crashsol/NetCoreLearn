using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;


namespace ReflectionLearn
{
    [Flags]
    [Description("性别")]
    public enum GenderType
    {
        [Description("男")]
        Male = 1 << 0,
        [Description("女")]
        FeMale = 1 << 1,
        [Description("未知")]
        UnKnow = 1 << 2,

    }
}
