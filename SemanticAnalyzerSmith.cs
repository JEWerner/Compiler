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
        public override void InAProgram(comp5210.node.AProgram node)
        {
            IntType inttype = new IntType();
            inttype.name = "int";
            FloatType flttype = new FloatType();
            flttype.name = "float";
            StringType stringtype = new StringType();
            stringtype.name = "string";
            BooleanType booltype = new BooleanType();
            booltype.name = "boolean";

            stringhash.Add(booltype.name, booltype);
            stringhash.Add(stringtype.name, stringtype);
            stringhash.Add(inttype.name, inttype);
            stringhash.Add(flttype.name, flttype);
        }

        public override void OutAManyConstants(comp5210.node.AManyConstants node)
        {
            // Add Constants to nodehash
            Definition constants;
            nodehash.TryGetValue(node.GetConstDeclare(), out constants);
            nodehash.Add(node, constants);
        }
       
        public override void OutAManyMethodsMethods(comp5210.node.AManyMethodsMethods node)
        {
            // Add method to nodehash
            Definition method;
            nodehash.TryGetValue(node.GetMethod(), out method);
            nodehash.Add(node, method);
        }
        public override void OutALastMethodMethods(comp5210.node.ALastMethodMethods node)
        {
            // Add method to nodehash
            Definition method;
            nodehash.TryGetValue(node.GetMethod(), out method);
            nodehash.Add(node, method);
        }
        
        public override void OutAIntdeclConstDeclare(comp5210.node.AIntdeclConstDeclare node)
        {
            Console.WriteLine("test int const");

            string typename = node.GetVarType().Text;
            string varname = node.GetVarName().Text;
            Definition typedefn, stuff;

            // lookup the type
            if (!stringhash.TryGetValue(typename,out typedefn))
            {
                Console.WriteLine("[" + node.GetVarType().Line + "]: " +
                    typename + " is not defined.");
                ErrorDefinition errordef = new ErrorDefinition();
                nodehash.Add(node, errordef);
            }
            // check to make sure what we got back is a type
            else if (!(typedefn is TypeDefinition))
            {
                Console.WriteLine("[" + node.GetSemicolon().Line + "]: " +
                    typename + " is an invalid type.");
                ErrorDefinition errordef = new ErrorDefinition();
                nodehash.Add(node, errordef);
            }
			// check if the variable is already in the stringhash
			else if (stringhash.TryGetValue(varname,out stuff))
			{
                Console.WriteLine("[" + node.GetSemicolon().Line + "]: " +
                    varname + " is already declared.");
                ErrorDefinition errordef = new ErrorDefinition();
                nodehash.Add(node, errordef);
			}
            else
            {
                // add this variable to the hash table
                VariableDefinition vardefn = new VariableDefinition();
                vardefn.name = varname;
                vardefn.vartype = typedefn as TypeDefinition;
                stringhash.Add(vardefn.name, vardefn);
            }
        }
        public override void OutAFloatdeclConstDeclare(comp5210.node.AFloatdeclConstDeclare node)
        {
            Console.WriteLine("test float const");

            string typename = node.GetVarType().Text;
            string varname = node.GetVarName().Text;
            Definition typedefn, stuff;

            // lookup the type
            if (!stringhash.TryGetValue(typename, out typedefn))
            {
                Console.WriteLine("[" + node.GetVarType().Line + "]: " +
                    typename + " is not defined.");
                ErrorDefinition errordef = new ErrorDefinition();
                nodehash.Add(node, errordef);
            }
            // check to make sure what we got back is a type
            else if (!(typedefn is TypeDefinition))
            {
                Console.WriteLine("[" + node.GetSemicolon().Line + "]: " +
                    typename + " is an invalid type.");
                ErrorDefinition errordef = new ErrorDefinition();
                nodehash.Add(node, errordef);
            }
            // check if the variable is already in the stringhash
            else if (stringhash.TryGetValue(varname, out stuff))
            {
                Console.WriteLine("[" + node.GetSemicolon().Line + "]: " +
                    varname + " is already declared.");
                ErrorDefinition errordef = new ErrorDefinition();
                nodehash.Add(node, errordef);
            }
            else
            {
                // add this variable to the hash table
                VariableDefinition vardefn = new VariableDefinition();
                vardefn.name = varname;
                vardefn.vartype = typedefn as TypeDefinition;
                stringhash.Add(vardefn.name, vardefn);
            }
        }	
       
        public override void OutAWithParamMethod(comp5210.node.AWithParamMethod node)
        {            
            string methodname = node.GetMethodName().Text;         
            Definition typedefn; 
            // lookup the type
            if (stringhash.TryGetValue(methodname, out typedefn))
            {
                Console.WriteLine("[" + node.GetMethodName().Line + "]: " +
                    methodname + " is already defined.");
                ErrorDefinition errordef = new ErrorDefinition();
                nodehash.Add(node, errordef);
            }
            else
            {
                // add this variable to the hash table
                MethodDefinition methdefn = new MethodDefinition();
                methdefn.name = methodname;
                stringhash.Add(methdefn.name, methdefn);
            }
        }
        public override void OutAWithoutParamMethod(comp5210.node.AWithoutParamMethod node)
        {
            string methodname = node.GetMethodName().Text;
            Definition typedefn;
            // lookup the type
            if (stringhash.TryGetValue(methodname, out typedefn))
            {
                Console.WriteLine("[" + node.GetMethodName().Line + "]: " +
                    methodname + " is already defined.");
                ErrorDefinition errordef = new ErrorDefinition();
                nodehash.Add(node, errordef);
            }
            else
            {
                // add this variable to the hash table
                MethodDefinition methdefn = new MethodDefinition();
                methdefn.name = methodname;
                stringhash.Add(methdefn.name, methdefn);
            }
        }
        public override void OutAMethodCall(comp5210.node.AMethodCall node)
        {
            string methodname = node.GetMethodName().Text;
            Definition typedefn;
            // lookup the method name to ensure it exists
            if (!stringhash.TryGetValue(methodname, out typedefn))
            {
                Console.WriteLine("[" + node.GetMethodName().Line + "]: " +
                    methodname + " is not defined.");
                ErrorDefinition errordef = new ErrorDefinition();
                nodehash.Add(node, errordef);
            }
        }

        public override void OutAManyParametersFormalParameters(comp5210.node.AManyParametersFormalParameters node)
        {
            string typename = node.GetVarType().Text;
            string varname = node.GetVarName().Text;
            Definition typedefn, stuff;
            // lookup the type
            if (!stringhash.TryGetValue(typename, out typedefn))
            {
                Console.WriteLine("[" + node.GetVarType().Line + "]: " +
                    typename + " is not defined.");
                ErrorDefinition errordef = new ErrorDefinition();
                nodehash.Add(node, errordef);
            }
            // check to make sure what we got back is a type
            else if (!(typedefn is TypeDefinition))
            {
                Console.WriteLine("[" + node.GetComma().Line + "]: " +
                    typename + " is an invalid type.");
                ErrorDefinition errordef = new ErrorDefinition();
                nodehash.Add(node, errordef);
            }
            // check if the variable is already in the stringhash
            else if (stringhash.TryGetValue(varname, out stuff))
            {
                Console.WriteLine("[" + node.GetComma().Line + "]: " +
                    varname + " is already declared.");
                ErrorDefinition errordef = new ErrorDefinition();
                nodehash.Add(node, errordef);
            }
            else
            {
                // add this variable to the hash table
                VariableDefinition vardefn = new VariableDefinition();
                vardefn.name = varname;
                vardefn.vartype = typedefn as TypeDefinition;
                stringhash.Add(vardefn.name, vardefn);
            }
        }
        public override void OutAFinalParameterFormalParameters(comp5210.node.AFinalParameterFormalParameters node)
        {
            string typename = node.GetVarType().Text;
            string varname = node.GetVarName().Text;
            Definition typedefn, stuff;
            // lookup the type
            if (!stringhash.TryGetValue(typename, out typedefn))
            {
                Console.WriteLine("[" + node.GetVarType().Line + "]: " +
                    typename + " is not defined.");
                ErrorDefinition errordef = new ErrorDefinition();
                nodehash.Add(node, errordef);
            }
            // check to make sure what we got back is a type
            else if (!(typedefn is TypeDefinition))
            {
                Console.WriteLine("[" + node.GetVarName().Line + "]: " +
                    typename + " is an invalid type.");
                ErrorDefinition errordef = new ErrorDefinition();
                nodehash.Add(node, errordef);
            }
            // check if the variable is already in the stringhash
            else if (stringhash.TryGetValue(varname, out stuff))
            {
                Console.WriteLine("[" + node.GetVarName().Line + "]: " +
                    varname + " is already declared.");
                ErrorDefinition errordef = new ErrorDefinition();
                nodehash.Add(node, errordef);
            }
            else
            {
                // add this variable to the hash table
                VariableDefinition vardefn = new VariableDefinition();
                vardefn.name = varname;
                vardefn.vartype = typedefn as TypeDefinition;
                stringhash.Add(vardefn.name, vardefn);
            }
        }
       
        public override void OutAManyStatementsStatements(comp5210.node.AManyStatementsStatements node)
        {
            // add state to the nodehash
            Definition state;
            nodehash.TryGetValue(node.GetStatement(), out state);
            nodehash.Add(node, state);
        }
        public override void OutAOneStatementStatements(comp5210.node.AOneStatementStatements node)
        {
            // add state to the nodehash
            Definition state;
            nodehash.TryGetValue(node.GetStatement(), out state);
            nodehash.Add(node, state);
        }
        
        public override void OutANumDeclareStatement(comp5210.node.ANumDeclareStatement node)
        {
            // add numdecl to the nodehash
            Definition numdecl;
            nodehash.TryGetValue(node.GetNumDeclare(), out numdecl);
            nodehash.Add(node, numdecl);
        }
        public override void OutAAssignmentStatement(comp5210.node.AAssignmentStatement node)
        {
            // add assign to the nodehash
            Definition assign;
            nodehash.TryGetValue(node.GetAssignment(), out assign);
            nodehash.Add(node, assign);
        } 
        public override void OutAVarDeclareStatement(comp5210.node.AVarDeclareStatement node)
        {            
            string typename = node.GetVarType().Text;
            string varname = node.GetVarName().Text;
            Definition typedefn, stuff;
            Console.WriteLine("Test "+ typename+" "+varname);
            // lookup the type
            if (!stringhash.TryGetValue(typename, out typedefn))
            {
                Console.WriteLine("[" + node.GetVarType().Line + "]: " +
                    typename + " is not defined.");
                ErrorDefinition errordef = new ErrorDefinition();
                nodehash.Add(node, errordef);
            }
            // check to make sure what we got back is a type
            else if (!(typedefn is TypeDefinition))
            {
                Console.WriteLine("[" + node.GetVarName().Line + "]: " +
                    typename + " is an invalid type.");
                ErrorDefinition errordef = new ErrorDefinition();
                nodehash.Add(node, errordef);
            }
            // check if the variable is already in the stringhash
            else if (stringhash.TryGetValue(varname, out stuff))
            {
                Console.WriteLine("[" + node.GetVarName().Line + "]: " +
                    varname + " is already declared.");
                ErrorDefinition errordef = new ErrorDefinition();
                nodehash.Add(node, errordef);
            }
            else
            {
                // add this variable to the hash table
                Console.WriteLine("good code");
                VariableDefinition vardefn = new VariableDefinition();
                vardefn.name = varname;
                vardefn.vartype = typedefn as TypeDefinition;
                stringhash.Add(vardefn.name, vardefn);            }
        }
        public override void OutAIfStatement(comp5210.node.AIfStatement node)
        {
            // add ifstate to the nodehash
            Definition ifstate;
            nodehash.TryGetValue(node.GetIf(), out ifstate);
            nodehash.Add(node, ifstate);
        }
        public override void OutAWhileStatement(comp5210.node.AWhileStatement node)
        {
            // add whilestate to the nodehash
            Definition whilestate;
            nodehash.TryGetValue(node.GetWhile(), out whilestate);
            nodehash.Add(node, whilestate);
        }
        public override void OutAMethodCallStatement(comp5210.node.AMethodCallStatement node)
        {
            // add meth to the nodehash, not in our bodies. That stuff is illegal and not good for you
            Definition meth;
            nodehash.TryGetValue(node.GetMethodCall(), out meth);
            nodehash.Add(node, meth);
        }

        public override void OutAAssignExpressionAssignment(comp5210.node.AAssignExpressionAssignment node)
        {            
            string typename = node.GetVarName().Text;
            Definition rhs, lhs;
            nodehash.TryGetValue(node.GetExpressions(), out rhs);
            stringhash.TryGetValue(node.GetVarName().Text, out lhs);
            Console.WriteLine(lhs.name + " " + rhs.GetType().ToString());
            // check if the type is in the stringhash
            if (!stringhash.TryGetValue(typename, out rhs))
            {
                Console.WriteLine("[" + node.GetVarName().Line + "]: " +
                    typename + " is not defined.");
                ErrorDefinition errordef = new ErrorDefinition();
                nodehash.Add(node, errordef);
            }
            // check if the types match
            else if (lhs.GetType() != rhs.GetType())
            {
                Console.WriteLine("[" + node.GetEqual().Line + "]: " +
                    "types don't match");
                ErrorDefinition errordef = new ErrorDefinition();
                nodehash.Add(node, errordef);
            }
            else
            {
                // add assign expression assignment to the nodehash
                nodehash.Add(node, rhs);
            }  
            
        }
        public override void OutAArrayAssignAssignment(comp5210.node.AArrayAssignAssignment node)
        {
            string typename = node.GetVarName().Text;
            Definition inter, name, outer, inttype;

            stringhash.TryGetValue("int", out inttype);
            nodehash.TryGetValue(node.GetInternalMath(), out inter);
            nodehash.TryGetValue(node.GetExternalMath(), out outer);
            stringhash.TryGetValue(node.GetVarName().Text, out name);
            // check if the type is in the stringhash
            if (!stringhash.TryGetValue(typename, out name))
            {
                Console.WriteLine("[" + node.GetVarName().Line + "]: " +
                    typename + " is not defined.");
                ErrorDefinition errordef = new ErrorDefinition();
                nodehash.Add(node, errordef);
            }
            // check if it is an integer
            else if (inttype != inter)
            {
                Console.WriteLine("[" + node.GetEqual().Line + "]: " +
                    "index is not of type int");
                ErrorDefinition errordef = new ErrorDefinition();
                nodehash.Add(node, errordef);
            }
            // check if both sides have equal types
            else if ((name as VariableDefinition).vartype != outer)
            {
                Console.WriteLine("[" + node.GetEqual().Line + "]: " +
                    "types don't match");
                ErrorDefinition errordef = new ErrorDefinition();
                nodehash.Add(node, errordef);
            }
            else
            {
                // add varddef to the nodehash
                VariableDefinition varddef = new VariableDefinition();
                varddef.vartype = name as TypeDefinition;
                nodehash.Add(node, varddef);
            }
        }

        public override void OutAIntdeclNumDeclare(comp5210.node.AIntdeclNumDeclare node)
        {
            Console.WriteLine("test pnt 3");

            string varname = node.GetVarName().Text;
            string vartype = node.GetVarType().Text;

            Definition typedefn, stuff, inttype;
            stringhash.TryGetValue("int", out inttype);
            // lookup the type see if it's in the table
            if (!stringhash.TryGetValue(vartype, out typedefn))
            {
                Console.WriteLine("[" + node.GetVarType().Line + "]: " +
                    vartype + " is not defined.");
                ErrorDefinition errordef = new ErrorDefinition();
                nodehash.Add(node, errordef);
            }
            // checks if it is an int
            else if (typedefn != inttype)
            {
                Console.WriteLine("[" + node.GetVarType().Line + "]: " +
                    varname + " is the wrong type, int.");
                ErrorDefinition errordef = new ErrorDefinition();
                nodehash.Add(node, errordef);
            }
            // checks if varname is already in table
            else if (stringhash.TryGetValue(varname, out stuff))
            {
                Console.WriteLine("[" + node.GetVarType().Line + "]: " +
                    varname + " is already defined.");
                ErrorDefinition errordef = new ErrorDefinition();
                nodehash.Add(node, errordef);
            }
            else
            {
                // add them to the string and node hash
                stringhash.Add(varname, inttype);
                nodehash.Add(node, inttype);
            }
        }
        public override void OutAFloatdeclNumDeclare(comp5210.node.AFloatdeclNumDeclare node)
        {
            Console.WriteLine("test pnt 6");

            string varname = node.GetVarName().Text;
            string vartype = node.GetVarType().Text;
            Definition typedefn, stuff, flttype;
            stringhash.TryGetValue("float", out flttype);
            // lookup the type see if it's in the table
            if (!stringhash.TryGetValue(vartype, out typedefn))
            {
                Console.WriteLine("[" + node.GetVarType().Line + "]: " +
                    vartype + " is not defined.");
                ErrorDefinition errordef = new ErrorDefinition();
                nodehash.Add(node, errordef);
            }
            // checks if it is an int
            else if (typedefn != flttype)
            {
                Console.WriteLine("[" + node.GetVarType().Line + "]: " +
                    varname + " is the wrong type, float.");
                ErrorDefinition errordef = new ErrorDefinition();
                nodehash.Add(node, errordef);
            }
            // checks if varname is already in table
            else if (stringhash.TryGetValue(varname, out stuff))
            {
                Console.WriteLine("[" + node.GetVarType().Line + "]: " +
                    varname + " is already defined.");
                ErrorDefinition errordef = new ErrorDefinition();
                nodehash.Add(node, errordef);
            }
            else
            {
                // adds them to the string and node hash
                stringhash.Add(varname, flttype);
                nodehash.Add(node, flttype);
            }
        }
        public override void OutAArrayIndexNumDeclare(comp5210.node.AArrayIndexNumDeclare node)
        {
            string typename = node.GetVarType().Text;
            string varname = node.GetVarName().Text;
            Definition typedefn;
            Definition stuff;

            // lookup the type
            if (!stringhash.TryGetValue(typename, out typedefn))
            {
                Console.WriteLine("[" + node.GetVarType().Line + "]: " +
                    typename + " is not defined.");
                ErrorDefinition errordef = new ErrorDefinition();
                nodehash.Add(node, errordef);
            }
            // check to make sure what we got back is a type
            else if (!(typedefn is TypeDefinition))
            {
                Console.WriteLine("[" + node.GetSemicolon().Line + "]: " +
                    typename + " is an invalid type.");
                ErrorDefinition errordef = new ErrorDefinition();
                nodehash.Add(node, errordef);
            }
            // lookup the variable
            else if (stringhash.TryGetValue(varname, out stuff))
            {
                Console.WriteLine("[" + node.GetSemicolon().Line + "]: " +
                    varname + " is already declared.");
                ErrorDefinition errordef = new ErrorDefinition();
                nodehash.Add(node, errordef);
            }
            else
            {
                // add this variable to the hash table
                Array vardefn = new Array();
                vardefn.name = varname;
                vardefn.vartype = typedefn as TypeDefinition;
                vardefn.size = Convert.ToInt32(node.GetIntlit().Text);
                stringhash.Add(vardefn.name, vardefn);
            }
        }
		
        public override void OutAIfElseIf(comp5210.node.AIfElseIf node)
        {
            Definition exprtype, booltype;
            stringhash.TryGetValue("boolean", out booltype);
            nodehash.TryGetValue(node.GetExpressions(), out exprtype);
            // checks if it is a booltype
            if (exprtype != booltype)
            {
                Console.WriteLine("[" + node.GetIflit().Line + "]: " +
                    "types don't match");
                ErrorDefinition errordef = new ErrorDefinition();
                nodehash.Add(node, errordef);
            }
            else
            {
                // adds it to the nodehash
                nodehash.Add(node, booltype);
            }
        }
        public override void OutAIfIf(comp5210.node.AIfIf node)
        {
            Definition booltype;
            Definition exprtype;

            stringhash.TryGetValue("boolean", out booltype);
            nodehash.TryGetValue(node.GetExpressions(), out exprtype);
            // checks if it is a booltype
            if (exprtype != booltype)
            {
                Console.WriteLine("[" + node.GetIflit().Line + "]: " +
                    "types don't match");
                ErrorDefinition errordef = new ErrorDefinition();
                nodehash.Add(node, errordef);
            }
            else
            {
                // adds it to the nodehash
                nodehash.Add(node, booltype);
            }
        }
        public override void OutAElseElsepart(comp5210.node.AElseElsepart node)
        {
            BasicType booltype = new BasicType();
            booltype.name = "boolean";
            nodehash.Add(node, booltype);
        }
        public override void OutAWhile(comp5210.node.AWhile node)
        {
            Definition booltype, exprtype;
            stringhash.TryGetValue("boolean", out booltype);
            nodehash.TryGetValue(node.GetExpressions(), out exprtype);
            // checks if it is a booltype
            if (exprtype != booltype)
            {
                Console.WriteLine("[" + node.GetWhilelit().Line + "]: " +
                    "types don't match");
                ErrorDefinition errordef = new ErrorDefinition();
                nodehash.Add(node, errordef);
            }
            // adds it to the nodehash
            else
            {
                nodehash.Add(node, booltype);
            }
        }
		
        public override void OutAAndExpressions(comp5210.node.AAndExpressions node)
        {
            Definition rhs, lhs, booltype;
            stringhash.TryGetValue("boolean", out booltype);
            nodehash.TryGetValue(node.GetExpressions(), out lhs);
            nodehash.TryGetValue(node.GetLogicalCompare(), out rhs);
            // make sure left hand side and right hand side match and are booltype
            if (lhs != booltype || rhs != booltype)
            {
                Console.WriteLine("[" + node.GetAnd().Line + "]: " +
                    "types don't match");
                ErrorDefinition errordef = new ErrorDefinition();
                nodehash.Add(node, errordef);
            }
            else
            {
                // adds it to the nodehash
                nodehash.Add(node, booltype);
            }
        }
        public override void OutAOrExpressions(comp5210.node.AOrExpressions node)
        {
            Definition rhs, lhs, booltype;
            stringhash.TryGetValue("boolean", out booltype);
            nodehash.TryGetValue(node.GetExpressions(), out lhs);
            nodehash.TryGetValue(node.GetLogicalCompare(), out rhs);
            // make sure left hand side and right hand side match and are booltype
            if (lhs != booltype || rhs != booltype)
            {
                Console.WriteLine("[" + node.GetOr().Line + "]: " +
                    "types don't match");
                ErrorDefinition errordef = new ErrorDefinition();
                nodehash.Add(node, errordef);
            }
            else
            {
                // adds it to the nodehash
                nodehash.Add(node, booltype);
            }
        }
        public override void OutANoLogOpExpressions(comp5210.node.ANoLogOpExpressions node)
        {
            // adds it to the nodehash
            Definition exprdefn;
            nodehash.TryGetValue(node.GetLogicalCompare(), out exprdefn);
            nodehash.Add(node, exprdefn);
        }

        public override void OutAGreaterLogicalCompare(comp5210.node.AGreaterLogicalCompare node)
        {
            Definition rhs, lhs, booltype;
            stringhash.TryGetValue("boolean", out booltype);
            nodehash.TryGetValue(node.GetSide1(), out lhs);
            nodehash.TryGetValue(node.GetSide2(), out rhs);
            // make sure left hand side and right hand side match
            if (lhs != rhs)
            {
                Console.WriteLine("[" + node.GetGrtr().Line + "]: " +
                    "types don't match");
                ErrorDefinition errordef = new ErrorDefinition();
                nodehash.Add(node, errordef);
            }
            else
            {
                // adds it to the nodehash
                nodehash.Add(node, booltype);
            }
        }
        public override void OutALessThanLogicalCompare(comp5210.node.ALessThanLogicalCompare node)
        {
            Definition rhs, lhs, booltype;
            stringhash.TryGetValue("boolean", out booltype);
            nodehash.TryGetValue(node.GetSide1(), out lhs);
            nodehash.TryGetValue(node.GetSide2(), out rhs);
            // make sure left hand side and right hand side match
            if (lhs != rhs)
            {
                Console.WriteLine("[" + node.GetLessthan().Line + "]: " +
                    "types don't match");
                ErrorDefinition errordef = new ErrorDefinition();
                nodehash.Add(node, errordef);
            }
            else
            {
                // adds it to the nodehash
                nodehash.Add(node, booltype);
            }
        }
        public override void OutAGreaterThanEqualToLogicalCompare(comp5210.node.AGreaterThanEqualToLogicalCompare node)
        {
            Definition rhs, lhs, booltype;
            stringhash.TryGetValue("boolean", out booltype);
            nodehash.TryGetValue(node.GetSide1(), out lhs);
            nodehash.TryGetValue(node.GetSide2(), out rhs);
            // make sure left hand side and right hand side match
            if (lhs != rhs)
            {
                Console.WriteLine("[" + node.GetGrtreqto().Line + "]: " +
                    "types don't match");
                ErrorDefinition errordef = new ErrorDefinition();
                nodehash.Add(node, errordef);
            }
            else
            {
                // adds it to the nodehash
                nodehash.Add(node, booltype);
            }
        }
        public override void OutALessThanEqualToLogicalCompare(comp5210.node.ALessThanEqualToLogicalCompare node)
        {
            Definition rhs, lhs, booltype;
            stringhash.TryGetValue("boolean", out booltype);
            nodehash.TryGetValue(node.GetSide1(), out lhs);
            nodehash.TryGetValue(node.GetSide2(), out rhs);
            // make sure left hand side and right hand side match
            if (lhs != rhs)
            {
                Console.WriteLine("[" + node.GetLesseqto().Line + "]: " +
                    "types don't match");
                ErrorDefinition errordef = new ErrorDefinition();
                nodehash.Add(node, errordef);
            }
            else
            {
                // adds it to the nodehash
                nodehash.Add(node, booltype);
            }
        }
        public override void OutAEqualToLogicalCompare(comp5210.node.AEqualToLogicalCompare node)
        {
            Definition rhs, lhs, booltype;
            stringhash.TryGetValue("boolean", out booltype);
            nodehash.TryGetValue(node.GetSide1(), out lhs);
            nodehash.TryGetValue(node.GetSide2(), out rhs);
            // make sure left hand side and right hand side match
            if (lhs != rhs)
            {
                Console.WriteLine("[" + node.GetEqto().Line + "]: " +
                    "types don't match");
                ErrorDefinition errordef = new ErrorDefinition();
                nodehash.Add(node, errordef);
            }
            else
            {
                // adds it to the nodehash
                nodehash.Add(node, booltype);
            }
        }
        public override void OutANoLogCompareLogicalCompare(comp5210.node.ANoLogCompareLogicalCompare node)
        {
            // adds it to the nodehash
            Definition exprdefn;
            nodehash.TryGetValue(node.GetAddSub(), out exprdefn);
            nodehash.Add(node, exprdefn);
        }

        public override void OutAAddAddSub(comp5210.node.AAddAddSub node)
        {
            Definition rhs, lhs;
            Definition exprdefn;
            nodehash.TryGetValue(node.GetAddSub(), out lhs);
            nodehash.TryGetValue(node.GetMultiDiv(), out rhs);
            // make sure left hand side and right hand side match
            if (lhs != rhs)
            {
                Console.WriteLine("[" + node.GetPlus().Line + "]: " +
                    "types don't match");
                ErrorDefinition errordef = new ErrorDefinition();
                nodehash.Add(node, errordef);
            }
            else
            {
                // adds it to the nodehash
                nodehash.TryGetValue(node.GetMultiDiv(), out exprdefn);
                nodehash.Add(node, exprdefn);
            }
        }
        public override void OutASubtractAddSub(comp5210.node.ASubtractAddSub node)
        {
            Definition rhs, lhs;
            Definition exprdefn;
            nodehash.TryGetValue(node.GetAddSub(), out lhs);
            nodehash.TryGetValue(node.GetMultiDiv(), out rhs);
            // make sure left hand side and right hand side match
            if (lhs != rhs)
            {
                Console.WriteLine("[" + node.GetSub().Line + "]: " +
                    "types don't match");
                ErrorDefinition errordef = new ErrorDefinition();
                nodehash.Add(node, errordef);
            }
            else
            {
                // adds it to the nodehash
                nodehash.TryGetValue(node.GetMultiDiv(), out exprdefn);
                nodehash.Add(node, exprdefn);
            }
        }
        public override void OutANoMoreAddSubAddSub(comp5210.node.ANoMoreAddSubAddSub node)
        {
            // adds it to the nodehash
            Definition exprdefn;
            nodehash.TryGetValue(node.GetMultiDiv(), out exprdefn);
            nodehash.Add(node, exprdefn);
        }

        public override void OutAMultiplyMultiDiv(comp5210.node.AMultiplyMultiDiv node)
        {
            Definition rhs, lhs;
            Definition exprdefn;
            nodehash.TryGetValue(node.GetMultiDiv(), out lhs);
            nodehash.TryGetValue(node.GetParenth(), out rhs);
            // make sure left hand side and right hand side match
            if (lhs != rhs)
            {
                Console.WriteLine("[" + node.GetMulti().Line + "]: " +
                    "types don't match");
                ErrorDefinition errordef = new ErrorDefinition();
                nodehash.Add(node, errordef);
            }
            else
            {
                // adds it to the nodehash
                nodehash.TryGetValue(node.GetParenth(), out exprdefn);
                nodehash.Add(node, exprdefn);
            }
        }
        public override void OutADivideMultiDiv(comp5210.node.ADivideMultiDiv node)
        {
            Definition rhs, lhs;
            Definition exprdefn;
            nodehash.TryGetValue(node.GetMultiDiv(), out lhs);
            nodehash.TryGetValue(node.GetParenth(), out rhs);
            // make sure left hand side and right hand side match
            if (lhs != rhs)
            {
                Console.WriteLine("[" + node.GetDiv().Line + "]: " +
                    "types don't match");
                ErrorDefinition errordef = new ErrorDefinition();
                nodehash.Add(node, errordef);
            }
            else
            {
                // adds it to the nodehash
                nodehash.TryGetValue(node.GetParenth(), out exprdefn);
                nodehash.Add(node, exprdefn);
            }
        }
        public override void OutANoMoreDivMultiMultiDiv(comp5210.node.ANoMoreDivMultiMultiDiv node)
        {
            // adds it to the nodehash
            Definition exprdefn;
            nodehash.TryGetValue(node.GetParenth(), out exprdefn);
            nodehash.Add(node, exprdefn);
        }
		
        public override void OutAParenthParenth(comp5210.node.AParenthParenth node)
        {
            // adds it to the nodehash
            Definition exprdefn;
            nodehash.TryGetValue(node.GetExpressions(), out exprdefn);
            nodehash.Add(node, exprdefn);
        }
        public override void OutAIntParenth(comp5210.node.AIntParenth node)
        {
            // adds it to the nodehash
            Definition inttype;
            stringhash.TryGetValue("int", out inttype);
            nodehash.Add(node, inttype);
        }
        public override void OutAFloatParenth(comp5210.node.AFloatParenth node)
        {
            // adds it to the nodehash
            Definition floattype;
            stringhash.TryGetValue("float", out floattype);
            nodehash.Add(node, floattype);
        }
        public override void OutAArrayParenth(comp5210.node.AArrayParenth node)
        {
            string variable = node.GetId().Text;
            Definition inttype, typedefn, rhs;
            nodehash.TryGetValue(node.GetExpressions(), out rhs);
            stringhash.TryGetValue(node.GetId().Text, out inttype);
            // lookup the type
            if (!stringhash.TryGetValue(variable, out typedefn))
            {
                Console.WriteLine("[" + node.GetId().Line + "]: " +
                    variable + " is not defined.");
                ErrorDefinition errordef = new ErrorDefinition();
                nodehash.Add(node, errordef);
            }
            // check to make sure what we got back is not a type
            else if (typedefn is TypeDefinition)
            {
                Console.WriteLine("[" + node.GetId().Line + "]: " +
                    variable + " is an invalid type.");
                ErrorDefinition errordef = new ErrorDefinition();
                nodehash.Add(node, errordef);
            }
            // checks if rhs is an int            
            else if(rhs != inttype)
            {
                Console.WriteLine("[" + node.GetId().Line + "]: " +
                    node.GetExpressions() + " is an invalid index.");
                ErrorDefinition errordef = new ErrorDefinition();
                nodehash.Add(node, errordef);
            }
            else
            {
                // adds it to the nodehash
                Array total = new Array();
                total.vartype = typedefn as TypeDefinition;
                nodehash.Add(node, total);
            }
        }
        public override void OutAVariableParenth(comp5210.node.AVariableParenth node)
        {
            string variable = node.GetId().Text;
            Definition typedefn;

            // lookup the type
            if (!stringhash.TryGetValue(variable, out typedefn))
            {
                Console.WriteLine("[" + node.GetId().Line + "]: " +
                    variable + " is not defined.");
                ErrorDefinition errordef = new ErrorDefinition();
                nodehash.Add(node, errordef);
            }
            // check to make sure what we got back is not a type
            else if (!(typedefn is VariableDefinition))
            {
                Console.WriteLine("[" + node.GetId().Line + "]: " +
                    variable + " is not a variable.");
                ErrorDefinition errordef = new ErrorDefinition();
                nodehash.Add(node, errordef);
            }
            else
            {
                // adds it to the nodehash      
                nodehash.Add(node, (typedefn as VariableDefinition).vartype);
            }
        }
    }
}
