using System;
using Microsoft.AspNetCore.Components;
using MyBlazorApp.BlazorClient.Backend.Models;
using MyBlazorApp.Utility.FSharpHelpers;

namespace MyBlazorApp.ComponentsAndPages.Components;

public abstract class SubscribingComponentBase : ComponentBase, IDisposable
{
    private ComponentDataChangeEventHandler _handler;

    /// <summary>
    /// Used for binding StateHasChanged call of the component to component's data OnChangedEvent
    /// </summary>
    protected abstract ComponentData[] Subscriptions { get; }

    protected override void OnParametersSet()
    {
        base.OnParametersSet();
        _handler.Subscribe(() => InvokeAsync(StateHasChanged), Subscriptions);
    }

    public virtual void Dispose() => _handler.Unsubscribe();
}