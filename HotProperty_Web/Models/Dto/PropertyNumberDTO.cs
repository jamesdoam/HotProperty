﻿using System.ComponentModel.DataAnnotations;

namespace HotProperty_Web.Models.Dto
{
    public class PropertyNumberDTO
    {
        [Required]
        public int PropertyNo { get; set; }

        [Required]
        public int PropertyID { get; set; }
        public string SpecialDetails { get; set; }
        public PropertyDTO Property { get; set; }
    }
}
