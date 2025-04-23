namespace Todoapp.DTOS.Tasks
{
    public class TakeGetPaging
    { 
        public int PageIndex { get; set; }  
        public int PageSize { get; set; }   

        public string? SearchText { get; set; } 
    }
}
