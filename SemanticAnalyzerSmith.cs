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
        }
		
        public override void OutAManyConstants(comp5210.node.AManyConstants node)
        {
        }
        public override void OutANothingConstants(comp5210.node.ANothingConstants node)
        {
        }
		
        public override void OutAIntdeclConst_declare(comp5210.node.AIntdeclConst_declare node)
        {
        }
        public override void OutAFloatdeclConst_declare(comp5210.node.AFloatdeclConst_declare node)
        {
        }
		
        public override void OutAMany_methodsMethods(comp5210.node.AMany_methodsMethods node)
        {
        }
        public override void OutALast_methodMethods(comp5210.node.ALast_methodMethods node)
        {
        }
		
        public override void OutAMethod(comp5210.node.AMethod node)
        {
        }
		
        public override void OutAMethod_call(comp5210.node.AMethod_call node)
        {
        }
		
        public override void OutAString_paramActual_parameters(comp5210.node.AString_paramActual_parameters node)
        {
        }
        public override void OutAInt_parametersActual_parameters(comp5210.node.AInt_parametersActual_parameters node)
        {
        }
        public override void OutAFloat_ParametersActual_parameters(comp5210.node.AFloat_parametersActual_parameters node)
        {
        }
        public override void OutAVar_parametersActual_parameters(comp5210.node.AVar_parametersActual_parameters node)
        {
        }
        public override void OutAStringActual_parameters(comp5210.node.AStringActual_parameters node)
        {
        }
        public override void OutAVariableActual_parameters(comp5210.node.AVariableActual_parameters node)
        {
        }
        public override void OutAIntActual_parameters(comp5210.node.AIntActual_parameters node)
        {
        }
        public override void OutAFloatActual_parameters(comp5210.node.AFloatActual_parameters node)
        {
        }
        public override void OutANothingActual_parameters(comp5210.node.ANothingActual_parameters node)
        {
        }
		
        public override void OutAMany_parametersFormal_parameters(comp5210.node.AMany_parametersFormal_parameters node)
        {
        }
        public override void OutAFinal_parametersFormal_parameters(comp5210.node.AFinal_parametersFormal_parameters node)
        {
        }
		
        public override void OutAMany_statementsStatements(comp5210.node.AMany_statementsStatements node)
        {
        }
        public override void OutAOne_statementFormal_parameters(comp5210.node.AOne_statementFormal_parameters node)
        {
        }		
		
        public override void OutAAssignmentStatement(comp5210.node.AAssignmentStatement node)
        {
        }
        public override void OutANum_declareStatement(comp5210.node.ANum_declareStatement node)
        {
        }	
        public override void OutAMethod_callStatement(comp5210.node.AMethod_callStatement node)
        {
        }
        public override void OutAIfStatement(comp5210.node.AIfStatement node)
        {
        }	
        public override void OutAWhileStatement(comp5210.node.AWhileStatement node)
        {
        }
		
        public override void OutAAssign_intAssignment(comp5210.node.AAssign_intAssignment node)
        {
        }	
        public override void OutAVar_DeclareAssignment(comp5210.node.AVar_DeclareAssignment node)
        {
        }
        public override void OutAArray_assign_math_indexAssignment(comp5210.node.AArray_assign_math_indexAssignment node)
        {
        }	
        public override void OutAArray_assignAssignment(comp5210.node.AArray_assignAssignment node)
        {
        }
			
        public override void OutAIntdeclNum_declare(comp5210.node.AIntdeclNum_declare node)
        {
        }
        public override void OutAFloatdeclNum_declare(comp5210.node.AFloatdeclNum_declare node)
        {
        }
        public override void OutAArray_indexNum_declare(comp5210.node.AArray_indexNum_declare node)
        {
        }
		
        public override void OutAElseElsepart(comp5210.node.AElseElsepart node)
        {
        }
        public override void OutANo_elseElsepart(comp5210.node.ANo_elseElsepart node)
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
        public override void OutANotExpressions(comp5210.node.ANotExpressions node)
        {
        }
        public override void OutANo_log_opExpressions(comp5210.node.ANo_log_opExpressions node)
        {
        }
		
        public override void OutAGreaterLogical_compare(comp5210.node.AGreaterLogical_compare node)
        {
        }
        public override void OutALess_thanLogical_compare(comp5210.node.ALess_thanLogical_compare node)
        {
        }
        public override void OutAGreater_than_equal_toLogical_compare(comp5210.node.AGreater_than_equal_toLogical_compare node)
        {
        }
        public override void OutALess_than_equal_toLogical_compare(comp5210.node.ALess_than_equal_toLogical_compare node)
        {
        }
        public override void OutAEqual_toLogical_compare(comp5210.node.AEqual_toLogical_compare node)
        {
        }
        public override void OutANo_log_compareLogical_compare(comp5210.node.ANo_log_compareLogical_compare node)
        {
        }
		
        public override void OutAAddAdd_sub(comp5210.node.AAddAdd_Sub node)
        {
        }
        public override void OutASubtractAdd_sub(comp5210.node.ASubtractAdd_Sub node)
        {
        }
        public override void OutANo_more_add_subAdd_sub(comp5210.node.ANo_more_add_subAdd_Sub node)
        {
        }
		
        public override void OutAMultiplyMulti_div(comp5210.node.AMultiplyMulti_div node)
        {
        }
        public override void OutADivideMulti_div(comp5210.node.ADivideMulti_div node)
        {
        }
        public override void OutANo_more_div_multiMulti_div(comp5210.node.ANo_more_div_multiMulti_div node)
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
