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
        public TypeDefinition assignment;
    }
    public class Array : TypeDefinition
    {
        public TypeDefinition assignment;
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
	public SubprogramDefinition statements;
    }
}
