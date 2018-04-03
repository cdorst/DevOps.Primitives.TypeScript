using DevOps.Primitives.Strings.EntityFramework;
using Microsoft.EntityFrameworkCore;

namespace DevOps.Primitives.TypeScript.EntityFramework
{
    public class TypeScriptDbContext : UniqueStringsDbContext
    {
        public TypeScriptDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Accessor> Accessors { get; set; }
        public DbSet<AccessorList> AccessorLists { get; set; }
        public DbSet<AccessorListAssociation> AccessorListAssociations { get; set; }
        public DbSet<Argument> Arguments { get; set; }
        public DbSet<ArgumentList> ArgumentLists { get; set; }
        public DbSet<ArgumentListAssociation> ArgumentListAssociations { get; set; }
        public DbSet<BaseList> BaseLists { get; set; }
        public DbSet<BaseListAssociation> BaseListAssociations { get; set; }
        public DbSet<BaseType> BaseTypes { get; set; }
        public DbSet<Block> Blocks { get; set; }
        public DbSet<ClassDeclaration> ClassDeclarations { get; set; }
        public DbSet<Constraint> Constraints { get; set; }
        public DbSet<ConstraintClause> ConstraintClauses { get; set; }
        public DbSet<ConstraintClauseListAssociation> ConstraintClauseListAssociations { get; set; }
        public DbSet<ConstraintClauseList> ConstraintClauseLists { get; set; }
        public DbSet<ConstraintList> ConstraintLists { get; set; }
        public DbSet<ConstraintListAssociation> ConstraintListAssociations { get; set; }
        public DbSet<Constructor> Constructors { get; set; }
        public DbSet<ConstructorList> ConstructorLists { get; set; }
        public DbSet<ConstructorListAssociation> ConstructorListAssociations { get; set; }
        public DbSet<Decorator> Decorators { get; set; }
        public DbSet<DecoratorArgumentListExpression> DecoratorArgumentListExpressions { get; set; }
        public DbSet<DecoratorList> DecoratorLists { get; set; }
        public DbSet<DecoratorListAssociation> DecoratorListAssociations { get; set; }
        public DbSet<DocumentationComment> DocumentationComments { get; set; }
        public DbSet<DocumentationCommentAttribute> DocumentationCommentAttributes { get; set; }
        public DbSet<DocumentationCommentAttributeList> DocumentationCommentAttributeLists { get; set; }
        public DbSet<DocumentationCommentAttributeListAssociation> DocumentationCommentAttributeListAssociations { get; set; }
        public DbSet<DocumentationCommentList> DocumentationCommentLists { get; set; }
        public DbSet<DocumentationCommentListAssociation> DocumentationCommentListAssociations { get; set; }
        public DbSet<EnumDeclaration> EnumDeclarations { get; set; }
        public DbSet<EnumMember> EnumMembers { get; set; }
        public DbSet<EnumMemberList> EnumMemberLists { get; set; }
        public DbSet<EnumMemberListAssociation> EnumMemberListAssociations { get; set; }
        public DbSet<Expression> Expressions { get; set; }
        public DbSet<Field> Fields { get; set; }
        public DbSet<FieldList> FieldLists { get; set; }
        public DbSet<FieldListAssociation> FieldListAssociations { get; set; }
        public DbSet<Identifier> Identifiers { get; set; }
        public DbSet<ImportStatement> ImportStatements { get; set; }
        public DbSet<ImportStatementList> ImportStatementLists { get; set; }
        public DbSet<ImportStatementListAssociation> ImportStatementListAssociations { get; set; }
        public DbSet<InterfaceDeclaration> InterfaceDeclarations { get; set; }
        public DbSet<Method> Methods { get; set; }
        public DbSet<MethodList> MethodLists { get; set; }
        public DbSet<MethodListAssociation> MethodListAssociations { get; set; }
        public DbSet<ModifierList> ModifierLists { get; set; }
        public DbSet<ModifierListAssociation> ModifierListAssociations { get; set; }
        public DbSet<Namespace> Namespaces { get; set; }
        public DbSet<Parameter> Parameters { get; set; }
        public DbSet<ParameterList> ParameterLists { get; set; }
        public DbSet<ParameterListAssociation> ParameterListAssociations { get; set; }
        public DbSet<Property> Properties { get; set; }
        public DbSet<PropertyList> PropertyLists { get; set; }
        public DbSet<PropertyListAssociation> PropertyListAssociations { get; set; }
        public DbSet<Statement> Statements { get; set; }
        public DbSet<StatementList> StatementLists { get; set; }
        public DbSet<StatementListAssociation> StatementListAssociations { get; set; }
        public DbSet<SyntaxToken> SyntaxTokens { get; set; }
        public DbSet<TypeArgument> TypeArguments { get; set; }
        public DbSet<TypeArgumentList> TypeArgumentLists { get; set; }
        public DbSet<TypeArgumentListAssociation> TypeArgumentListAssociations { get; set; }
        public DbSet<TypeDeclaration> TypeDeclarations { get; set; }
        public DbSet<TypeParameter> TypeParameters { get; set; }
        public DbSet<TypeParameterList> TypeParameterLists { get; set; }
        public DbSet<TypeParameterListAssociation> TypeParameterListAssociations { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            AddIndexes(modelBuilder);
        }

