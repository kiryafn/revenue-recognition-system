using RevenueRecognition.Application.DTOs;
using RevenueRecognition.Application.DTOs.Companies;
using RevenueRecognition.Domain.Models;

namespace RevenueRecognition.Application.Mappers;

public static class CompanyClientMapper
{
        public static CompanyClientResponseDto ToDto(CompanyClient c) =>
            new CompanyClientResponseDto {
                Id          = c.Id,
                CompanyName = c.CompanyName,
                Email       = c.Email,
                PhoneNumber = c.PhoneNumber,
                KrsNumber   = c.KrsNumber,
                Address     = new AddressDto {
                    Country    = c.Address.Country,
                    City       = c.Address.City,
                    Street     = c.Address.Street,
                    PostalCode = c.Address.PostalCode
                }
            };
        
        public static CompanyClient ToEntity(CompanyClientCreateDto c) =>
            new CompanyClient {
                CompanyName = c.CompanyName,
                Email       = c.Email,
                PhoneNumber = c.PhoneNumber,
                KrsNumber   = c.KrsNumber,
                Address     = new ClientAddress{
                    Country    = c.Address.Country,
                    City       = c.Address.City,
                    Street     = c.Address.Street,
                    PostalCode = c.Address.PostalCode
                }
            };

        public static void Map(CompanyClientUpdateDto dto, CompanyClient c)
        {
            if (dto.CompanyName is not null)   c.CompanyName   = dto.CompanyName;
            if (dto.Email       is not null)   c.Email       = dto.Email;
            if (dto.PhoneNumber is not null)   c.PhoneNumber = dto.PhoneNumber;
            if (dto.IsDeleted   is not null)   c.IsDeleted   = dto.IsDeleted.Value;
            if (dto.Address     is not null)
            {
                c.Address = new ClientAddress
                {
                    Country = dto.Address.Country,
                    City = dto.Address.City,
                    Street = dto.Address.Street,
                    PostalCode = dto.Address.PostalCode
                };
            }
        }
    }