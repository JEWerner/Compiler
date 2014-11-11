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
            BasicType inttype = new BasicType();
            inttype.name = "int";
            BasicType flttype = new BasicType();
            flttype.name = "float";
            BasicType stringtype = new BasicType();
            stringtype.name = "string";
            BasicType booltype = new BasicType();
            booltype.name = "boolean";

            stringhash.Add(booltype.name, booltype);
            stringhash.Add(stringtype.name, stringtype);
            stringhash.Add(inttype.name, inttype);
            stringhash.Add(flttype.name, flttype);
        }
		

        public override void OutAIntdeclConst_declare(comp5210.node.AIntdeclConstDeclare node)
        {
            string typename = node.GetVarType().Text;
            string varname = node.GetVarName().Text;
            Definition typedefn;
            Definition stuff;

            // lookup the type
            if (!stringhash.TryGetValue(typename,out typedefn))
            {
                Console.WriteLine("[" + node.GetVarType().Line + "]: " +
                    typename + " is not defined.");
            }
            // check to make sure what we got back is a type
            else if (!(typedefn is TypeDefinition))
            {
                Console.WriteLine("[" + node.GetSemicolon().Line + "]: " +
                    typename + " is an invalid type.");
            }
			
			
			//Changed this
			else if (stringhash.TryGetValue(varname,out stuff ))
			{
                Console.WriteLine("[" + node.GetSemicolon().Line + "]: " +
                    varname + " is already declared.");
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
        public override void OutAFloatdeclConst_declare(comp5210.node.AFloatdeclConstDeclare node)
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
            }
            // check to make sure what we got back is a type
            else if (!(typedefn is TypeDefinition))
            {
                Console.WriteLine("[" + node.GetSemicolon().Line + "]: " +
                    typename + " is an invalid type.");
            }


            //Changed this
            else if (stringhash.TryGetValue(varname, out stuff))
            {
                Console.WriteLine("[" + node.GetSemicolon().Line + "]: " +
                    varname + " is already declared.");
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
		
		
        public override void OutAWithParamMethod(comp5210.node.AWithParamMethod node)
        {
            
            string methodname = node.GetMethodName().Text;
         
            Definition typedefn;
            

            // lookup the type
            if (stringhash.TryGetValue(methodname, out typedefn))
            {
                Console.WriteLine("[" + node.GetMethodName().Line + "]: " +
                    methodname + " is already defined.");
            }

            else
            {
                // add this variable to the hash table
                // note you need to add checks to make sure this 
                // variable name isn't already defined.
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
            }

            else
            {
                // add this variable to the hash table
                // note you need to add checks to make sure this 
                // variable name isn't already defined.
                MethodDefinition methdefn = new MethodDefinition();
                methdefn.name = methodname;
                stringhash.Add(methdefn.name, methdefn);
            }
        }		
        public override void OutAMethod_call(comp5210.node.AMethodCall node)
        {
        }
		
        public override void OutAString_paramActual_parameters(comp5210.node.AStringParamActualParameters node)
        {
        }
        public override void OutAExpression_paramActualParameters(comp5210.node.AExpressionParamActualParameters node)
        {
        }
        public override void OutAString_param_lastActual_parameters(comp5210.node.ALastParamStringActualParameters node)
        {
        }
        public override void OutAExpression_last_Actual_parameters(comp5210.node.ALastParamExpressionActualParameters node)
        {
        }
		
        public override void OutAMany_parametersFormal_parameters(comp5210.node.AManyParametersFormalParameters node)
        {
        }
        public override void OutAFinal_parametersFormal_parameters(comp5210.node.AFinalParameterFormalParameters node)
        {
        }
		
        public override void OutAMany_statementsStatements(comp5210.node.AManyStatementsStatements node)
        {
        }
        public override void OutAFinal_Formal_parameters(comp5210.node.AFinalParameterFormalParameters node)
        {
        }		
		
        public override void OutAAssignmentStatement(comp5210.node.AAssignmentStatement node)
        {
        }
        public override void OutANum_declareStatement(comp5210.node.ANumDeclareStatement node)
        {
        }	
        public override void OutAMethod_callStatement(comp5210.node.AMethodCallStatement node)
        {
        }
        public override void OutAIfStatement(comp5210.node.AIfStatement node)
        {
        }	
        public override void OutAWhileStatement(comp5210.node.AWhileStatement node)
        {
        }
		
        public override void OutAAssignExpression(comp5210.node.AAssignExpressionAssignment node)
        {
        }	
        public override void OutAVar_DeclareAssignment(comp5210.node.AVarDeclareStatement node)
        {
        }
        public override void OutAArray_assignAssignment(comp5210.node.AArrayAssignAssignment node)
        {
        }	
			
        public override void OutAIntdeclNum_declare(comp5210.node.AIntdeclNumDeclare node)
        {
        }
        public override void OutAFloatdeclNum_declare(comp5210.node.AFloatdeclNumDeclare node)
        {
        }
        public override void OutAArray_indexNum_declare(comp5210.node.AArrayIndexNumDeclare node)
        {
        }
		
        public override void OutAElseElsepart(comp5210.node.AElseElsepart node)
        {
        }
        public override void OutAIfElseIf(comp5210.node.AIfElseIf node)
        {
        }
        public override void OutAIf_If(comp5210.node.AIfIf node)
        {
        }
        public override void OutAWhile(comp5210.node.AWhile node)
        {
        }
		
        public override void OutAAndExpressions(comp5210.node.AAndExpressions node)
        {
        }
        public override void OutAOrExpressions(comp5210.node.AOrExpressions node)
        {
        }
        public override void OutANo_log_opExpressions(comp5210.node.ANoLogOpExpressions node)
        {
        }
		
        public override void OutAGreaterLogical_compare(comp5210.node.AGreaterLogicalCompare node)
        {
        }
        public override void OutALess_thanLogical_compare(comp5210.node.ALessThanLogicalCompare node)
        {
        }
        public override void OutAGreater_than_equal_toLogical_compare(comp5210.node.AGreaterThanEqualToLogicalCompare node)
        {
        }
        public override void OutALess_than_equal_toLogical_compare(comp5210.node.ALessThanEqualToLogicalCompare node)
        {
        }
        public override void OutAEqual_toLogical_compare(comp5210.node.AEqualToLogicalCompare node)
        {
        }
        public override void OutANo_log_compareLogical_compare(comp5210.node.ANoLogCompareLogicalCompare node)
        {
        }
		
        public override void OutAAddAdd_sub(comp5210.node.AAddAddSub node)
        {
        }
        public override void OutASubtractAdd_sub(comp5210.node.ASubtractAddSub node)
        {
        }
        public override void OutANo_more_add_subAdd_sub(comp5210.node.ANoMoreAddSubAddSub node)
        {
        }
		
        public override void OutAMultiplyMulti_div(comp5210.node.AMultiplyMultiDiv node)
        {
        }
        public override void OutADivideMulti_div(comp5210.node.ADivideMultiDiv node)
        {
        }
        public override void OutANo_more_div_multiMulti_div(comp5210.node.ANoMoreDivMultiMultiDiv node)
        {
        }
		
        public override void OutAParenthParenth(comp5210.node.AParenthParenth node)
        {
        }
        public override void OutAIntParenth(comp5210.node.AIntParenth node)
        {
        }
        public override void OutAFloatParenth(comp5210.node.AFloatParenth node)
        {
        }
        public override void OutAArrayParenth(comp5210.node.AArrayParenth node)
        {
        }
        public override void OutAVariableParenth(comp5210.node.AVariableParenth node)
        {
        }
    }
}
