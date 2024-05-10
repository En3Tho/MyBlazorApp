namespace MyBlazorApp.AppHost;

public static partial class DistributedApplicationBuilderExtensions
{
    public static IResourceBuilder<TDestination> WithReferences<TDestination>(this IResourceBuilder<TDestination> builder, IResourceBuilder<IResourceWithServiceDiscovery> source)
        where TDestination : IResourceWithEnvironment
    {
        builder.WithReference(source);
        foreach (var alias in source.Resource.Annotations.OfType<AliasResourceAnnotation>())
        {
            builder.WithReference(alias.Resource);
        }

        return builder;
    }

    public static IResourceBuilder<ProjectResource> WithAlias(this IResourceBuilder<ProjectResource> builder, string alias)
    {
        return builder.WithAnnotation(new AliasResourceAnnotation(new AliasResource(alias, builder.Resource)));
    }
}

public class AliasResource(string alias, ProjectResource resource) : IResourceWithServiceDiscovery, IResourceBuilder<AliasResource>
{
    public string Name => alias;
    public ResourceAnnotationCollection Annotations => resource.Annotations;

    public IResourceBuilder<AliasResource> WithAnnotation<TAnnotation>(TAnnotation annotation,
        ResourceAnnotationMutationBehavior behavior = ResourceAnnotationMutationBehavior.Append) where TAnnotation : IResourceAnnotation =>
        throw new NotSupportedException();

    public IDistributedApplicationBuilder ApplicationBuilder =>
        throw new NotSupportedException();

    public AliasResource Resource => this;
}

public class AliasResourceAnnotation(IResourceBuilder<AliasResource> aliasResource) : IResourceAnnotation
{
    public IResourceBuilder<AliasResource> Resource => aliasResource;
}