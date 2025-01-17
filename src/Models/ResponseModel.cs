/**
** @author Ramadan Ismael
*/

namespace userManagementSystemBack.src.Models
{
    public class ResponseModel<T>
    {
        public T? Datas { get; set; }
        public string Message { get; set; } = string.Empty;
        public bool Status { get; set; } = true;
    }
}