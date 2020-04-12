using LocalDestination.Data;
using LocalDestination.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LocalDestination.Services
{
    public class HotelService
    {
        private readonly Guid _userId;

        public HotelService(Guid userId)
        {
            _userId = userId;
        }

        public IEnumerable<HotelListItem> GetHotels()
        {
            using (var ctx = new ApplicationDbContext())
            {
                var hotelQuery =
                    ctx
                        .Hotels
                        .Where(e => e.OwnerId == _userId)
                        .Select(
                            e => new HotelListItem
                            {
                                HotelId = e.HotelId,
                                Name = e.Name,
                                CreatedUtc = e.CreatedUtc,
                                DestinationId = e.DestinationId,
                                DestinationName = e.Destination.Name
                            });

                return hotelQuery.ToArray();
            }
        }

        public bool CreateHotel(HotelCreate model)
        {
            var entity =
                new Hotel
                {
                    OwnerId = _userId,
                    Name = model.Name,
                    Address = model.Address,
                    Comment = model.Comment,
                    DestinationId = model.DestinationId,
                    CreatedUtc = DateTimeOffset.Now
                };

            using (var ctx = new ApplicationDbContext())
            {
                ctx.Hotels.Add(entity);
                return ctx.SaveChanges() == 1;
            }
        }

        public HotelDetail GetHotelById(int id)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                        .Hotels
                        .SingleOrDefault(e => e.HotelId == id && e.OwnerId == _userId);

                return
                    new HotelDetail
                    {
                        HotelId = entity.HotelId,
                        Name = entity.Name,
                        Address = entity.Address,
                        Comment = entity.Comment,
                        CreatedUtc = entity.CreatedUtc,
                        ModifiedUtc = entity.ModifiedUtc,
                        DestinationId = entity.DestinationId,
                        DestinationName = entity.Destination.Name
                    };
            }
        }

        public bool UpdateHotel(HotelEdit model)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                        .Hotels
                        .SingleOrDefault(e => e.HotelId == model.HotelId && e.OwnerId == _userId);

                entity.Name = model.Name;
                entity.Comment = model.Comment;
                entity.Address = model.Address;
                entity.ModifiedUtc = DateTimeOffset.Now;

                return ctx.SaveChanges() == 1;
            }
        }

        public bool DeleteHotel(int hotelId)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                        .Hotels
                        .SingleOrDefault(e => e.HotelId == hotelId && e.OwnerId == _userId);

                ctx.Hotels.Remove(entity);

                return ctx.SaveChanges() == 1;
            }
        }
    }
}
