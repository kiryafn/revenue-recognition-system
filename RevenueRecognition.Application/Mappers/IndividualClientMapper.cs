using RevenueRecognition.Application.DTOs;
using RevenueRecognition.Application.DTOs.Clients.Individuals;
using RevenueRecognition.Domain.Models;

namespace RevenueRecognition.Application.Mappers;

public static class IndividualClientMapper
    {
        public static IndividualClientResponseDto ToDto(IndividualClient c) =>
            new () {
                Id          = c.Id,
                FirstName   = c.FirstName,
                LastName    = c.LastName,
                Email       = c.Email,
                PhoneNumber = c.PhoneNumber,
                Pesel       = c.Pesel,
                Address     = new AddressDto {
                    Country    = c.Address.Country,
                    City       = c.Address.City,
                    Street     = c.Address.Street,
                    PostalCode = c.Address.PostalCode
                }
            };

        public static IndividualClient ToEntity(IndividualClientCreateDto dto) =>
            new () {
                FirstName   = dto.FirstName,
                LastName    = dto.LastName,
                Email       = dto.Email,
                PhoneNumber = dto.PhoneNumber,
                Pesel       = dto.Pesel,
                Address     = new ClientAddress{
                   Country = dto.Address.Country,
                   City = dto.Address.City,
                   Street = dto.Address.Street,
                   PostalCode = dto.Address.PostalCode}
            };

        public static void Map(IndividualClientUpdateDto dto, IndividualClient c)
        {
            if (dto.FirstName   is not null)   c.FirstName   = dto.FirstName;
            if (dto.LastName    is not null)   c.LastName    = dto.LastName;
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