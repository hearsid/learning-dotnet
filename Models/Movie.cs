using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace cinemaAPI.Models
{

    public class Movie
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public string Description { get; set; }

        public string Language { get; set; }

        public string Duration { get; set; }

        public DateTime PlayingDate { get; set; }

        public DateTime PlayingTime { get; set; }

        public double TicketPrice { get; set; }
        public double Rating { get; set; }

        [NotMapped]
        public IFormFile Image { get; set; }

        public string ImageUrl { get; set; }

        public ICollection<Reservation> Reservations { get; set; }
    }
}