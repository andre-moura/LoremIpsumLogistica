using LoremIpsumLogistica.Server.Domains.DTOs.Request;
using LoremIpsumLogistica.Server.Domains.DTOs.Response;
using LoremIpsumLogistica.Server.Domains.Entities;
using LoremIpsumLogistica.Server.Infraestructure.UnitOfWork;
using Microsoft.EntityFrameworkCore;

namespace LoremIpsumLogistica.Application.Services
{
    public class CustomerService
    {
        private readonly IUnitOfWork _unitOfWork;

        public CustomerService(IUnitOfWork unitOfWork) => _unitOfWork = unitOfWork;

        public async Task<IEnumerable<CustomerResponse>> GetAllCustomersAsync() =>
            (await _unitOfWork.Repository<Customer>().GetAllAsync(query => query.Include(c => c.Addresses)))
            .Select(customer => new CustomerResponse
            {
                Id = customer.Id,
                Name = customer.Name,
                DateOfBirth = customer.DateOfBirth,
                Gender = customer.Gender,
                Addresses = customer.Addresses?.Select(address => new AddressResponse 
                {
                    Id = address.Id,
                    ZipCode = address.ZipCode,
                    Street = address.Street,
                    Number = address.Number,
                    Complement = address.Complement,
                    Neighborhood = address.Neighborhood,
                    City = address.City,
                    State = address.State,
                    AddressType = address.AddressType
                }).ToList()
            }).ToList();

        public async Task<CustomerResponse> GetCustomerByIdAsync(int id)
        {
            var customer = await _unitOfWork.Repository<Customer>().GetByIdAsync(id, query => query.Include(c => c.Addresses));

            if (customer == null) return new();

            return new CustomerResponse
            {
                Id = customer.Id,
                Name = customer.Name,
                DateOfBirth = customer.DateOfBirth,
                Gender = customer.Gender,
                Addresses = customer.Addresses?.Select(address => new AddressResponse
                {
                    Id = address.Id,
                    ZipCode = address.ZipCode,
                    Street = address.Street,
                    Number = address.Number,
                    Complement = address.Complement,
                    Neighborhood = address.Neighborhood,
                    City = address.City,
                    State = address.State,
                    AddressType = address.AddressType
                }).ToList()
            };
        }

        public async Task<int> CreateCustomerAsync(CustomerRequest customerRequest)
        {
            var customer = new Customer
            {
                Name = customerRequest.Name,
                DateOfBirth = customerRequest.DateOfBirth,
                Gender = customerRequest.Gender,
                Addresses = customerRequest.Addresses?.Select(addressRequest => new Address
                {
                    ZipCode = addressRequest.ZipCode,
                    Street = addressRequest.Street,
                    Number = addressRequest.Number,
                    Complement = addressRequest.Complement,
                    Neighborhood = addressRequest.Neighborhood,
                    City = addressRequest.City,
                    State = addressRequest.State,
                    AddressType = addressRequest.AddressType
                }).ToList() ?? []
            };
            await _unitOfWork.Repository<Customer>().AddAsync(customer);
            await _unitOfWork.CommitAsync();

            return customer.Id;
        }

        public async Task UpdateCustomerAsync(int id, CustomerRequest customerRequest)
        {
            var customer = await _unitOfWork.Repository<Customer>().GetByIdAsync(id, query => query.Include(c => c.Addresses));

            if (customer != null)
            {
                customer.Name = customerRequest.Name;
                customer.DateOfBirth = customerRequest.DateOfBirth;
                customer.Gender = customerRequest.Gender;

                var existingAddresses = customer.Addresses ?? [];

                var updatedAddresses = customerRequest.Addresses?.Select(address => new Address
                {
                    Id = address.Id,
                    ZipCode = address.ZipCode,
                    Street = address.Street,
                    Number = address.Number,
                    Complement = address.Complement,
                    Neighborhood = address.Neighborhood,
                    City = address.City,
                    State = address.State,
                    AddressType = address.AddressType,
                    CustomerId = id
                }).ToList() ?? [];

                if (!updatedAddresses.Any())
                {
                    foreach (var existingAddress in existingAddresses)
                    {
                        await _unitOfWork.Repository<Address>().DeleteAsync(existingAddress);
                    }
                }
                else
                {
                    foreach (var existingAddress in existingAddresses)
                    {
                        if (!updatedAddresses.Any(a => a.Id == existingAddress.Id))
                        {
                            await _unitOfWork.Repository<Address>().DeleteAsync(existingAddress);
                        }
                    }
                }

                customer.Addresses = updatedAddresses;

                await _unitOfWork.Repository<Customer>().UpdateAsync(customer);
                await _unitOfWork.CommitAsync();
            }
        }


        public async Task DeleteCustomerAsync(int id)
        {
            var customer = await _unitOfWork.Repository<Customer>().GetByIdAsync(id);
            if (customer != null)
            {
                await _unitOfWork.Repository<Customer>().DeleteAsync(customer);
                await _unitOfWork.CommitAsync();
            }
        }
    }
}