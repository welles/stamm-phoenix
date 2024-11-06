using FastEndpoints;

namespace StammPhoenix.Api.Core;

public abstract class PostEndpoint<TRequest, TResponse, TGroup>
    : EndpointBase<TRequest, TResponse, TGroup>
    where TRequest : notnull
    where TGroup : EndpointGroup, new()
{
    public override Http[] EndpointVerbs { get; } = [Http.POST];
}

public abstract class PostEndpoint<TRequest, TGroup> : PostEndpoint<TRequest, object?, TGroup>
    where TRequest : notnull
    where TGroup : EndpointGroup, new();

public abstract class PostEndpointWithoutRequest<TResponse, TGroup>
    : PostEndpoint<EmptyRequest, TResponse, TGroup>
    where TGroup : EndpointGroup, new();

public abstract class PostEndpointWithoutRequest<TGroup>
    : PostEndpoint<EmptyRequest, object?, TGroup>
    where TGroup : EndpointGroup, new();
