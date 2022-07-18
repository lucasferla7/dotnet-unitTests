using Blockbuster.API.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.ChangeTracking.Internal;
using Microsoft.EntityFrameworkCore.Metadata;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blockbuster.API.Tests.Utils
{
    public class EntityFrameworkUtils
    {
        public static EntityEntry<T> BuildEntityEntry<T>(T entity)
            where T : Entity
        {
            var internalEntityEntry = new InternalEntityEntry(
                new Mock<IStateManager>().Object,
                new RuntimeEntityType(
                    "Movie",
                    typeof(Movie),
                    false,
                    null,
                    null,
                    null,
                    ChangeTrackingStrategy.Snapshot,
                    null,
                    false),
                entity);

            var entityEntry = new EntityEntry<T>(internalEntityEntry);

            return entityEntry;
        }
    }
}
