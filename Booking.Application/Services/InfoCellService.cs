using Booking.Application.Resources;
using Booking.Domain.Dto.Facility;
using Booking.Domain.Dto.Hotel;
using Booking.Domain.Dto.HotelInfoCell;
using Booking.Domain.Entity;
using Booking.Domain.Enum;
using Booking.Domain.Interfaces.Repositories;
using Booking.Domain.Interfaces.Services;
using Booking.Domain.Result;
using Microsoft.EntityFrameworkCore;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking.Application.Services
{
    public class InfoCellService: IInfoCellService
    {
        private readonly IBaseRepository<Hotel> _hotelRepository = null!;
        private readonly ILogger _logger = null!;

        public InfoCellService(IBaseRepository<Hotel> facilityRepository, ILogger logger)
        {
            _hotelRepository = facilityRepository;
            _logger = logger;
        }

        public async Task<CollectionResult<HotelInfoCellDto>> GetHotelInfoCellsAsync(long hotelId)
        {
            if (hotelId < 0)
            {
                return new CollectionResult<HotelInfoCellDto>()
                {
                    ErrorCode = (int)ErrorCodes.InvalidParameters,
                    ErrorMessage = ErrorMessage.InvalidParameters
                };
            }

            var infoCells = await _hotelRepository.GetAll()
                .Where(x => x.Id == hotelId)
                .Include(x => x.HotelInfoCells).ThenInclude(x => x.InfoIcon)
                .Select(x => x.HotelInfoCells.Select(x => new HotelInfoCellDto
                    {
                        TextLine_1 = x.TextLine_1,
                        TextLine_2 = x.TextLine_2,
                        InfoIcon = x.InfoIcon!.IconFileName
                    }).ToList()
                ).FirstOrDefaultAsync();
 

            if (infoCells == null)
            {
                _logger.Warning(ErrorMessage.InfoCellNotFound);
                return new CollectionResult<HotelInfoCellDto>()
                {
                    ErrorMessage = ErrorMessage.InfoCellNotFound,
                    ErrorCode = (int)ErrorCodes.InfoCellNotFound
                };
            }

            if (infoCells.Count == 0)
            {
                _logger.Warning(ErrorMessage.InfoCellNotFound, infoCells.Count);
                return new CollectionResult<HotelInfoCellDto>()
                {
                    ErrorMessage = ErrorMessage.InfoCellNotFound,
                    ErrorCode = (int)ErrorCodes.InfoCellNotFound
                };
            }

            return new CollectionResult<HotelInfoCellDto>()
            {
                Data = infoCells,
                Count = infoCells.Count()
            };
        }
    }
}
