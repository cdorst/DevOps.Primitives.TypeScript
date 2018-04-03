﻿using ProtoBuf;

namespace DevOps.Primitives.TypeScript
{
    [ProtoContract]
    public class InterfaceDeclaration : TypeDeclaration
    {
        public InterfaceDeclaration() { }
        public InterfaceDeclaration(
            string identifier,
            string @namespace,
            ModifierList modifierList = null,
            ImportStatementList importStatementList = null,
            DocumentationCommentList documentationCommentList = null,
            DecoratorList attributeListCollection = null,
            TypeParameterList typeParameterList = null,
            ConstraintClauseList constraintClauseList = null,
            BaseList baseList = null,
            MethodList methodList = null,
            PropertyList propertyList = null)
            : base(
                  identifier,
                  @namespace,
                  modifierList,
                  importStatementList,
                  documentationCommentList,
                  attributeListCollection,
                  typeParameterList,
                  constraintClauseList,
                  baseList,
                  null,
                  null,
                  methodList,
                  propertyList)
        {
        }
    }
}