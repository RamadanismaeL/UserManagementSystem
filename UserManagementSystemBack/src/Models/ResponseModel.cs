/*
*@author Ramadan Ismael
*/
namespace UserManagementSystemBack.src.Models
{
    public class ResponseModel<T>
    {
        public T? Datas { get; set; }
        public string? Message { get; set; }
        public bool Status { get; set; }
    }
}