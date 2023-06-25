// Auto-generated
namespace Microsoft.AspNetCore.Components.QuickGrid
open System.Runtime.CompilerServices
open En3Tho.FSharp.BlazorBuilder.Core

[<AutoOpen>]
module PropertyColumn__2__Import =
    open Microsoft.AspNetCore.Components.QuickGrid
    open System
    open System.Linq.Expressions
    open Microsoft.AspNetCore.Components
    open Microsoft.AspNetCore.Components.Web.Virtualization

    type [<Struct; IsReadOnly>] PropertyColumn__2__Import<'TGridItem, 'TProp>(builder: BlazorBuilderCore) =

        member this.Format with set(value: String) =
            builder.AddAttribute("Format", value)

        member this.Title with set(value: String) =
            builder.AddAttribute("Title", value)

        member this.Class with set(value: String) =
            builder.AddAttribute("Class", value)

        member this.Align with set(value: Align) =
            builder.AddAttribute("Align", value)

        member this.HeaderTemplate with set(value: RenderFragment<ColumnBase<'TGridItem>>) =
            builder.AddAttribute("HeaderTemplate", value)

        member this.ColumnOptions with set(value: RenderFragment) =
            builder.AddAttribute("ColumnOptions", value)

        member this.Sortable with set(value: Nullable<Boolean>) =
            builder.AddAttribute("Sortable", value)

        member this.IsDefaultSort with set(value: Nullable<SortDirection>) =
            builder.AddAttribute("IsDefaultSort", value)

        member this.PlaceholderTemplate with set(value: RenderFragment<PlaceholderContext>) =
            builder.AddAttribute("PlaceholderTemplate", value)

        interface IComponentImport with
            member _.Builder = builder

    type PropertyColumn<'TGridItem, 'TProp> with
        static member inline Render(builder: BlazorBuilderCore, property: Expression<Func<'TGridItem, 'TProp>>) =
            builder.OpenComponent<PropertyColumn<'TGridItem, 'TProp>>()
            builder.AddAttribute("Property", property)
            PropertyColumn__2__Import<'TGridItem, 'TProp>(builder)

[<AutoOpen>]
module TemplateColumn__1__Import =
    open Microsoft.AspNetCore.Components.QuickGrid
    open Microsoft.AspNetCore.Components
    open System
    open Microsoft.AspNetCore.Components.Web.Virtualization

    type [<Struct; IsReadOnly>] TemplateColumn__1__Import<'TGridItem>(builder: BlazorBuilderCore) =

        member this.ChildContent with set(value: RenderFragment<'TGridItem>) =
            builder.AddAttribute("ChildContent", value)

        member this.SortBy with set(value: GridSort<'TGridItem>) =
            builder.AddAttribute("SortBy", value)

        member this.Title with set(value: String) =
            builder.AddAttribute("Title", value)

        member this.Class with set(value: String) =
            builder.AddAttribute("Class", value)

        member this.Align with set(value: Align) =
            builder.AddAttribute("Align", value)

        member this.HeaderTemplate with set(value: RenderFragment<ColumnBase<'TGridItem>>) =
            builder.AddAttribute("HeaderTemplate", value)

        member this.ColumnOptions with set(value: RenderFragment) =
            builder.AddAttribute("ColumnOptions", value)

        member this.Sortable with set(value: Nullable<Boolean>) =
            builder.AddAttribute("Sortable", value)

        member this.IsDefaultSort with set(value: Nullable<SortDirection>) =
            builder.AddAttribute("IsDefaultSort", value)

        member this.PlaceholderTemplate with set(value: RenderFragment<PlaceholderContext>) =
            builder.AddAttribute("PlaceholderTemplate", value)

        interface IComponentImport with
            member _.Builder = builder

    type TemplateColumn<'TGridItem> with
        static member inline Render(builder: BlazorBuilderCore) =
            builder.OpenComponent<TemplateColumn<'TGridItem>>()
            TemplateColumn__1__Import<'TGridItem>(builder)

[<AutoOpen>]
module Paginator__Import =
    open Microsoft.AspNetCore.Components.QuickGrid
    open Microsoft.AspNetCore.Components

    type [<Struct; IsReadOnly>] Paginator__Import(builder: BlazorBuilderCore) =

        member this.SummaryTemplate with set(value: RenderFragment) =
            builder.AddAttribute("SummaryTemplate", value)

        interface IComponentImport with
            member _.Builder = builder

    type Paginator with
        static member inline Render(builder: BlazorBuilderCore, value: PaginationState) =
            builder.OpenComponent<Paginator>()
            builder.AddAttribute("Value", value)
            Paginator__Import(builder)

[<AutoOpen>]
module QuickGrid__1__Import =
    open Microsoft.AspNetCore.Components.QuickGrid
    open System.Linq
    open System
    open Microsoft.AspNetCore.Components

    type [<Struct; IsReadOnly>] QuickGrid__1__Import<'TGridItem>(builder: BlazorBuilderCore) =

        member this.Items with set(value: IQueryable<'TGridItem>) =
            builder.AddAttribute("Items", value)

        member this.ItemsProvider with set(value: GridItemsProvider<'TGridItem>) =
            builder.AddAttribute("ItemsProvider", value)

        member this.Class with set(value: String) =
            builder.AddAttribute("Class", value)

        member this.Theme with set(value: String) =
            builder.AddAttribute("Theme", value)

        member this.ChildContent with set(value: RenderFragment) =
            builder.AddAttribute("ChildContent", value)

        member this.Virtualize with set(value: Boolean) =
            builder.AddAttribute("Virtualize", value)

        member this.ItemSize with set(value: Single) =
            builder.AddAttribute("ItemSize", value)

        member this.ResizableColumns with set(value: Boolean) =
            builder.AddAttribute("ResizableColumns", value)

        member this.ItemKey with set(value: Func<'TGridItem, Object>) =
            builder.AddAttribute("ItemKey", value)

        member this.Pagination with set(value: PaginationState) =
            builder.AddAttribute("Pagination", value)

        interface IComponentImport with
            member _.Builder = builder

    type QuickGrid<'TGridItem> with
        static member inline Render(builder: BlazorBuilderCore) =
            builder.OpenComponent<QuickGrid<'TGridItem>>()
            QuickGrid__1__Import<'TGridItem>(builder)

[<AutoOpen>]
module _Imports__Import =
    open Microsoft.AspNetCore.Components.QuickGrid

    type [<Struct; IsReadOnly>] _Imports__Import(builder: BlazorBuilderCore) =

        interface IComponentImport with
            member _.Builder = builder

    type _Imports with
        static member inline Render(builder: BlazorBuilderCore) =
            builder.OpenComponent<_Imports>()
            _Imports__Import(builder)