/*
*@author Ramadan Ismael
*/
namespace UserManagementSystemBack.src.Models
{
    /// <summary>
    /// method to return all datas
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class ResponseModel<T>
    {
        /// <summary>
        /// All datas organize here
        /// </summary>
        /// <value></value>
        public T? Datas { get; set; }
        /// <summary>
        /// Message after request
        /// </summary>
        /// <value></value>
        public string? Message { get; set; }
        /// <summary>
        /// it's true or false
        /// </summary>
        /// <value></value>
        public bool Status { get; set; }
    }
}