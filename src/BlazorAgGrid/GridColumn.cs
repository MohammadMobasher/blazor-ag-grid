using Microsoft.AspNetCore.Components;
using System.Text.Json.Serialization;

namespace BlazorAgGrid
{
    public partial class GridColumn : ComponentBase
    {
        [CascadingParameter(Name = nameof(GridOptions.ColumnDefinitions))]
        public List<ColumnDefinition> ColumnDefinitions { get; set; }
        
        [Parameter] public string Field { get; set; }
        [Parameter] public string Header { get; set; }

        [Parameter] public string Width { get; set; }
        [Parameter] public string MaxWidth { get; set; }

        [Parameter] public bool IsResizable { get; set; }
        [Parameter] public bool IsSortable { get; set; }
        [Parameter] public bool IsFiltered { get; set; }
        [Parameter] public bool IsEditable { get; set; }
        [Parameter] public bool IsFloatingFilter { get; set; }
        [Parameter] public string HeaderClass { get; set; } = string.Empty;
        [Parameter] public bool SuppressMenu { get; set; } = true;

        [Parameter] public CellDataType CellDataType { get; set; }

        protected override void OnInitialized()
        {
            ColumnDefinitions.Add(new ColumnDefinition
            {
                Field = Field,
                HeaderName = Header,
                IsResizable = IsResizable,
                IsSortable = IsSortable,
                IsFiltered = IsFiltered,
                IsEditable = IsEditable,
                IsFloatingFilter = IsFloatingFilter,
                Width = Width,
                MaxWidth = MaxWidth,
                HeaderClass = HeaderClass,
                CellDataType = CellDataType,
                SuppressMenu = SuppressMenu,
            }); ;
        }
    }


    [JsonConverter(typeof(EnumConverter))]
    public enum CellDataType
    {
        Text = 0,
        Number = 1,
        Boolean = 2,
        Date = 3,
        Object = 4
    }
}
