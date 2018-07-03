using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PasswordGenerator.Models
{
    public class PasswordRequirement
    {
        public int Length { get; set; }
        public bool IncludeSymbols { get; set; }
        public bool IncludeNumbers { get; set; }
        public bool IncludeLowercase { get; set; }
        public bool IncludeUpperCase { get; set; }
        public bool MultiplePasswords { get; set; }
    }
}
