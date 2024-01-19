namespace ProductStoreAsp.Models.ViewModels
{
    public class RegisterUserViewModel
    {
        public string UserName { get; set; } = null!;

        public string Email { get; set; } = null!;
        
        public string Password { get; set; } = null!;
        public string ConfirmPassword { get; set; } = null!;
    }
}
