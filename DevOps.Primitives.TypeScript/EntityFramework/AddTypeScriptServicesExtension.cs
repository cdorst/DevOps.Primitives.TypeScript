﻿using Common.EntityFrameworkServices;
using DevOps.Primitives.TypeScript.EntityFramework.Services;
using DevOps.Primitives.Strings.EntityFramework;
using Microsoft.Extensions.DependencyInjection;

namespace DevOps.Primitives.TypeScript.EntityFramework
{
    public static class AddTypeScriptServicesExtension
    {
        public static IServiceCollection AddTypeScriptServices<TDbContext>(this IServiceCollection services)
            where TDbContext : TypeScriptDbContext
            => services
                .AddUniqueStringsServices<TDbContext>()
                .AddScoped<IUpsertService<TDbContext, ArgumentListAssociation>, ArgumentListAssociationUpsertService<TDbContext>>()
                .AddScoped<IUpsertService<TDbContext, ArgumentList>, ArgumentListUpsertService<TDbContext>>()
                .AddScoped<IUpsertService<TDbContext, Argument>, ArgumentUpsertService<TDbContext>>()
                .AddScoped<IUpsertService<TDbContext, DecoratorArgumentListExpression>, DecoratorArgumentListExpressionUpsertService<TDbContext>>()
                .AddScoped<IUpsertService<TDbContext, DecoratorListAssociation>, DecoratorListAssociationUpsertService<TDbContext>>()
                .AddScoped<IUpsertService<TDbContext, DecoratorList>, DecoratorListUpsertService<TDbContext>>()
                .AddScoped<IUpsertService<TDbContext, Decorator>, DecoratorUpsertService<TDbContext>>()
                .AddScoped<IUpsertService<TDbContext, BaseListAssociation>, BaseListAssociationUpsertService<TDbContext>>()
                .AddScoped<IUpsertService<TDbContext, BaseList>, BaseListUpsertService<TDbContext>>()
                .AddScoped<IUpsertService<TDbContext, BaseType>, BaseTypeUpsertService<TDbContext>>()
                .AddScoped<IUpsertService<TDbContext, Block>, BlockUpsertService<TDbContext>>()
                .AddScoped<IUpsertService<TDbContext, ClassDeclaration>, ClassDeclarationUpsertService<TDbContext>>()
                .AddScoped<IUpsertService<TDbContext, Constructor>, ConstructorUpsertService<TDbContext>>()
                .AddScoped<IUpsertService<TDbContext, DocumentationComment>, DocumentationCommentUpsertService<TDbContext>>()
                .AddScoped<IUpsertService<TDbContext, EnumDeclaration>, EnumDeclarationUpsertService<TDbContext>>()
                .AddScoped<IUpsertService<TDbContext, EnumMemberListAssociation>, EnumMemberListAssociationUpsertService<TDbContext>>()
                .AddScoped<IUpsertService<TDbContext, EnumMemberList>, EnumMemberListUpsertService<TDbContext>>()
                .AddScoped<IUpsertService<TDbContext, EnumMember>, EnumMemberUpsertService<TDbContext>>()
                .AddScoped<IUpsertService<TDbContext, Expression>, ExpressionUpsertService<TDbContext>>()
                .AddScoped<IUpsertService<TDbContext, Identifier>, IdentifierUpsertService<TDbContext>>()
                .AddScoped<IUpsertService<TDbContext, ImportStatementListAssociation>, ImportStatementListAssociationUpsertService<TDbContext>>()
                .AddScoped<IUpsertService<TDbContext, ImportStatementList>, ImportStatementListUpsertService<TDbContext>>()
                .AddScoped<IUpsertService<TDbContext, ImportStatement>, ImportStatementUpsertService<TDbContext>>()
                .AddScoped<IUpsertService<TDbContext, InterfaceDeclaration>, InterfaceDeclarationUpsertService<TDbContext>>()
                .AddScoped<IUpsertService<TDbContext, MethodListAssociation>, MethodListAssociationUpsertService<TDbContext>>()
                .AddScoped<IUpsertService<TDbContext, MethodList>, MethodListUpsertService<TDbContext>>()
                .AddScoped<IUpsertService<TDbContext, Method>, MethodUpsertService<TDbContext>>()
                .AddScoped<IUpsertService<TDbContext, Namespace>, NamespaceUpsertService<TDbContext>>()
                .AddScoped<IUpsertService<TDbContext, ParameterListAssociation>, ParameterListAssociationUpsertService<TDbContext>>()
                .AddScoped<IUpsertService<TDbContext, ParameterList>, ParameterListUpsertService<TDbContext>>()
                .AddScoped<IUpsertService<TDbContext, Parameter>, ParameterUpsertService<TDbContext>>()
                .AddScoped<IUpsertService<TDbContext, PropertyListAssociation>, PropertyListAssociationUpsertService<TDbContext>>()
                .AddScoped<IUpsertService<TDbContext, PropertyList>, PropertyListUpsertService<TDbContext>>()
                .AddScoped<IUpsertService<TDbContext, Property>, PropertyUpsertService<TDbContext>>()
                .AddScoped<IUpsertService<TDbContext, StatementListAssociation>, StatementListAssociationUpsertService<TDbContext>>()
                .AddScoped<IUpsertService<TDbContext, StatementList>, StatementListUpsertService<TDbContext>>()
                .AddScoped<IUpsertService<TDbContext, Statement>, StatementUpsertService<TDbContext>>()
                .AddScoped<IUpsertService<TDbContext, TypeArgumentListAssociation>, TypeArgumentListAssociationUpsertService<TDbContext>>()
                .AddScoped<IUpsertService<TDbContext, TypeArgumentList>, TypeArgumentListUpsertService<TDbContext>>()
                .AddScoped<IUpsertService<TDbContext, TypeArgument>, TypeArgumentUpsertService<TDbContext>>()
                .AddScoped<IUpsertService<TDbContext, TypeParameterListAssociation>, TypeParameterListAssociationUpsertService<TDbContext>>()
                .AddScoped<IUpsertService<TDbContext, TypeParameterList>, TypeParameterListUpsertService<TDbContext>>()
                .AddScoped<IUpsertService<TDbContext, TypeParameter>, TypeParameterUpsertService<TDbContext>>();
    }
}
