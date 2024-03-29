﻿using System.ComponentModel.DataAnnotations;

namespace HPhoto.Model
{
    public class Tag
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; } = string.Empty;
        [Required]
        public string Description { get; set; } = string.Empty;
        [Required]
        public int Rating { get; set; }

    }
}
