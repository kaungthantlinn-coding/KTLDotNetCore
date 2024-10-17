namespace ToDoListApi.DataModels
{
    public class ToDoList
    {
        public int TaskID { get; set; }
        public string TaskTitle { get; set; } = string.Empty;
        public string? TaskDescription { get; set; }
        public int? CategoryID { get; set; }
        public byte PriorityLevel { get; set; }
        public string Status { get; set; } = "Pending";
        public DateTime DueDate { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public DateTime? CompletedDate { get; set; }

        
    }
}
