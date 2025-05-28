using AutoMapper;
using BaseLibrary.DTOs;
using BaseLibrary.Entities;
using ServerLibrary.Repositories.Implementations;
using Server.Services.Interfaces;
using ServerLibrary.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.Services.Implementations
{
    public class EventService : IEventService
    {
        private readonly IEventRepository _eventRepository;
        private readonly IUnitOfWork _unitOfWork;
        public EventService(IEventRepository eventRepository, IUnitOfWork unitOfWork)
        {
            _eventRepository = eventRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<List<EventLibrary>> GetAllEvents()
        {
            var events = await _eventRepository.GetAllAsync();

            return events?.ToList();
        }

        public async Task<EventLibrary> GetEventById(int id)
        {
            var eventEntity = await _eventRepository.GetByIdAsync(id);

            if (eventEntity == null)
            {
                return null;
            }
            return eventEntity;
        }

        public async Task<EventLibrary> CreateEvent(EventLibrary eventToAdd)
        {
            if (eventToAdd != null)
            {
                _eventRepository.Add(eventToAdd);
                await _unitOfWork.SaveChangesAsync();

                return eventToAdd;
            }
            return null;
        }

        public async Task<EventLibrary> UpdateEvent(int id, EventLibrary eventToUpdate)
        {
            var eventEntity = await _eventRepository.GetByIdAsync(id);

            if (eventEntity != null)
            {
                eventEntity.Title = eventToUpdate.Title;
                eventEntity.Description = eventToUpdate.Description;
                eventEntity.EventDate = eventToUpdate.EventDate;
                eventEntity.OrganizerId = eventToUpdate.OrganizerId;
                await _unitOfWork.SaveChangesAsync();

                return eventEntity;
            }
            return null;
        }

        public async Task<string> DeleteEvent(int id)
        {
            var eventEntity = await _eventRepository.GetByIdAsync(id);

            if (eventEntity != null)
            {
                _eventRepository.Remove(eventEntity);
                await _unitOfWork.SaveChangesAsync();
                return "Event has been deleted";
            }
            return null;
        }
    }
}
