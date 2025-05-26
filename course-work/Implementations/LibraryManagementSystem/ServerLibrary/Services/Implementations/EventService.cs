using AutoMapper;
using BaseLibrary.DTOs;
using BaseLibrary.Entities;
using ServerLibrary.Repositories.Interfaces;
using ServerLibrary.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServerLibrary.Services.Implementations
{
    public class EventService(IRepository<Event> repository, IMapper mapper, IUnitOfWork unitOfWork) : ServicesBase<Event, EventDTO>(repository, mapper, unitOfWork), IEventService
    {
    }
}
