using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace parser
{
    class SemanticAnalyzer : comp5210.analysis.DepthFirstAdapter
    {
        System.Collections.Generic.Dictionary<string,parser.Definition>
            stringhash = new Dictionary<string,Definition>();
        System.Collections.Generic.Dictionary<comp5210.node.Node, parser.Definition>
            nodehash = new Dictionary<comp5210.node.Node, Definition>();
        // before theclass starts, create the two hashes and 
        // add int and float
        public override void InATheclass(comp5210.node.ATheclass node)
        {
            BasicType inttype = new BasicType();
            inttype.name = "int";
            BasicType flttype = new BasicType();
            flttype.name = "float";
            stringhash.Add(inttype.name,inttype);
            stringhash.Add(flttype.name,flttype);
        }

        // override the case to change the order, this way processes them
        // backwards (for no good reason)
        public override void CaseAMoreDecls(comp5210.node.AMoreDecls node)
        {
            InAMoreDecls(node);
            if (node.GetDecl() != null)
            {
                node.GetDecl().Apply(this);
            }
            if (node.GetDecls() != null)
            {
                node.GetDecls().Apply(this);
            }
            OutAMoreDecls(node);
        }
        // 
        public override void OutADecl(comp5210.node.ADecl node)
        {
            string typename = node.GetTypename().Text;
            string varname = node.GetVarname().Text;
            Definition typedefn;
            // lookup the type
            if (!stringhash.TryGetValue(typename,out typedefn))
            {
                Console.WriteLine("[" + node.GetTypename().Line + "]: " +
                    typename + " is not defined.");
            }
            // check to make sure what we got back is a type
            else if (!(typedefn is TypeDefinition))
            {
                Console.WriteLine("[" + node.GetSemicolon().Line + "]: " +
                    typename + " is an invalid type.");
            }
            else
            {
                // add this variable to the hash table
                // note you need to add checks to make sure this 
                // variable name isn't already defined.
                VariableDefinition vardefn = new VariableDefinition();
                vardefn.name = varname;
                vardefn.vartype = typedefn as TypeDefinition;
                stringhash.Add(vardefn.name, vardefn);
            }
        }
        public override void OutAStmt(comp5210.node.AStmt node)
        {
            Definition rhs,lhs;
            nodehash.TryGetValue(node.GetExpr(), out rhs);
            stringhash.TryGetValue(node.GetId().Text, out lhs);
            // make sure left hand side and right hand side match
            // of course, you should really make sure left side is
            // a variable first
            if ((lhs as VariableDefinition).vartype != rhs)
            {
                Console.WriteLine("[" + node.GetEquals().Line + "]: " +
                    "types don't match");
            }
        }
        public override void OutAComplexExpr(comp5210.node.AComplexExpr node)
        {
            Definition lhs;
            nodehash.TryGetValue(node.GetBase(), out lhs);
            // you should really get the types of both sides and make sure 
            // they match
            // make sure the type of the child is a BasicType, as those
            // are the only addable things.
            nodehash.Add(node, lhs);
        }
        public override void OutASimpleExpr(comp5210.node.ASimpleExpr node)
        {
            Definition lhs;
            nodehash.TryGetValue(node.GetBase(), out lhs);
            nodehash.Add(node, lhs);
        }
        public override void OutAParenBase(comp5210.node.AParenBase node)
        {
            Definition exprdefn;
            nodehash.TryGetValue(node.GetExpr(), out exprdefn);
            nodehash.Add(node, exprdefn);
        }
        public override void OutAIdBase(comp5210.node.AIdBase node)
        {
            Definition iddefn;
            if (!stringhash.TryGetValue(node.GetId().Text, out iddefn))
            {
                Console.WriteLine("[" + node.GetId().Line + "]: " +
                    node.GetId().Text + " is not defined");
            }
            // you should really make sure that iddefn is a variable 
            // definition
            else
            {
                nodehash.Add(node, (iddefn as VariableDefinition).vartype);
            }
        }
    }
}
