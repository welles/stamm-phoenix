using FastEndpoints;

namespace StammPhoenix.Api.Core;

public abstract class DeleteEndpoint<TRequest, TResponse, TGroup>
    : EndpointBase<TRequest, TResponse, TGroup>
    where TRequest : notnull
    where TGroup : EndpointGroup, new()
{
    public override Http[] EndpointVerbs { get; } = [Http.DELETE];
}

public abstract class DeleteEndpoint<TRequest, TGroup> : DeleteEndpoint<TRequest, object?, TGroup>
    where TRequest : notnull
    where TGroup : EndpointGroup, new();

public abstract class DeleteEndpointWithoutRequest<TResponse, TGroup>
    : DeleteEndpoint<EmptyRequest, TResponse, TGroup>
    where TGroup : EndpointGroup, new();

public abstract class DeleteEndpointWithoutRequest<TGroup>
    : DeleteEndpoint<EmptyRequest, object?, TGroup>
    where TGroup : EndpointGroup, new();
