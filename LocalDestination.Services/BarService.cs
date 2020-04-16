using LocalDestination.Data;
using LocalDestination.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LocalDestination.Services
{
    public class BarService
    {
        private readonly Guid _userId;

        public BarService(Guid userId)
        {
            _userId = userId;
        }

        public IEnumerable<BarListItem> GetBars()
        {
            using (var ctx = new ApplicationDbContext())
            {
                var barQuery =
                    ctx
                        .Bars
                        .Select(
                            e => new BarListItem
                            {
                                BarId = e.BarId,
                                Name = e.Name,
                                CreatedUtc = e.CreatedUtc,
                                DestinationId = e.DestinationId,
                                DestinationName = e.Destination.Name,
                                IsUserOwned = e.OwnerId == _userId
                            });

                return barQuery.ToArray();
            }
        }

        public bool CreateBar(BarCreate model)
        {
            var entity =
                new Bar
                {
                    OwnerId = _userId,
                    Name = model.Name,
                    Address = model.Address,
                    Comment = model.Comment,
                    ServesFood = model.ServesFood,
                    DestinationId = model.DestinationId,
                    CreatedUtc = DateTimeOffset.Now
                };

            using (var ctx = new ApplicationDbContext())
            {
                ctx.Bars.Add(entity);
                return ctx.SaveChanges() == 1;
            }
        }

        public BarDetail GetBarById(int id)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                        .Bars
                        .SingleOrDefault(e => e.BarId == id);

                return
                    new BarDetail
                    {
                        BarId = entity.BarId,
                        Name = entity.Name,
                        Address = entity.Address,
                        Comment = entity.Comment,
                        ServesFood = entity.ServesFood,
                        CreatedUtc = entity.CreatedUtc,
                        ModifiedUtc = entity.ModifiedUtc,
                        DestinationId = entity.DestinationId,
                        DestinationName = entity.Destination.Name,
                        IsUserOwned = entity.OwnerId == _userId
                    };
            }
        }

        public bool UpdateBar(BarEdit model)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                        .Bars
                        .SingleOrDefault(e => e.BarId == model.BarId && e.OwnerId == _userId);

                entity.Name = model.Name;
                entity.Comment = model.Comment;
                entity.Address = model.Address;
                entity.ServesFood = model.ServesFood;
                entity.ModifiedUtc = DateTimeOffset.Now;

                return ctx.SaveChanges() == 1;
            }
        }

        public bool DeleteBar(int barId)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                        .Bars
                        .SingleOrDefault(e => e.BarId == barId && e.OwnerId == _userId);

                ctx.Bars.Remove(entity);

                return ctx.SaveChanges() == 1;
            }
        }
    }
}
