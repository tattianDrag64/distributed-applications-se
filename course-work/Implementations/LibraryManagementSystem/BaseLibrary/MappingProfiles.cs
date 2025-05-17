using AutoMapper;
using BaseLibrary.DTOs;
using BaseLibrary.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseLibrary
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<User, UserDTO>().ReverseMap();
            CreateMap<User, RegisterDTO>().ReverseMap();
            CreateMap<User, LoginDTO>().ReverseMap();
            CreateMap<User, UpdateDTO>().ReverseMap();

            CreateMap<Book, BookDTO>().ReverseMap();

            CreateMap<Author, AuthorDTO>().ReverseMap();

            CreateMap<Genre, GenreDTO>().ReverseMap();

            CreateMap<Review, ReviewDTO>().ReverseMap();

            CreateMap<Reservation, ReservationDTO>().ReverseMap();

            CreateMap<Penalty, PenaltyDTO>().ReverseMap();
              
            CreateMap<Event, EventDTO>().ReverseMap();

            CreateMap<User, AccountBaseDTO>().ReverseMap();
        }
    }
}
