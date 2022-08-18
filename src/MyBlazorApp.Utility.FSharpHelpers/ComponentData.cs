using System;

namespace MyBlazorApp.Utility.FSharpHelpers;

public abstract class ComponentData
{
    public event EventHandler OnChange;

    protected virtual void OnDataChanged()
    {
        var handler = OnChange;
        handler?.Invoke(this, EventArgs.Empty);
    }
}