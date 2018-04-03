using ProtoBuf;

namespace DevOps.Primitives.TypeScript
{
    [ProtoContract]
    public class ClassDeclaration : TypeDeclaration
    {
        public ClassDeclaration() { }
        public ClassDeclaration(
            Identifier identifier,
            Namespace @namespace,
            ModifierList modifierList = null,
            UsingDirectiveList usingDirectiveList = null,
            DocumentationCommentList documentationCommentList = null,
            DecoratorList attributeListCollection = null,
            TypeParameterList typeParameterList = null,
            ConstraintClauseList constraintClauseList = null,
            BaseList baseList = null,
            ConstructorList constructorList = null,
            FieldList fieldList = null,
            MethodList methodList = null,
            PropertyList propertyList = null)
            : base(
                  identifier,
                  @namespace,
                  modifierList,
                  usingDirectiveList,
                  documentationCommentList,
                  attributeListCollection,
                  typeParameterList,
                  constraintClauseList,
                  baseList,
                  constructorList,
                  fieldList,
                  methodList,
                  propertyList)
        {
        }
        public ClassDeclaration(
            string identifier,
            string @namespace,
            ModifierList modifierList = null,
            UsingDirectiveList usingDirectiveList = null,
            DocumentationCommentList documentationCommentList = null,
            DecoratorList attributeListCollection = null,
            TypeParameterList typeParameterList = null,
            ConstraintClauseList constraintClauseList = null,
            BaseList baseList = null,
            ConstructorList constructorList = null,
            FieldList fieldList = null,
            MethodList methodList = null,
            PropertyList propertyList = null)
            : this(
                  new Identifier(identifier),
                  new Namespace(@namespace),
                  modifierList,
                  usingDirectiveList,
                  documentationCommentList,
                  attributeListCollection,
                  typeParameterList,
                  constraintClauseList,
                  baseList,
                  constructorList,
                  fieldList,
                  methodList,
                  propertyList)
        {
        }
    }
}
