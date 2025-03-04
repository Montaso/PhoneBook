namespace PhoneBook.src.Dtos
{
    public class PutContactRequest {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; } 
        public string Password { get; set; }
        public string PhoneNumber { get; set; }
        public DateOnly BirthDate { get; set; }
        public string Category { get; set; }
        public string Subcategory { get; set; }
    }

}