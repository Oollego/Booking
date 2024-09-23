using Booking.Domain.Entity;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking.DAL.Configurations
{
    internal class HotelLabelConfigurationcs : IEntityTypeConfiguration<HotelLabel>
    {
        public void Configure(EntityTypeBuilder<HotelLabel> builder)
        {
            builder.ToTable("hotel_labels");
            builder.Property(x => x.HotelId).IsRequired();
            builder.Property(x => x.HotelLabelTypeId).IsRequired();

            builder.HasData(new List<HotelLabel>
            {
                new HotelLabel { HotelId = 1, HotelLabelTypeId = 1 }, 
                new HotelLabel { HotelId = 1, HotelLabelTypeId = 2 }, 
                new HotelLabel { HotelId = 1, HotelLabelTypeId = 3 },
                new HotelLabel { HotelId = 1, HotelLabelTypeId = 4 },

                new HotelLabel { HotelId = 2, HotelLabelTypeId = 1 }, 
                new HotelLabel { HotelId = 2, HotelLabelTypeId = 3 }, 
                                                 
                new HotelLabel { HotelId = 3, HotelLabelTypeId = 2 }, 
                new HotelLabel { HotelId = 3, HotelLabelTypeId = 3 }, 
                new HotelLabel { HotelId = 3, HotelLabelTypeId = 1 },
                new HotelLabel { HotelId = 3, HotelLabelTypeId = 4 },

                new HotelLabel { HotelId = 4, HotelLabelTypeId = 1 }, 
                new HotelLabel { HotelId = 4, HotelLabelTypeId = 3 }, 
                                                 
                new HotelLabel { HotelId = 5, HotelLabelTypeId = 2 }, 
                new HotelLabel { HotelId = 5, HotelLabelTypeId = 3 }, 
                new HotelLabel { HotelId = 5, HotelLabelTypeId = 1 }, 
                                                  
                new HotelLabel { HotelId = 6, HotelLabelTypeId = 1 }, 
                new HotelLabel { HotelId = 6, HotelLabelTypeId = 3 }, 
                                                  
                new HotelLabel { HotelId = 7, HotelLabelTypeId = 2 }, 
                new HotelLabel { HotelId = 7, HotelLabelTypeId = 3 }, 
                new HotelLabel { HotelId = 7, HotelLabelTypeId = 1 },
                new HotelLabel { HotelId = 7, HotelLabelTypeId = 4 },

                new HotelLabel { HotelId = 8, HotelLabelTypeId = 1 }, 
                new HotelLabel { HotelId = 8, HotelLabelTypeId = 2 }, 
                                                  
                new HotelLabel { HotelId = 9, HotelLabelTypeId = 1 }, 
                new HotelLabel { HotelId = 9, HotelLabelTypeId = 3 }, 
                                                   
                new HotelLabel { HotelId = 10, HotelLabelTypeId = 1 },
                new HotelLabel { HotelId = 10, HotelLabelTypeId = 2 },
                new HotelLabel { HotelId = 10, HotelLabelTypeId = 3 },
                                                   
                new HotelLabel { HotelId = 11, HotelLabelTypeId = 1 },
                new HotelLabel { HotelId = 11, HotelLabelTypeId = 3 },
                new HotelLabel { HotelId = 11, HotelLabelTypeId = 4 },

                new HotelLabel { HotelId = 12, HotelLabelTypeId = 2 },
                new HotelLabel { HotelId = 12, HotelLabelTypeId = 3 },
                new HotelLabel { HotelId = 12, HotelLabelTypeId = 1 },
                new HotelLabel { HotelId = 12, HotelLabelTypeId = 4 },

                new HotelLabel { HotelId = 13, HotelLabelTypeId = 1 },
                new HotelLabel { HotelId = 13, HotelLabelTypeId = 3 },
                new HotelLabel { HotelId = 13, HotelLabelTypeId = 4 },

                new HotelLabel { HotelId = 14, HotelLabelTypeId = 1 },
                new HotelLabel { HotelId = 14, HotelLabelTypeId = 3 },
                                                   
                new HotelLabel { HotelId = 15, HotelLabelTypeId = 1 },
                new HotelLabel { HotelId = 15, HotelLabelTypeId = 3 },
                new HotelLabel { HotelId = 15, HotelLabelTypeId = 2 },
            });                                       
        }
    }
}
