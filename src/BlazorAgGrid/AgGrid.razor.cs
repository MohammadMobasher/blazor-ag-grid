﻿using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System.Text.Json;

namespace BlazorAgGrid
{
    public partial class AgGrid : IDisposable
    {
        private static readonly JsonSerializerOptions AgGridJsonSerOptions = new JsonSerializerOptions
        {
            DefaultIgnoreCondition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingNull,
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
        };

#pragma warning disable CS0649 // Field 'field' is never assigned to, and will always have its default value 'value'
        // These are referenced by nested components as cascading parameters
        private List<ColumnDefinition> _columnDefinitions = new List<ColumnDefinition>();
        //private RowData _rowData = new RowData();
        private List<object> _rowData = new List<object>();
        // This will be populated by the component @ref
        private ElementReference _gridDiv;
#pragma warning restore CS0649

        // A unique ID is assigned to the Grid for Grid API references
        private string _id = Guid.NewGuid().ToString();
        private bool _isRendered = false;

        [Parameter] public RenderFragment ChildContent { get; set; }
        [Parameter] public string HeightStyle { get; set; } = "500px";
        [Parameter] public string WidthStyle { get; set; } = "500px";
        [Parameter] public GridOptions Options { get; set; }
        [Parameter] public GridEvents Events { get; set; }
        [Parameter] public GridCallbacks Callbacks { get; set; }
        [Parameter] public string ConfigureScript { get; set; }
        [Parameter] public bool IsSizeColumnsToFit { get; set; } = false;
        [Parameter] public string CssClassForHeader { get; set; } = string.Empty;

        [Inject] private IJSRuntime JS { get; set; }

        public GridApi Api { get; private set; }
        public GridColumnApi ColumnApi { get; private set; }

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (!_isRendered)
            {
                _isRendered = true;
                await CreateGrid();
                Api = new GridApi(JS, _id);
                if(IsSizeColumnsToFit)
                    await Api.SizeColumnsToFit();
                ColumnApi = new GridColumnApi(JS, _id);
            }
        }

        private async Task CreateGrid()
        {
            if (Options == null)
                Options = new GridOptions();

            if (_columnDefinitions?.Count > 0)
            {
                if(!string.IsNullOrEmpty(CssClassForHeader))
                    _columnDefinitions.Where(x=> x.HeaderClass == string.Empty).ToList().ForEach(x => x.HeaderClass = CssClassForHeader);
                Options.InternalColumnDefinitions = _columnDefinitions;
            }

            if (_rowData?.Count > 0)
                Options.InternalRowData = _rowData;

            // Execute any needed adjustments or tranformations
            // before handing off the options for JS interop
            Options.FirePrepareForInterop(JS);

            // ag-Grid treats null values differently from undefined values in its
            // inputs which means we can't just set (or leave) values as null to
            // _not provide a value_ for them.  AND, unfortunately, until
            // [https://github.com/dotnet/aspnetcore/issues/12685] or
            // [https://github.com/dotnet/corefx/issues/39735] is addressed
            // we can't control the JSRuntime serializaton to "ignore null values"
            // so we have to wrap the options in a class that declares a custom
            // converter which will serialize its constituent components using
            // customized serialization options
            var interopOptions = new InteropGridOptions
            {
                CallbackId = _id,
                Options = Options,
                Callbacks = Callbacks,
                Events = Events,
            };

            await JS.InvokeVoidAsync("blazor_ag_grid.createGrid", _gridDiv,
                interopOptions, ConfigureScript);
        }

        public async void Dispose()
        {
            try
            {
                await JS.InvokeVoidAsync("blazor_ag_grid.destroyGrid", _gridDiv, _id);
            }
            catch (Exception ex)
            {
                Console.WriteLine("WARNING: failure during clean up: " + ex);
            }
        }
    }
}
