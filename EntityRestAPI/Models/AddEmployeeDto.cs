namespace EntityRestAPI.Models
{
    public class AddEmployeeDto
    {
        public required string Name { get; set; }

        public required string Email { get; set; }

        public string? Phone { get; set; } // Nullable property

        public decimal Salary { get; set; }
    }
}
