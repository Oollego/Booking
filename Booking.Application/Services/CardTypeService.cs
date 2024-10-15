using Booking.Application.Resources;
using Booking.Domain.Dto.CardType;
using Booking.Domain.Dto.Topic;
using Booking.Domain.Dto.UserTopicDto;
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
    public class CardTypeService : ICardTypeService
    {
        private readonly ILogger _logger = null!;
        private readonly IBaseRepository<CardType> _cardTypeRepository = null!;

        public CardTypeService(ILogger logger, IBaseRepository<CardType> cardTypeRepository) 
        { 
            _logger = logger;
            _cardTypeRepository = cardTypeRepository;
        }
        
        public async Task<BaseResult<CardTypeDto>> CreatCardTypeAsync(CreateCardTypeDto dto)
        {
            if(dto.CardName == null || dto.CardName.Length <= 0)
            {
                _logger.Warning(ErrorMessage.InvalidParameters);
                return new BaseResult<CardTypeDto>
                {
                    ErrorCode = (int)ErrorCodes.InvalidParameters,
                    ErrorMessage = ErrorMessage.InvalidParameters
                };
            }

            var cardType = await _cardTypeRepository.GetAll().Where(ct => ct.CardName == dto.CardName).FirstOrDefaultAsync();

            if (cardType != null)
            {
                _logger.Warning(ErrorMessage.CardTypeAlreadyExists);
                return new BaseResult<CardTypeDto>
                {
                    ErrorCode = (int)ErrorCodes.CardTypeAlreadyExists,
                    ErrorMessage = ErrorMessage.CardTypeAlreadyExists
                };
            }

            var newCardType = new CardType()
            {
                CardName = dto.CardName
            };

            newCardType = await _cardTypeRepository.CreateAsync(newCardType);
            await _cardTypeRepository.SaveChangesAsync();

            return new BaseResult<CardTypeDto>()
            {
                Data = new CardTypeDto
                {
                    Id = newCardType.Id,
                    CardName = newCardType.CardName
                }
            };
        }

        public async Task<BaseResult<CardTypeDto>> DeleteCardTypeAsync(long id)
        {
            if (id <= 0)
            {
                _logger.Warning(ErrorMessage.InvalidParameters);
                return new BaseResult<CardTypeDto>
                {
                    ErrorCode = (int)ErrorCodes.InvalidParameters,
                    ErrorMessage = ErrorMessage.InvalidParameters
                };
            }

            var cardType = await _cardTypeRepository.GetAll().Where(ct => ct.Id == id).FirstOrDefaultAsync();

            if (cardType == null)
            {
                _logger.Warning(ErrorMessage.CardTypeNotFound);
                return new BaseResult<CardTypeDto>
                {
                    ErrorCode = (int)ErrorCodes.CardTypeNotFound,
                    ErrorMessage = ErrorMessage.CardTypeNotFound
                };
            }

            cardType = _cardTypeRepository.Remove(cardType);
            await _cardTypeRepository.SaveChangesAsync();

            return new BaseResult<CardTypeDto>()
            {
                Data = new CardTypeDto
                {
                    Id = cardType.Id,
                    CardName = cardType.CardName
                }
            };
        }

        public async Task<CollectionResult<CardTypeDto>> GetAllCardTypesAsync()
        {
            var cardTypes = await _cardTypeRepository.GetAll()
                .Select(ct => new CardTypeDto()
                    {
                        Id = ct.Id,
                        CardName = ct.CardName
                    })
                .ToListAsync();

            if (cardTypes == null || cardTypes.Count == 0 )
            {
                _logger.Warning(ErrorMessage.CardTypeNotFound);
                return new CollectionResult<CardTypeDto>()
                {
                    ErrorMessage = ErrorMessage.CardTypeNotFound,
                    ErrorCode = (int)ErrorCodes.CardTypeNotFound
                };
            }

            return new CollectionResult<CardTypeDto>
            {
                Data = cardTypes
            };
        }

        public async Task<BaseResult<CardTypeDto>> GetCardTypeByIdAsync(long id)
        {
            if (id <= 0)
            {
                _logger.Warning(ErrorMessage.InvalidParameters);
                return new BaseResult<CardTypeDto>
                {
                    ErrorCode = (int)ErrorCodes.InvalidParameters,
                    ErrorMessage = ErrorMessage.InvalidParameters
                };
            }

            var cardType = await _cardTypeRepository.GetAll()
                .Where(x =>  x.Id == id)
                .Select( x => new CardTypeDto
                {
                    Id = x.Id,
                    CardName = x.CardName,
                }).FirstOrDefaultAsync();

            if (cardType == null)
            {
                _logger.Warning(ErrorMessage.CardTypeNotFound);
                return new BaseResult<CardTypeDto>
                {
                    ErrorCode = (int)ErrorCodes.CardTypeNotFound,
                    ErrorMessage = ErrorMessage.CardTypeNotFound
                };
            }

            return new BaseResult<CardTypeDto>
            {
                Data = cardType
            };

        }

        public async Task<BaseResult<CardTypeDto>> UpdateCardTypeAsync(CardTypeDto dto)
        {
            if (dto.CardName == null || dto.CardName.Length <= 0 || dto.Id <= 0)
            {
                _logger.Warning(ErrorMessage.InvalidParameters);
                return new BaseResult<CardTypeDto>
                {
                    ErrorCode = (int)ErrorCodes.InvalidParameters,
                    ErrorMessage = ErrorMessage.InvalidParameters
                };
            }

            var cardType = await _cardTypeRepository.GetAll().Where(x => x.Id == dto.Id).FirstOrDefaultAsync();

            if (cardType == null)
            {
                _logger.Warning(ErrorMessage.CardTypeNotFound);
                return new BaseResult<CardTypeDto>
                {
                    ErrorCode = (int)ErrorCodes.CardTypeNotFound,
                    ErrorMessage = ErrorMessage.CardTypeNotFound
                };
            }

            cardType.CardName = dto.CardName;

            cardType = _cardTypeRepository.Update(cardType);
            await _cardTypeRepository.SaveChangesAsync();

            return new BaseResult<CardTypeDto>()
            {
                Data = new CardTypeDto
                {
                    Id = cardType.Id,
                    CardName = cardType.CardName
                }
            };
        }
    }
}
