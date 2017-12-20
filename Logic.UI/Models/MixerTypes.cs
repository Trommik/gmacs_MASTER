using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic.UI.Models
{
    /// <summary>
    /// Enum which holds every GeneratorType for TypeSafty.
    /// </summary>
    public enum MixerTypes : int
    {
        Add = 0,
        Substract = 1,
        Multiply = 2,
        Divide = 3,
        Difference = 4,
        AND = 5,
        OR = 6,
        XOR = 7,
    }
}
