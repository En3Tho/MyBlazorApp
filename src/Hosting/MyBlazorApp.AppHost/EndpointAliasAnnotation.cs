using Aspire.Hosting.ApplicationModel;

class EndpointAliasAnnotation(string alias) : IResourceAnnotation
{
    public string Alias => alias;
}