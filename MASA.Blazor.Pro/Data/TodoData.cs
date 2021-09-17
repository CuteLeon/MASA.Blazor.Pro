﻿namespace MASA.Blazor.Pro.Data
{
    public class TodoData
    {
        public bool IsChecked { get; set; }

        public bool IsImportant { get; set; }

        public bool IsCompleted { get; set; }

        public bool IsDeleted { get; set; }

        public string? Title { get; set; }

        public string? Assignee { get; set; }

        public int Avatar { get; set; }

        public DateTime DueDate { get; set; }

        public string? Tag { get; set; }

        public string? Description { get; set; }
    }
}
