﻿using Microsoft.AspNetCore.Components;

namespace BlazorAgGrid
{
    public partial class GridColumn : ComponentBase
    {
        [CascadingParameter(Name = nameof(GridOptions.ColumnDefinitions))]
        public List<ColumnDefinition> ColumnDefinitions { get; set; }
        
        [Parameter] public string Field { get; set; }
        [Parameter] public string Header { get; set; }
        [Parameter] public bool IsResizable { get; set; }
        [Parameter] public bool IsSortable { get; set; }
        [Parameter] public bool IsFiltered { get; set; }
        [Parameter] public bool IsEditable { get; set; }
        [Parameter] public bool IsFloatingFilter { get; set; }

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
            }); ;
        }
    }
}
