using System.ComponentModel;

namespace UserAPI.Domain
{
    public class Employee
    {
        [DefaultValue("name")]
        public string Name { get; set; }

        [DefaultValue("+79000000000")]
        public string PhoneNumber { get; set; }
        public Identity Identity { get; set; }
    }
}