        private void AddIndexes(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Accessor>()
                .HasIndex(e => new { e.BodyId, e.SyntaxTokenId }).IsUnique();
            modelBuilder.Entity<AccessorList>()
                .HasIndex(e => new { e.ListIdentifierId }).IsUnique();
            modelBuilder.Entity<AccessorListAssociation>()
                .HasIndex(e => new { e.AccessorId, e.AccessorListId }).IsUnique();
            modelBuilder.Entity<Argument>()
                .HasIndex(e => new { e.IdentifierId }).IsUnique();
            modelBuilder.Entity<ArgumentList>()
                .HasIndex(e => new { e.ListIdentifierId }).IsUnique();
            modelBuilder.Entity<ArgumentListAssociation>()
                .HasIndex(e => new { e.ArgumentId, e.ArgumentListId }).IsUnique();
            modelBuilder.Entity<BaseList>()
                .HasIndex(e => new { e.ListIdentifierId }).IsUnique();
            modelBuilder.Entity<BaseListAssociation>()
                .HasIndex(e => new { e.BaseTypeId, e.BaseListId }).IsUnique();
            modelBuilder.Entity<BaseType>()
                .HasIndex(e => new { e.IdentifierId, e.TypeArgumentListId }).IsUnique();
            modelBuilder.Entity<Block>()
                .HasIndex(e => new { e.StatementListId }).IsUnique();
            modelBuilder.Entity<Constraint>()
                .HasIndex(e => new { e.IdentifierId }).IsUnique();
            modelBuilder.Entity<ConstraintClause>()
                .HasIndex(e => new { e.ConstraintListId, e.IdentifierId }).IsUnique();
            modelBuilder.Entity<ConstraintClauseList>()
                .HasIndex(e => new { e.ListIdentifierId }).IsUnique();
            modelBuilder.Entity<ConstraintClauseListAssociation>()
                .HasIndex(e => new { e.ConstraintClauseId, e.ConstraintClauseListId }).IsUnique();
            modelBuilder.Entity<ConstraintList>()
                .HasIndex(e => new { e.ListIdentifierId }).IsUnique();
            modelBuilder.Entity<ConstraintListAssociation>()
                .HasIndex(e => new { e.ConstraintId, e.ConstraintListId }).IsUnique();
            modelBuilder.Entity<Constructor>()
                .HasIndex(e => new { e.DecoratorListId, e.BlockId, e.DocumentationCommentListId, e.IdentifierId, e.ParameterListId }).IsUnique();
            modelBuilder.Entity<ConstructorList>()
                .HasIndex(e => new { e.ListIdentifierId }).IsUnique();
            modelBuilder.Entity<ConstructorListAssociation>()
                .HasIndex(e => new { e.ConstructorId, e.ConstructorListId }).IsUnique();
            modelBuilder.Entity<Decorator>()
                .HasIndex(e => new { e.IdentifierId, e.DecoratorArgumentListExpressionId }).IsUnique();
            modelBuilder.Entity<DecoratorArgumentListExpression>()
                .HasIndex(e => new { e.ExpressionId }).IsUnique();
            modelBuilder.Entity<DecoratorList>()
                .HasIndex(e => new { e.ListIdentifierId }).IsUnique();
            modelBuilder.Entity<DecoratorListAssociation>()
                .HasIndex(e => new { e.DecoratorId, e.DecoratorListId }).IsUnique();
            modelBuilder.Entity<DocumentationComment>()
                .HasIndex(e => new { e.IncludeNewLine, e.IndentLevel, e.IdentifierId, e.TextId }).IsUnique();
            modelBuilder.Entity<DocumentationCommentAttribute>()
                .HasIndex(e => new { e.IdentifierId, e.ValueId }).IsUnique();
            modelBuilder.Entity<DocumentationCommentAttributeList>()
                .HasIndex(e => new { e.ListIdentifierId }).IsUnique();
            modelBuilder.Entity<DocumentationCommentAttributeListAssociation>()
                .HasIndex(e => new { e.DocumentationCommentAttributeId, e.DocumentationCommentAttributeListId }).IsUnique();
            modelBuilder.Entity<DocumentationCommentList>()
                .HasIndex(e => new { e.ListIdentifierId }).IsUnique();
            modelBuilder.Entity<DocumentationCommentListAssociation>()
                .HasIndex(e => new { e.DocumentationCommentId, e.DocumentationCommentListId }).IsUnique();
            modelBuilder.Entity<EnumMember>()
                .HasIndex(e => new { e.EqualsValue, e.IdentifierId, e.DocumentationCommentListId, e.AttributeListCollectionId }).IsUnique();
            modelBuilder.Entity<EnumMemberList>()
                .HasIndex(e => new { e.ListIdentifierId }).IsUnique();
            modelBuilder.Entity<EnumMemberListAssociation>()
                .HasIndex(e => new { e.EnumMemberListId, e.EnumMemberId }).IsUnique();
            modelBuilder.Entity<Expression>()
                .HasIndex(e => new { e.TextId }).IsUnique();
            modelBuilder.Entity<Field>()
                .HasIndex(e => new { e.AttributeListCollectionId, e.DocumentationCommentListId, e.IdentifierId, e.InitializerId, e.ModifierListId, e.TypeId }).IsUnique();
            modelBuilder.Entity<FieldList>()
                .HasIndex(e => new { e.ListIdentifierId }).IsUnique();
            modelBuilder.Entity<FieldListAssociation>()
                .HasIndex(e => new { e.FieldId, e.FieldListId }).IsUnique();
            modelBuilder.Entity<Identifier>()
                .HasIndex(e => new { e.NameId }).IsUnique();
            modelBuilder.Entity<ImportStatement>()
                .HasIndex(e => new { e.ExportNameId, e.ModuleNameId }).IsUnique();
            modelBuilder.Entity<ImportStatementList>()
                .HasIndex(e => new { e.ListIdentifierId }).IsUnique();
            modelBuilder.Entity<ImportStatementListAssociation>()
                .HasIndex(e => new { e.ImportStatementId, e.ImportStatementListId }).IsUnique();
            modelBuilder.Entity<Method>()
                .HasIndex(e => new { e.ArrowClauseExpressionValueId, e.AttributeListCollectionId, e.BlockId, e.ConstraintClauseListId, e.DocumentationCommentListId, e.IdentifierId, e.ModifierListId, e.ParameterListId, e.TypeId, e.TypeParameterListId }).IsUnique();
            modelBuilder.Entity<MethodList>()
                .HasIndex(e => new { e.ListIdentifierId }).IsUnique();
            modelBuilder.Entity<MethodListAssociation>()
                .HasIndex(e => new { e.MethodId, e.MethodListId }).IsUnique();
            modelBuilder.Entity<ModifierList>()
                .HasIndex(e => new { e.ListIdentifierId }).IsUnique();
            modelBuilder.Entity<ModifierListAssociation>()
                .HasIndex(e => new { e.ModifierListId, e.SyntaxTokenId }).IsUnique();
            modelBuilder.Entity<Namespace>()
                .HasIndex(e => new { e.IdentifierId }).IsUnique();
            modelBuilder.Entity<Parameter>()
                .HasIndex(e => new { e.AttributeListCollectionId, e.DefaultValueId, e.IdentifierId, e.TypeId }).IsUnique();
            modelBuilder.Entity<ParameterList>()
                .HasIndex(e => new { e.ListIdentifierId }).IsUnique();
            modelBuilder.Entity<ParameterListAssociation>()
                .HasIndex(e => new { e.ParameterId, e.ParameterListId }).IsUnique();
            modelBuilder.Entity<Property>()
                .HasIndex(e => new { e.AccessorListId, e.AttributeListCollectionId, e.DocumentationCommentListId, e.IdentifierId, e.ModifierListId, e.TypeId }).IsUnique();
            modelBuilder.Entity<PropertyList>()
                .HasIndex(e => new { e.ListIdentifierId }).IsUnique();
            modelBuilder.Entity<PropertyListAssociation>()
                .HasIndex(e => new { e.PropertyId, e.PropertyListId }).IsUnique();
            modelBuilder.Entity<Statement>()
                .HasIndex(e => new { e.TextId }).IsUnique();
            modelBuilder.Entity<StatementList>()
                .HasIndex(e => new { e.ListIdentifierId }).IsUnique();
            modelBuilder.Entity<StatementListAssociation>()
                .HasIndex(e => new { e.StatementId, e.StatementListId }).IsUnique();
            modelBuilder.Entity<SyntaxToken>()
                .HasIndex(e => new { e.SyntaxKind }).IsUnique();
            modelBuilder.Entity<TypeArgument>()
                .HasIndex(e => new { e.IdentifierId }).IsUnique();
            modelBuilder.Entity<TypeArgumentList>()
                .HasIndex(e => new { e.ListIdentifierId }).IsUnique();
            modelBuilder.Entity<TypeArgumentListAssociation>()
                .HasIndex(e => new { e.TypeArgumentId, e.TypeArgumentListAssociationId }).IsUnique();
            modelBuilder.Entity<TypeDeclaration>()
                .HasIndex(e => new { e.IdentifierId, e.NamespaceId }).IsUnique();
            modelBuilder.Entity<TypeParameter>()
                .HasIndex(e => new { e.IdentifierId }).IsUnique();
            modelBuilder.Entity<TypeParameterList>()
                .HasIndex(e => new { e.ListIdentifierId }).IsUnique();
            modelBuilder.Entity<TypeParameterListAssociation>()
                .HasIndex(e => new { e.TypeParameterId, e.TypeParameterListId }).IsUnique();
        }
    }
}
