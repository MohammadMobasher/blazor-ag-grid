using System.Text.Json.Serialization;

namespace BlazorAgGrid
{
    /// <summary>
    /// Strongly-typed representation of:
    ///   https://www.ag-grid.com/javascript-grid-column-properties/
    /// </summary>
    public partial class ColumnDefinition
    {
        public string Field { get; set; }

        public string HeaderName { get; set; }

        [JsonPropertyName("resizable")]
        public bool IsResizable { get; set; }

        [JsonPropertyName("sortable")]
        public bool IsSortable { get; set; }

        [JsonPropertyName("filter")]
        public bool IsFiltered { get; set; }

        [JsonPropertyName("editable")]
        public bool IsEditable { get; set; }

        [JsonPropertyName("floatingFilter")]
        public bool IsFloatingFilter { get; set; }

        [JsonPropertyName("width")]
        public string Width { get; set; } = string.Empty;

        [JsonPropertyName("maxWidth")]
        public string MaxWidth { get; set; } = string.Empty;

        [JsonPropertyName("headerClass")]
        public string HeaderClass { get; set; } = string.Empty;
    }
}
