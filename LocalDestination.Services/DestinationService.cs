using LocalDestination.Data;
using LocalDestination.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LocalDestination.Services
{
    public class DestinationService
    {
        private readonly Guid _userId;
        public DestinationService(Guid userId)
        {
            _userId = userId;
        }

        public IEnumerable<DestinationListItem> GetDestinations()
        {
            using (var ctx = new ApplicationDbContext())
            {
                var destinationQuery =
                    ctx
                        .Destinations
                        .Select(e => new DestinationListItem
                        {
                            DestinationId = e.DestinationId,
                            Name = e.Name,
                            IsUserOwned = e.CreatorId == _userId
                        });

                return destinationQuery.ToArray();
            }
        }

        public bool CreateDestination(DestinationCreate model)
        {
            var entity = new Destination
            {
                Name = model.Name,
                City = model.City,
                State = model.State,
                Country = model.Country,
                CreatorId = _userId
            };

            using (var ctx = new ApplicationDbContext())
            {
                ctx.Destinations.Add(entity);
                return ctx.SaveChanges() == 1;
            }
        }

        public DestinationDetail GetDestinationById(int destinationId)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity = ctx.Destinations.SingleOrDefault(e => e.DestinationId == destinationId);

                return
                    new DestinationDetail
                    {
                        DestinationId = entity.DestinationId,
                        Name = entity.Name,
                        City = entity.City,
                        State = entity.State,
                        Country = entity.Country,
                        IsUserOwned = entity.CreatorId == _userId
                    };
            }
        }

        public bool UpdateDestination(DestinationEdit model)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                        .Destinations
                        .SingleOrDefault(e => e.DestinationId == model.DestinationId && e.CreatorId == _userId);

                entity.DestinationId = model.DestinationId;
                entity.Name = model.Name;
                entity.City = model.City;
                entity.State = model.State;
                entity.Country = model.Country;

                return ctx.SaveChanges() == 1;
            }
        }

        public bool DeleteDestination(int destinationId)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                        .Destinations
                        .SingleOrDefault(e => e.DestinationId == destinationId && e.CreatorId == _userId);

                ctx.Destinations.Remove(entity);

                return ctx.SaveChanges() == 1;
            }
        }
    }
}
