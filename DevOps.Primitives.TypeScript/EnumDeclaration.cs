using ProtoBuf;

namespace DevOps.Primitives.TypeScript
{
    [ProtoContract]
    public class EnumDeclaration : TypeDeclaration
    {
        public EnumDeclaration() { }
        public EnumDeclaration(
            Identifier identifier,
            Namespace @namespace,
            ModifierList modifierList = null,
            ImportStatementList ImportStatementList = null,
            DocumentationCommentList documentationCommentList = null,
            DecoratorList attributeListCollection = null,
            TypeParameterList typeParameterList = null,
            ConstraintClauseList constraintClauseList = null,
            BaseList baseList = null,
            ConstructorList constructorList = null,
            FieldList fieldList = null,
            MethodList methodList = null,
            PropertyList propertyList = null,
            EnumMemberList enumMemberList = null)
            : base(
                  identifier,
                  @namespace,
                  modifierList,
                  ImportStatementList,
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
            EnumMemberList = enumMemberList;
        }
        public EnumDeclaration(
            string identifier,
            string @namespace,
            ModifierList modifierList = null,
            ImportStatementList ImportStatementList = null,
            DocumentationCommentList documentationCommentList = null,
            DecoratorList attributeListCollection = null,
            TypeParameterList typeParameterList = null,
            ConstraintClauseList constraintClauseList = null,
            BaseList baseList = null,
            ConstructorList constructorList = null,
            FieldList fieldList = null,
            MethodList methodList = null,
            PropertyList propertyList = null,
            EnumMemberList enumMemberList = null)
            : this(
                  new Identifier(identifier),
                  new Namespace(@namespace),
                  modifierList,
                  ImportStatementList,
                  documentationCommentList,
                  attributeListCollection,
                  typeParameterList,
                  constraintClauseList,
                  baseList,
                  constructorList,
                  fieldList,
                  methodList,
                  propertyList,
                  enumMemberList)
        {
        }

        [ProtoMember(28)]
        public EnumMemberList EnumMemberList { get; set; }
        [ProtoMember(29)]
        public int EnumMemberListId { get; set; }
    }
}
