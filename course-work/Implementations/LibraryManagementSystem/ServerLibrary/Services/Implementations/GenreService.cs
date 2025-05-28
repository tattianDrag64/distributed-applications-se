using AutoMapper;
using BaseLibrary.DTOs;
using BaseLibrary.Entities;
using Server.Services.Interfaces;
using ServerLibrary.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.Services.Implementations
{
    public class GenreService(IRepository<Genre> repository, IMapper mapper, IUnitOfWork unitOfWork) : ServicesBase<Genre, GenreDTO>(repository, mapper, unitOfWork), IGenreService
    {
    }
}
