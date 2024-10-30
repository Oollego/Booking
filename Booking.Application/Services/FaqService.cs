using Booking.Application.Resources;
using Booking.Domain.Dto.Faq;
using Booking.Domain.Dto.User;
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
    public class FaqService : IFaqService
    {
        private readonly ILogger _logger;
        private readonly IBaseRepository<Faq> _faqRepository;

        public FaqService(ILogger logger, IBaseRepository<Faq> faqRepository)
        {
            _logger = logger;
            _faqRepository = faqRepository;
        }

        public async Task<BaseResult<long>> CreateFaq(CreateFaqDto dto)
        {
            if(dto.HotelId < 0 || dto.Question == null || dto.Question.Length < 0)
            {
                _logger.Warning(ErrorMessage.InvalidParameters);
                return new BaseResult<long>()
                {
                    ErrorMessage = ErrorMessage.InvalidParameters,
                    ErrorCode = (int)ErrorCodes.InvalidParameters,

                };
            }

            var faq = _faqRepository.GetAll().AsNoTracking()
                .Where(f => f.Question == dto.Question && f.HotelId == dto.HotelId)
                .FirstOrDefault();

            if(faq != null)
            {
                _logger.Warning(ErrorMessage.FaqAlreadyExists);
                return new BaseResult<long>()
                {
                    ErrorMessage = ErrorMessage.FaqAlreadyExists,
                    ErrorCode = (int)ErrorCodes.FaqAlreadyExists
                };
            }

            var newFaq = new Faq()
            {
                HotelId = dto.HotelId,
                Question = dto.Question,
                Answer = dto.Answer,
            };

            newFaq = await _faqRepository.CreateAsync(newFaq);
            await _faqRepository.SaveChangesAsync();

            return new BaseResult<long>
            {
                Data = newFaq.Id
            };
        }

        public async Task<BaseResult> DeleteFaq(long id)
        {
            if (id < 0 )
            {
                _logger.Warning(ErrorMessage.InvalidParameters);
                return new BaseResult()
                {
                    ErrorMessage = ErrorMessage.InvalidParameters,
                    ErrorCode = (int)ErrorCodes.InvalidParameters,

                };
            }

            var faq = _faqRepository.GetAll().AsNoTracking()
                .Where(f => f.Id == id)
                .FirstOrDefault();

            if (faq == null)
            {
                _logger.Warning(ErrorMessage.FaqNotFound);
                return new BaseResult<long>()
                {
                    ErrorMessage = ErrorMessage.FaqNotFound,
                    ErrorCode = (int)ErrorCodes.FaqNotFound
                };
            }

            _faqRepository.Remove(faq);
            await _faqRepository.SaveChangesAsync();

            return new BaseResult();
        }

        public async Task<CollectionResult<FaqDto>> GetAllHotelFaqs(long hotelId)
        {
            if (hotelId < 0)
            {
                _logger.Warning(ErrorMessage.InvalidParameters);
                return new CollectionResult<FaqDto>()
                {
                    ErrorMessage = ErrorMessage.InvalidParameters,
                    ErrorCode = (int)ErrorCodes.InvalidParameters,

                };
            }

            var faqs = await _faqRepository.GetAll().AsNoTracking()
                .Where(f => f.HotelId == hotelId)
                .Select(f => new FaqDto
                {
                    FaqId = f.Id,
                    Question = f.Question,
                    Answer = f.Answer
                })
                .ToListAsync();

            if (faqs.Count == 0)
            {
                _logger.Warning(ErrorMessage.FaqNotFound);
                return new CollectionResult<FaqDto>()
                {
                    ErrorMessage = ErrorMessage.FaqNotFound,
                    ErrorCode = (int)ErrorCodes.FaqNotFound
                };
            }

            return new CollectionResult<FaqDto>()
            {
                Data = faqs
            };
        }

        public async Task<BaseResult<FaqFullDto>> GetFaq(long id)
        {
            if (id < 0)
            {
                _logger.Warning(ErrorMessage.InvalidParameters);
                return new BaseResult<FaqFullDto>()
                {
                    ErrorMessage = ErrorMessage.InvalidParameters,
                    ErrorCode = (int)ErrorCodes.InvalidParameters,

                };
            }

            var faq = await _faqRepository.GetAll().AsNoTracking()
                .Where(f => f.Id == id)
                .Select(f => new FaqFullDto
                {
                    Id = f.Id,
                    Question = f.Question,
                    Answer = f.Answer,
                    HotelId = f.HotelId
                })
                .FirstOrDefaultAsync();

            if (faq == null)
            {
                _logger.Warning(ErrorMessage.FaqNotFound);
                return new BaseResult<FaqFullDto>()
                {
                    ErrorMessage = ErrorMessage.FaqNotFound,
                    ErrorCode = (int)ErrorCodes.FaqNotFound
                };
            }

            return new BaseResult<FaqFullDto>()
            {
                Data = faq
            };
        }

        public async Task<BaseResult<FaqFullDto>> UpdateFaq(FaqFullDto dto)
        {
            if (dto.HotelId < 0 || dto.Question == null || dto.Question.Length < 0 ||
                dto.Id < 0)
            {
                _logger.Warning(ErrorMessage.InvalidParameters);
                return new BaseResult<FaqFullDto>()
                {
                    ErrorMessage = ErrorMessage.InvalidParameters,
                    ErrorCode = (int)ErrorCodes.InvalidParameters,

                };
            }

            var faq = _faqRepository.GetAll().AsNoTracking()
                .Where(f => f.Id == dto.Id)
                .FirstOrDefault();

            if (faq == null)
            {
                _logger.Warning(ErrorMessage.FaqNotFound);
                return new BaseResult<FaqFullDto>()
                {
                    ErrorMessage = ErrorMessage.FaqNotFound,
                    ErrorCode = (int)ErrorCodes.FaqNotFound  
                };
            }

            faq.Question = dto.Question;
            faq.Answer = dto.Answer;
            faq.HotelId = dto.HotelId;

            faq = _faqRepository.Update(faq);
            await _faqRepository.SaveChangesAsync();

            return new BaseResult<FaqFullDto>()
            {
                Data = new FaqFullDto
                {
                    Id = faq.Id,
                    Question = faq.Question,
                    Answer = faq.Answer,
                    HotelId = faq.HotelId
                }
            };
        }
    }
}
