using DevOps.Primitives.Strings.EntityFramework;
using Microsoft.EntityFrameworkCore;

namespace DevOps.Primitives.TypeScript.EntityFramework
{
    public class TypeScriptDbContext : UniqueStringsDbContext
    {
        public TypeScriptDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Argument> Arguments { get; set; }
        public DbSet<ArgumentList> ArgumentLists { get; set; }
        public DbSet<ArgumentListAssociation> ArgumentListAssociations { get; set; }
        public DbSet<BaseList> BaseLists { get; set; }
        public DbSet<BaseListAssociation> BaseListAssociations { get; set; }
        public DbSet<BaseType> BaseTypes { get; set; }
        public DbSet<Block> Blocks { get; set; }
        public DbSet<ClassDeclaration> ClassDeclarations { get; set; }
        public DbSet<Constructor> Constructors { get; set; }
        public DbSet<Decorator> Decorators { get; set; }
        public DbSet<DecoratorArgumentListExpression> DecoratorArgumentListExpressions { get; set; }
        public DbSet<DecoratorList> DecoratorLists { get; set; }
        public DbSet<DecoratorListAssociation> DecoratorListAssociations { get; set; }
        public DbSet<DocumentationComment> DocumentationComments { get; set; }
        public DbSet<EnumDeclaration> EnumDeclarations { get; set; }
        public DbSet<EnumMember> EnumMembers { get; set; }
        public DbSet<EnumMemberList> EnumMemberLists { get; set; }
        public DbSet<EnumMemberListAssociation> EnumMemberListAssociations { get; set; }
        public DbSet<Expression> Expressions { get; set; }
        public DbSet<Identifier> Identifiers { get; set; }
        public DbSet<ImportStatement> ImportStatements { get; set; }
        public DbSet<ImportStatementList> ImportStatementLists { get; set; }
        public DbSet<ImportStatementListAssociation> ImportStatementListAssociations { get; set; }
        public DbSet<InterfaceDeclaration> InterfaceDeclarations { get; set; }
        public DbSet<Method> Methods { get; set; }
        public DbSet<MethodList> MethodLists { get; set; }
        public DbSet<MethodListAssociation> MethodListAssociations { get; set; }
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
            modelBuilder.Entity<Constructor>()
                .HasIndex(e => new { e.AccessModifier, e.BlockId, e.DecoratorListId, e.IdentifierId, e.ParameterListId }).IsUnique();
            modelBuilder.Entity<Decorator>()
                .HasIndex(e => new { e.IdentifierId, e.DecoratorArgumentListExpressionId }).IsUnique();
            modelBuilder.Entity<DecoratorArgumentListExpression>()
                .HasIndex(e => new { e.ExpressionId }).IsUnique();
            modelBuilder.Entity<DecoratorList>()
                .HasIndex(e => new { e.ListIdentifierId }).IsUnique();
            modelBuilder.Entity<DecoratorListAssociation>()
                .HasIndex(e => new { e.DecoratorId, e.DecoratorListId }).IsUnique();
            modelBuilder.Entity<DocumentationComment>()
                .HasIndex(e => new { e.TextId }).IsUnique();
            modelBuilder.Entity<EnumMember>()
                .HasIndex(e => new { e.DocumentationCommentId, e.EqualsValueId, e.IdentifierId }).IsUnique();
            modelBuilder.Entity<EnumMemberList>()
                .HasIndex(e => new { e.ListIdentifierId }).IsUnique();
            modelBuilder.Entity<EnumMemberListAssociation>()
                .HasIndex(e => new { e.EnumMemberListId, e.EnumMemberId }).IsUnique();
            modelBuilder.Entity<Expression>()
                .HasIndex(e => new { e.TextId }).IsUnique();
            modelBuilder.Entity<Identifier>()
                .HasIndex(e => new { e.NameId }).IsUnique();
            modelBuilder.Entity<ImportStatement>()
                .HasIndex(e => new { e.ExportNameId, e.ModuleNameId }).IsUnique();
            modelBuilder.Entity<ImportStatementList>()
                .HasIndex(e => new { e.ListIdentifierId }).IsUnique();
            modelBuilder.Entity<ImportStatementListAssociation>()
                .HasIndex(e => new { e.ImportStatementId, e.ImportStatementListId }).IsUnique();
            modelBuilder.Entity<Method>()
                .HasIndex(e => new { e.AccessModifier, e.IsAsync, e.BlockId, e.DecoratorListId, e.DocumentationCommentId, e.IdentifierId, e.ParameterListId, e.TypeId, e.TypeParameterListId }).IsUnique();
            modelBuilder.Entity<MethodList>()
                .HasIndex(e => new { e.ListIdentifierId }).IsUnique();
            modelBuilder.Entity<MethodListAssociation>()
                .HasIndex(e => new { e.MethodId, e.MethodListId }).IsUnique();
            modelBuilder.Entity<Namespace>()
                .HasIndex(e => new { e.IdentifierId }).IsUnique();
            modelBuilder.Entity<Parameter>()
                .HasIndex(e => new { e.IsReadonly, e.DecoratorListId, e.DefaultValueId, e.DocumentationCommentId, e.IdentifierId, e.TypeId }).IsUnique();
            modelBuilder.Entity<ParameterList>()
                .HasIndex(e => new { e.ListIdentifierId }).IsUnique();
            modelBuilder.Entity<ParameterListAssociation>()
                .HasIndex(e => new { e.ParameterId, e.ParameterListId }).IsUnique();
            modelBuilder.Entity<Property>()
                .HasIndex(e => new { e.AccessModifier, e.IsStatic, e.IsReadonly, e.DecoratorListId, e.DefaultValueId, e.DocumentationCommentId, e.IdentifierId, e.TypeId }).IsUnique();
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
            modelBuilder.Entity<TypeArgument>()
                .HasIndex(e => new { e.IdentifierId }).IsUnique();
            modelBuilder.Entity<TypeArgumentList>()
                .HasIndex(e => new { e.ListIdentifierId }).IsUnique();
            modelBuilder.Entity<TypeArgumentListAssociation>()
                .HasIndex(e => new { e.TypeArgumentId, e.TypeArgumentListAssociationId }).IsUnique();
            modelBuilder.Entity<TypeDeclaration>()
                .HasIndex(e => new { e.IdentifierId, e.NamespaceId }).IsUnique();
            modelBuilder.Entity<TypeParameter>()
                .HasIndex(e => new { e.IdentifierId, e.ExtendsConstraintId }).IsUnique();
            modelBuilder.Entity<TypeParameterList>()
                .HasIndex(e => new { e.ListIdentifierId }).IsUnique();
            modelBuilder.Entity<TypeParameterListAssociation>()
                .HasIndex(e => new { e.TypeParameterId, e.TypeParameterListId }).IsUnique();
        }
    }
}
