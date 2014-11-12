using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace parser
{
    public abstract class Definition
    {
        public string name;
    }
        public abstract class TypeDefinition : Definition
        {
        }
            public class BasicType : TypeDefinition
            {
            }
                public class IntType : BasicType
                {
                }
                public class FloatType : BasicType
                {
                }
                public class StringType : BasicType
                {
                }
                public class BooleanType : BasicType
                {
                }
                public class ErrorType : BasicType
                {
                }
            public class Array : TypeDefinition
            {
                public TypeDefinition vartype;
                public int size;
            }
        public class VariableDefinition : Definition
        {
            public TypeDefinition vartype;
        }
        public abstract class SubprogramDefinition : Definition
        {
        }
            public class Procedure : SubprogramDefinition
            {
                public System.Collections.Generic.List<VariableDefinition> list;
            }
            public class MethodDefinition : SubprogramDefinition
            {
            }
            public class ErrorDefinition : Definition
            {
                public ErrorDefinition()
                {
                    name = "error";
                }

            }
}
