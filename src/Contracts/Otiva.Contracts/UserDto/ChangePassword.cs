using Otiva.Contracts.Attributs;

namespace Otiva.Contracts.UserDto
{
    public class ChangePassword
    {
        public string CurrentPassword { get; set; }

        [ValidatePassword]
        public string NewPassword { get; set; }
    }
}
