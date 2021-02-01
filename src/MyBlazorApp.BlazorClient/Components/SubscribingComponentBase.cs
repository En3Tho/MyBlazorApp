using System;
using Microsoft.AspNetCore.Components;
using MyBlazorApp.BlazorClient.Backend.Models;
using MyBlazorApp.Utility.FSharpHelpers;

namespace MyBlazorApp.BlazorClient.Components
{
    public abstract class SubscribingComponentBase : ComponentBase, IDisposable
    {
        private ComponentDataChangeEventHandler _handler;
        protected abstract ComponentData[] Subscriptions { get; }

        protected override void OnParametersSet()
        {
            base.OnParametersSet();
            _handler.Subscribe(StateHasChanged, Subscriptions);
        }

        public virtual void Dispose() => _handler.Unsubscribe();
    }
}