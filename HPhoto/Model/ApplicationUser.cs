﻿namespace HPhoto.Model
{
    public class ApplicationUser
    {
        public int Id { get; set; }

        public string UserName { get; set; } = string.Empty;

        public string Email { get; set; } = string.Empty;

        public byte[] PasswordHash { get; set; } = new byte[32];


        public byte[] PasswordSalt { get; set; } = new byte[32];


        public string? VerificationToken { get; set; }

        public DateTime? VerifiedAt { get; set; }


        public string? PasswordResetToken { get; set; }


        public DateTime? ResetTokenExpires { get; set; }


        public int PhoneNum { get; set; }

        public string FavoriteTopic { get; set; } = string.Empty;
        public string ProfileImg { get; set; } = string.Empty;

    }
}
