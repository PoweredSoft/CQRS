﻿using HotChocolate.Resolvers;
using HotChocolate.Types;
using PoweredSoft.CQRS.Abstractions;
using PoweredSoft.CQRS.Abstractions.Discovery;
using System;
using System.Collections.Generic;

namespace PoweredSoft.CQRS.GraphQL.HotChocolate
{
    public class MutationObjectType : ObjectTypeExtension
    {
        private readonly ICommandDiscovery commandDiscovery;

        public MutationObjectType(ICommandDiscovery commandDiscovery) : base()
        {
            this.commandDiscovery = commandDiscovery;
        }

        protected override void Configure(IObjectTypeDescriptor desc)
        {
            desc.Name("Mutation");
            foreach (var m in commandDiscovery.GetCommands())
            {
                var mutationField = desc.Field(m.LowerCamelCaseName);

                Type typeToGet;
                if (m.CommandResultType == null)
                    typeToGet = typeof(ICommandHandler<>).MakeGenericType(m.CommandType);
                else
                    typeToGet = typeof(ICommandHandler<,>).MakeGenericType(m.CommandType, m.CommandResultType);

                if (m.CommandResultType == null)
                    mutationField.Type(typeof(int?));
                else
                    mutationField.Type(m.CommandResultType);

                //queryField.Use((sp, d) => new MutationAuthorizationMiddleware(m.CommandType, d));

                if (m.CommandType.GetProperties().Length == 0)
                {
                    mutationField.Resolve(async ctx =>
                    {
                        var queryArgument = Activator.CreateInstance(m.CommandType);
                        return await HandleMutation(m.CommandResultType != null, ctx, typeToGet, queryArgument);
                    });

                    continue;
                }

                mutationField.Argument("params", t => t.Type(m.CommandType));

                mutationField.Resolve(async ctx =>
                {
                    var queryArgument = ctx.ArgumentValue<object>("params");
                    return await HandleMutation(m.CommandResultType != null, ctx, typeToGet, queryArgument);
                });

                mutationField.Use<MutationParamRequiredMiddleware>();
                mutationField.Use<MutationValidationMiddleware>();
            }
        }

        private async System.Threading.Tasks.Task<object> HandleMutation(bool hasResult, IResolverContext ctx, Type typeToGet, object queryArgument)
        {
            dynamic service = ctx.Service(typeToGet);

            if (hasResult)
            {
                var result = await service.HandleAsync((dynamic)queryArgument);
                return result;
            } 
            else
            {
                await service.HandleAsync((dynamic)queryArgument);
                return null;
            }
        }
    }
}
