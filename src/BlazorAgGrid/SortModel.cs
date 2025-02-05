﻿using System.Text.Json.Serialization;

namespace BlazorAgGrid
{
    public class SortModel
    {
        [JsonPropertyName("colId")]
        public string ColumnId { get; set; }

        [JsonPropertyName("sort")]
        public string Direction { get; set; }
    }
}
