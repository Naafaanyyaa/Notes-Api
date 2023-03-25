﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using FirstLab.Business.Interfaces;
using FirstLab.Business.Models.Request;
using FirstLab.Data.Interfaces;
using FirstLab.Data.Models;

namespace FirstLab.Business.Services
{
    public class NoteService : INoteService
    {
        private readonly INoteRepository _repository;
        private readonly IMapper _mapper;

        public NoteService(INoteRepository noteRepository, IMapper mapper)
        {
            _repository = noteRepository;
            _mapper = mapper;
        }

        public async Task<Note?> GetNoteByIdAsync(string id)
        {
            var note = await _repository.GetByIdAsync(id);
            return note;
        }

        public async Task<Note> AddNoteAsync(NoteRequest note, string userId)
        {
            var noteResult = _mapper.Map<NoteRequest, Note>(note);

            noteResult.UserId = Guid.Parse(userId);

            noteResult.CreatedDate = DateTime.Now;

            var result = await _repository.AddAsync(noteResult);

            return result;
        }

        public async Task<List<Note>> GetListOfNotesByUserIdAsync(string userId)
        {
            var result = await _repository.GetNotesByUserId(userId);

            return result;
        }

        public async Task DeleteNoteByIdAsync(string noteId)
        {
            await _repository.DeleteByIdAsync(noteId);
        }

        public async Task<Note> EditNoteAsync(NoteEditRequest noteEditRequest, string noteId)
        {
            var note = _mapper.Map<NoteEditRequest, Note>(noteEditRequest);
            note.LastModifiedDate = DateTime.Now;
            note.Id = noteId;
            await _repository.UpdateAsync(note);

            var result = await _repository.GetByIdAsync(noteId);
            return result;
        }

        public async Task<List<Note>> GetListOfNotesByUserRequest(string userId, string request)
        {
            var list = await _repository.GetByRequestAsync(userId, request);
            return list;
        }
    }
}