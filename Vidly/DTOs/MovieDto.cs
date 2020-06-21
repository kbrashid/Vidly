﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Vidly.DTOs
{
    public class MovieDto
    {
       
        public int Id { get; set; }

        [Required]
        [StringLength(255)]
        public string Name { get; set; }

        //public Genre Genre { get; set; }

        [Required]        
        public byte GenreId { get; set; }

        public DateTime DateAdded { get; set; }

        [Required]
        
        public DateTime? ReleaseDate { get; set; }

        [Required]       
        [Range(1, 20)]
        public byte? NumberInStock { get; set; }

    }
}