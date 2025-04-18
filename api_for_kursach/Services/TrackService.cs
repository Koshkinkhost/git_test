﻿using api_for_kursach.DTO;
using api_for_kursach.Models;
using api_for_kursach.Repositories;
using Microsoft.EntityFrameworkCore;

namespace api_for_kursach.Services
{
    public interface ITrackService
    {
        Task<IEnumerable<TrackSimpleDTO>> GetAllTracksAsync();
        Task<IEnumerable<TrackSimpleDTO>> GetTracksByAlbumIdAsync(AlbumDTO album);
        Task IncrementPlayCountAsync(int trackId);
        Task<IEnumerable<TrackSimpleDTO>> SearchTracksByTitleAsync(string title);
        Task<IEnumerable<TrackSimpleDTO>> GetTopTracksAsync(int topN);
    }
    public class TrackService:ITrackService
    {
        private readonly MusicLabelContext _context;
        private readonly ITrackRepository _rep_track;
       
        public TrackService(ITrackRepository rep)
        {
            _rep_track = rep;
        }

        public Task<IEnumerable<TrackSimpleDTO>> GetAllTracksAsync()
        {
            return _rep_track.GetAllTracksAsync();
        }

        public Task<IEnumerable<TrackSimpleDTO>> GetTopTracksAsync(int topN)
        {
            if(topN > 0)
            {
                return _rep_track.GetTopTracksAsync(topN);
            }
            throw new InvalidDataException("передано отрицательное значение");
        }

        public Task<IEnumerable<TrackSimpleDTO>> GetTracksByAlbumIdAsync(AlbumDTO album)
        {
            if( !String.IsNullOrEmpty(album.Name))
            {
                return _rep_track.GetTracksByAlbumIdAsync(album.Name);
            }
            throw new InvalidDataException("отрицательное число");
        }

        public async Task IncrementPlayCountAsync(int trackId)
        {
            if(trackId > 0 )
            {

               await  _rep_track.IncrementPlayCountAsync(trackId);

            }
           

        }

        public Task<IEnumerable<TrackSimpleDTO>> SearchTracksByTitleAsync(string title)
        {
            throw new NotImplementedException();
        }
    }
}
